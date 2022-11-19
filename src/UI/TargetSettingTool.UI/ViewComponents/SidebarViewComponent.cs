using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using TargetSettingTool.UI.Helpers;
using TargetSettingTool.UI.Models.Auth;
using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.SideBar;

namespace TargetSettingTool.UI.ViewComponenets;


[ViewComponent(Name = "SidebarViewComponent")]
public class SidebarViewComponent : ViewComponent
{
    HttpClient client;
    public SidebarViewComponent(IConfiguration configuration)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //HttpContext.Session.GetString("User");
        var userSession = SessionHelper.GetObjectFromJson<AuthResponseDto>(HttpContext.Session, "user");
        if (userSession is not null)
        {
            ViewBag.Name = userSession.Role.Name;
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/SideBarMenu/GetMenu").Result;
            string data = response.Content.ReadAsStringAsync().Result;
            Response<List<SideBar>> sidebar = JsonConvert.DeserializeObject<Response<List<SideBar>>>(data);
            return View(sidebar.Data);
        }
        return View("EmptySidebar");
    }
}