using System.Text;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.Role;

namespace TargetSettingTool.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly HttpClient client;

        public RoleController(
            ILogger<RoleController> logger,
            IConfiguration configuration
        )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
            _logger = logger;

        }

        #region Read
        [HttpGet]
        public ActionResult GetAllRoles()
        {
            HttpResponseMessage response = client.GetAsync($"{client.BaseAddress}/Role/GetAllRoles").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<GetAllRolesVm>> getAllRoles = JsonConvert.DeserializeObject<Response<List<GetAllRolesVm>>>(data);
            return View(getAllRoles.Data);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(CreateRoleVm model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"{client.BaseAddress}/Role/CreateRole", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Role Created Successully!");
                return RedirectToAction("GetAllRoles");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Create Role");
                return View();
            }
        }
        #endregion

        #region Update
        [HttpGet]
        public ActionResult UpdateRole(Guid id)
        {
            HttpResponseMessage response = client.GetAsync($"{ client.BaseAddress}/Role/GetRoleById?id={id}").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<UpdateRoleVm> role = JsonConvert.DeserializeObject<Response<UpdateRoleVm>>(data);
            return View(role.Data);
        }

        [HttpPost]
        public ActionResult UpdateRole(UpdateRoleVm model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"{client.BaseAddress}/Role/UpdateRole", content).Result;
            if (response.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Role Updated Successully!");
                return RedirectToAction("GetAllRoles");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Update Role");
                return View();
            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public ActionResult DeleteRole(Guid id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync($"{client.BaseAddress}/Role/DeleteRole?id={id}&logInUser={id}").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                this.SetTempAlert(TempAlertCode.Ok, "Role Deleted Successfully!");
                return RedirectToAction("GetAllRoles");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Error, "Unable To Delete Role");
                return RedirectToAction("GetAllRoles");
            }
        }
        #endregion

        #region Remote Checks
        public JsonResult IsRoleExists(Guid id, string name)
        {
            HttpResponseMessage responseMessage = client.GetAsync($"{client.BaseAddress}/Role/GetRoleByName?id={id}&name={name}").Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                Response<bool> isRoleExists = JsonConvert.DeserializeObject<Response<bool>>(data);
                if (isRoleExists.Data)
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
