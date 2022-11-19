using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using TargetSettingTool.UI.Helpers;
using TargetSettingTool.UI.Models.Auth;
using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.Parameter;
using TargetSettingTool.UI.Models.ParameterType;

namespace TargetSettingTool.UI.Controllers
{
    public class ParameterController : Controller
    {
        private readonly ILogger<ParameterController> _logger;
        private readonly HttpClient client;

        public ParameterController(
            ILogger<ParameterController> logger,
            IConfiguration configuration
        )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
            _logger = logger;

        }

        #region Read
        [HttpGet]
        public ActionResult GetAllParameters()
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/Parameter/GetAllParameters").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<ParameterVm>> getAllParameters = JsonConvert.DeserializeObject<Response<List<ParameterVm>>>(data);
            return View(getAllParameters.Data);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult CreateParameter()
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/ParameterType/GetAllParameterTypes").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<ParameterTypeVm>> result = JsonConvert.DeserializeObject<Response<List<ParameterTypeVm>>>(data);
            ViewBag.ParameterTypes = new SelectList(result.Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateParameter(CreateParameterPageModel model, List<ParameterTargetModel> parametersTargets)
        {
            List<CreateParameterVm> parameterList = new List<CreateParameterVm>();
            foreach (ParameterTargetModel target in parametersTargets)
            {
                parameterList.Add(
                    new CreateParameterVm()
                    {
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        FinancialYear = model.FinancialYear,
                        ParameterTypeId = Guid.Parse(target.ParameterTypeId),
                        TargetAmount = double.Parse(target.TargetAmount)
                    }
                );
            }
            CreateParametersVm createParameters = new CreateParametersVm()
            {
                Parameters = parameterList,
                LoggedInUserId = SessionHelper.GetObjectFromJson<AuthResponseDto>(HttpContext.Session, "user").Id
            };
            string data = JsonConvert.SerializeObject(createParameters);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{client.BaseAddress}/Parameter/CreateParameters", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Parameter Added Successfully!");
                return RedirectToAction("GetAllParameters");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Add Parameter");
            }
            return RedirectToAction(nameof(GetAllParameters));
        }
        #endregion

        #region update
        [HttpGet]
        public ActionResult UpdateParameter(Guid id)
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/ParameterType/GetAllParameterTypes").Result;
            string getData = response.Content.ReadAsStringAsync().Result;
            Response<List<ParameterTypeVm>> result = JsonConvert.DeserializeObject<Response<List<ParameterTypeVm>>>(getData);
            ViewBag.ParameterTypes = new SelectList(result.Data, "Id", "Name");
            HttpResponseMessage res = client.GetAsync(client.BaseAddress + "/Parameter/GetParameterById?id=" + id.ToString()).Result;
            string data = res.Content.ReadAsStringAsync().Result;
            Response<UpdateParameterVm> result1 = JsonConvert.DeserializeObject<Response<UpdateParameterVm>>(data);
            return View(result1.Data);
        }

        [HttpPost]
        public ActionResult UpdateParameter(UpdateParameterVm model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"{client.BaseAddress}/Parameter/UpdateParameter", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Parameter Updated Successully!");
                return RedirectToAction("GetAllParameters");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Update Parameter");
            }
            return View();
        }
        #endregion

        #region Delete
        [HttpGet]
        public ActionResult DeleteParameter(Guid id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{client.BaseAddress}/Parameter/DeleteParameter?id={id}&logInUser={id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Parameter Deleted Successfully!");
                return RedirectToAction("GetAllParameters");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Delete Role");
            }
            return RedirectToAction("GetAllParameters");
        }
        #endregion
    }
}
