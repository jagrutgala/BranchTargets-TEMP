using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.Api.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
