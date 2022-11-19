using System;
using System.Data;
using System.Net.Mime;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using TargetSettingTool.UI.Helpers;
using TargetSettingTool.UI.Models;
using TargetSettingTool.UI.Models.Auth;
using TargetSettingTool.UI.Models.Branch;
using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Services;

namespace TargetSettingTool.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly AddUsersFromExcelService _addUsersFromExcelService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient client;

        public UserController(
            ILogger<UserController> logger,
            AddUsersFromExcelService addUsersFromExcelService,
            IConfiguration configuration
        )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5000/api/v3"); ;
            _logger = logger;
            _addUsersFromExcelService = addUsersFromExcelService;
            _configuration = configuration;
        }

        #region Read
        public IActionResult GetAllUsers()
        {
            List<UserVm> users = GetData<List<UserVm>>($"/User/GetAllUsers");
            return View(users);
        }
        #endregion

        #region Create
        public IActionResult CreateUser()
        {
            List<RoleVm> roles = GetData<List<RoleVm>>($"/Role/GetAllRoles");
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            List<RightVm> rights = GetData<List<RightVm>>($"/Right/GetAllRights");
            ViewBag.Rights = rights;
            List<BranchVm> branches = GetData<List<BranchVm>>($"/UserBranch/GetBranchDropdown");
            ViewBag.Branches = new SelectList(branches, "Id", "Name");
            if (branches.Count == 0)
            {
                ViewBag.Branches = new List<SelectListItem>() { new SelectListItem { Text = "No Branches Available To Assign", Value = "" } };
            }
            UserVm user = new UserVm();
            return View(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserVm user)
        {
            user.Id = Guid.NewGuid();
            user.LoggedInUserId = Guid.NewGuid();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage responseMessage = client.PostAsync($"{client.BaseAddress}/User/CreateUser", content).Result;
            string data = responseMessage.Content.ReadAsStringAsync().Result;
            Response<Guid> response = JsonConvert.DeserializeObject<Response<Guid>>(data);
            if (response.Data != null)
            {
                this.SetTempAlert(TempAlertCode.Ok, "User Created Successfully");
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                TempData["type"] = "danger";
                TempData["msg"] = "Unable to Create User";
                return RedirectToAction("CreateUser", user);
            }
        }
        #endregion

        #region Update
        public IActionResult EditUser(Guid id)
        {
            UserVm user = GetData<UserVm>($"/User/GetUserById?id={id}");
            List<RoleVm> roles = GetData<List<RoleVm>>($"/Role/GetAllRoles");
            ViewBag.Roles = new SelectList(roles, "Id", "Name");
            List<RightVm> rights = GetData<List<RightVm>>($"/Right/GetAllRights");
            ViewBag.Rights = rights;
            List<BranchVm> branches = GetData<List<BranchVm>>($"/UserBranch/GetBranchDropdown");
            branches.AddRange(user.UserBranches.Select(x => x.Branch));
            ViewBag.Branches = new SelectList(branches, "Id", "Name");
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(UserVm user)
        {
            user.LoggedInUserId = new Guid();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage responseMessage = client.PutAsync($"{client.BaseAddress}/User/UpdateUser", content).Result;
            string data = responseMessage.Content.ReadAsStringAsync().Result;
            Response<bool> response = JsonConvert.DeserializeObject<Response<bool>>(data);
            if (response.Data == true)
            {
                this.SetTempAlert(TempAlertCode.Ok, "User Updated Successfully");
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                TempData["type"] = "danger";
                TempData["msg"] = "Unable to Update User";
                return RedirectToAction("EditUser", user);
            }
        }
        #endregion

        #region Delete
        public IActionResult DeleteUser(Guid id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{client.BaseAddress}/User/DeleteUser?id={id}&loggedInUserId={id}").Result;
            string data = responseMessage.Content.ReadAsStringAsync().Result;
            Response<bool> response = JsonConvert.DeserializeObject<Response<bool>>(data);
            if (response.Data == true)
            {
                this.SetTempAlert(TempAlertCode.Ok, "User Deleted Successfully");
                return RedirectToAction("GetAllUsers");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "Unable to Delete User");
                return RedirectToAction("GetAllUsers");
            }
        }
        #endregion

        #region Remote Checks
        [HttpPost]
        public JsonResult IsEmailExist(string email, Guid id)
        {
            bool response = GetData<bool>($"/User/IsEmailExist?email={email}&id={id}");
            return Json(response);
        }
        [HttpPost]
        public JsonResult IsEmployeeCodeExist(string employeeCode, Guid id)
        {
            bool response = GetData<bool>($"/User/IsEmployeeCodeExist?employeeCode={employeeCode}&id={id}");
            return Json(response);
        }
        #endregion

        public T GetData<T>(string queryUrl)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{client.BaseAddress}{queryUrl}").Result;
            string data = responseMessage.Content.ReadAsStringAsync().Result;
            Response<T> response = JsonConvert.DeserializeObject<Response<T>>(data);
            return response.Data;
        }

        [HttpGet]
        public IActionResult AddUsersFromExcel()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersFromExcel(IFormFile file)
        {
            DataTable excelTable = await _addUsersFromExcelService.ReadExcelAsync(file);

            StringBuilder errorMessage = _addUsersFromExcelService.ValidateDataTable(excelTable);
            if (errorMessage.Length > 0)
            {
                this.SetTempAlert(TempAlertCode.Error, errorMessage.ToString());
                return RedirectToAction(nameof(AddUsersFromExcel));
            }

            DataTable filteredExcelTable = _addUsersFromExcelService.FormatDataTable(excelTable);

            AuthResponseDto loggedInUser = SessionHelper.GetObjectFromJson<AuthResponseDto>(HttpContext.Session, "user");
            DataTable createdUsersTable = await _addUsersFromExcelService.AddUsersAsync(
                _configuration.GetConnectionString("ApplicationConnectionString"),
                filteredExcelTable,
                loggedInUser.Id
            );

            if (createdUsersTable.Rows.Count <= 0)
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable to create multiple users");
                return RedirectToAction(nameof(AddUsersFromExcel));
            }

            await _addUsersFromExcelService.SendMailToUsersAsync(createdUsersTable);

            this.SetTempAlert(TempAlertCode.Ok, "Multiple users created successfully");
            return RedirectToAction(nameof(GetAllUsers));
    }
}
}
