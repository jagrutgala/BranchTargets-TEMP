using System.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json;

using TargetSettingTool.UI.Models;
using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.Right;

namespace TargetSettingTool.UI.Controllers
{
    public class RightController : Controller
    {
        private readonly ILogger<RightController> _logger;
        private readonly HttpClient client;

        public RightController(
            ILogger<RightController> logger,
            IConfiguration configuration
        )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
            _logger = logger;
        }

        #region Read
        [HttpGet]
        public ActionResult GetAllRights()
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/Right/GetAllRights").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<GetRightVM>> allRight = JsonConvert.DeserializeObject<Response<List<GetRightVM>>>(data);
            return View(allRight.Data);
        }

        #endregion

        #region Create
        [HttpGet]
        public ActionResult CreateRight()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRight(CreateRightVm rightsVm)
        {
            string data = JsonConvert.SerializeObject(rightsVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}/Right/CreateRight", content);
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Right Created Successfully!");
                return RedirectToAction("GetAllRights");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "Unable To Create Role");
                return View();
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult UpdateRight(Guid Id)
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/Right/GetRightById?Id={Id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<UpdateRightsVm> right = JsonConvert.DeserializeObject<Response<UpdateRightsVm>>(data);
            return View(right.Data);
        }

        [HttpPost]
        public ActionResult UpdateRight(UpdateRightsVm rightsVm)
        {
            string data = JsonConvert.SerializeObject(rightsVm);
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"{client.BaseAddress}/Right/UpdateRight", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Right Updated Successfully!");
                return RedirectToAction("GetAllRights");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Right Update Failed!");
                return View();
            }

        }
        #endregion

        #region Delete
        [HttpGet]
        public ActionResult DeleteRight(Guid id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{client.BaseAddress}/Right/DeleteRight?id={id}&loggedIn={id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Right Deleted Successfully!");
                return RedirectToAction("GetAllRights");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Right Deletion Failed!");
                return RedirectToAction("GetAllRights");
            }
        }
        #endregion

        #region Remote Checks
        [HttpGet]
        public async Task<JsonResult> IsRightExist(Guid id, string name)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"{client.BaseAddress}/Right/IsRightExist?id={id}&right={name}");
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                Response<bool> isRightExist = JsonConvert.DeserializeObject<Response<bool>>(data);
                if (isRightExist.Data == true)
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
    }
}
