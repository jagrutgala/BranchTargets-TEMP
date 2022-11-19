using System.Text;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.Parameter;
using TargetSettingTool.UI.Models.ParameterType;

namespace TargetSettingTool.UI.Controllers
{
    public class ParameterTypeController : Controller
    {
        private readonly ILogger<ParameterTypeController> _logger;
        private readonly HttpClient client;

        public ParameterTypeController(ILogger<ParameterTypeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
        }

        #region Read
        [HttpGet]
        public ActionResult GetAllParameterTypes()
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/ParameterType/GetAllParameterTypes").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<ParameterTypeVm>> getAllParameterTypes = JsonConvert.DeserializeObject<Response<List<ParameterTypeVm>>>(data);
            return View(getAllParameterTypes.Data);
        }

        [HttpGet]
        public ActionResult GetAllParameterTypesJson()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/ParameterType/GetAllParameterTypes").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<ParameterTypeVm>> getAllParameterTypesResponse = JsonConvert.DeserializeObject<Response<List<ParameterTypeVm>>>(data);
            return Json(getAllParameterTypesResponse.Data);
        }

        #endregion

        #region Create
        [HttpGet]
        public ActionResult CreateParameterType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateParameterType(CreateParameterTypeVm model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{client.BaseAddress}/ParameterType/CreateParameterType", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Parameter Type Created Successfully!");
                return RedirectToAction("GetAllParameterTypes");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "Unable To Create Parameter Type");
            }
            return View();
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult UpdateParameterType(Guid id)
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/ParameterType/GetParameterTypeById?id={id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<UpdateParameterTypeVm> parameterType = JsonConvert.DeserializeObject<Response<UpdateParameterTypeVm>>(data);
            return View(parameterType.Data);
        }

        [HttpPost]
        public ActionResult UpdateParameterType(UpdateParameterTypeVm updateParameterType)
        {
            string data = JsonConvert.SerializeObject(updateParameterType);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/ParameterType/UpdateParameterType", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "ParameterType Updated Successully!");
                return RedirectToAction("GetAllParameterTypes");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Update ParameterType");
            }
            return View();
        }
        #endregion

        #region Delete
        public ActionResult DeleteParameterType(Guid id, string name)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{client.BaseAddress}/ParameterType/DeleteParameterType?id={id}&logInUser={id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "ParameterType Deleted Successfully!");
                return RedirectToAction("GetAllParameterTypes");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Delete ParameterType");
                return RedirectToAction("GetAllParameterTypes");
            }

        }
        #endregion

        #region Remote Checks
        public JsonResult IsParameterTypeExists(Guid id, string name)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{client.BaseAddress}/ParameterType/GetParameterTypeByName?id={id}&name={name}").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                Response<bool> isParameterTypeExists = JsonConvert.DeserializeObject<Response<bool>>(data);
                if (isParameterTypeExists.Data)
                {
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            return Json(false);
        }
        #endregion

        [HttpPost]
        public JsonResult IsParameterTypeUsed(string name)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{client.BaseAddress}/Parameter/GetParameterByType?name={name}").Result;
            string data = responseMessage.Content.ReadAsStringAsync().Result;
            Response<bool> result = JsonConvert.DeserializeObject<Response<bool>>(data);
            return Json(result.Data);
        }

    }
}
