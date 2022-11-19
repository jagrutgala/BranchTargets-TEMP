using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

using ExcelDataReader;

using Microsoft.Data.SqlClient;

namespace TargetSettingTool.UI.Services
{
    public class AddUsersFromExcelService
    {
        private readonly ILogger<AddUsersFromExcelService> _logger;

        public AddUsersFromExcelService(ILogger<AddUsersFromExcelService> logger)
        {
            _logger = logger;
        }

        public async Task<DataTable> ReadExcelAsync(IFormFile file)
        {
            DataTable excelTable;
            MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(memoryStream))
            {
                DataSet excelData = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
                if (excelData is null)
                {
                    throw new ApplicationException();
                }

                excelTable = excelData.Tables[0];
            }
            return excelTable;
        }

        public StringBuilder ValidateDataTable(DataTable dt)
        {
            StringBuilder errorMessage = new StringBuilder();

            if (dt.AsEnumerable()
                    .Where(r => r["Name"] is DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Name should not blank in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r => r["Email"] is DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Email should not blank in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r => r["PhoneNumber"] is DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("PhoneNumber should not blank in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r => r["Role"] is DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Role should not blank in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r =>
                        (r["Role"] is "Admin" || r["Role"] is "Strategy Team" || r["Role"] is "Branch User") &&
                        r["Right"] is not DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Right should be blank when Role is Admin, Strategy Team or Branch User in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r =>
                        (r["Role"] is "Vertical" || r["Role"] is "Zone" || r["Role"] is "Region") &&
                        (r["Right"] is DBNull)
                    )
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Right should be either Maker, Checker or Validator when Role is Vertical, Zone or Region in file.");
            }

            if (dt.AsEnumerable()
                    .Where(r => r["Role"] is "Branch User" && r["Branches"] is DBNull)
                    .ToList().Count > 0)
            {
                errorMessage.AppendLine("Branches must be specified with comma separated values when Role is Branch User in file");
            }
            return errorMessage;
        }

        public DataTable FormatDataTable(DataTable dt)
        {
            DataTable formattedExcelTable = dt.Clone();
            for (int i = 0; i < formattedExcelTable.Columns.Count; i++)
            {
                formattedExcelTable.Columns[i].DataType = typeof(string);
            }

            formattedExcelTable = dt.AsEnumerable()
                .Select(row =>
                {
                    DataRow newRow = formattedExcelTable.NewRow();
                    newRow["Name"] = row["Name"].ToString();
                    newRow["Email"] = row["Email"].ToString();
                    newRow["PhoneNumber"] = row["PhoneNumber"].ToString();
                    newRow["Role"] = row["Role"].ToString();
                    newRow["Right"] = row["Right"].ToString();
                    newRow["Branches"] = row["Branches"].ToString();
                    return newRow;
                }).CopyToDataTable();

            return formattedExcelTable;
        }

        public async Task<DataTable> AddUsersAsync(string connectionString, DataTable userDt, Guid loggedInUserId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            try
            {
                command.Connection = connection;
                command.CommandText = "USP_Insert_User";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@User_Details", userDt);
                command.Parameters.AddWithValue("@CreatedBy", loggedInUserId);
                await connection.OpenAsync();
                dataAdapter.Fill(dt);
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
            return dt;
        }

        public async Task SendMailToUsersAsync(DataTable dt)
        {
            Thread t = new Thread(() =>
            {
                for (int index = 0; index < dt.Rows.Count; index++)
                {
                    SendMail(dt.Rows[index]);
                }
            });
            t.Start();
        }

        private void SendMail(object dtRow)
        {
            DataRow row = (DataRow)dtRow;
            MailMessage mail = GetEmail(row);
            try
            {
                SmtpClient smtp = new SmtpClient(host: "smtp.gmail.com", port: 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("malaysatiya19@gmail.com", "fgdkahjoqutcunoa");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Sending Login Credentials: {ex.Message}");
            }
        }

        MailMessage GetEmail(DataRow row)
        {
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("malaysatiya19@gmail.com"),
                Body = $"Dear {row["Email"].ToString()}, <br> <br> Your Registration to Target Setting Tool has been done successfully.  <br><br> Please find your Login Credentials below : <br>Employee Code : {row["EmployeeCode"].ToString()}<br>Password : {row["Password"].ToString()}<br><br><b>Note* : Don't share your Employee Code And Password with anyone.</b><br><br> Regards, <br>TargetSettingTool Team.",
                Subject = "Login Credentials",
                IsBodyHtml = true,
            };
            mail.To.Add(row["Email"].ToString());
            mail.Bcc.Add(new MailAddress("sonali.pardeshi@neosoftmail.com"));
            // mail.Bcc.Add(new MailAddress("malaysatiya19@gmail.com"));
            return mail;
        }
    }
}
