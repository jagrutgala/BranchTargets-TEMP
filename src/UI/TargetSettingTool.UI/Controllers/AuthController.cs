using System.Text;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using TargetSettingTool.UI.Helpers;
using TargetSettingTool.UI.Models.Auth;
using TargetSettingTool.UI.Models.Common;

namespace TargetSettingTool.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly HttpClient client;

        public AuthController(
            ILogger<AuthController> logger,
            IConfiguration configuration
        )
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
            _logger = logger;

        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginVm login)
        {

            // Validate Captcha Code
            if (!Captcha.ValidateCaptchaCode(login.CaptchaCode, HttpContext))
            {
                this.SetTempAlert(TempAlertCode.Error, "Invalid Captcha");
                return View();
            }

            string data = JsonConvert.SerializeObject(login);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Auth/UserLogin", content).Result;
            if(response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                Response<AuthResponseDto> loginData = JsonConvert.DeserializeObject<Response<AuthResponseDto>>(responseData);
                Console.WriteLine(loginData.Data);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", loginData.Data);

                if (loginData.Data.Role.Name == "Admin")
                {
                    this.SetTempAlert(TempAlertCode.Ok, "Login Successfull");
                    return RedirectToAction("GetAllUsers", "User");
                }
                else
                {
                    this.SetTempAlert(TempAlertCode.Ok, "Login Successfull");
                    return RedirectToAction("Index", "Home");
                }

            }
            this.SetTempAlert(TempAlertCode.Error, "Invalid Login Credentials");
            return View();

        }
        #endregion Login

        #region ForgotPassword
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordVm forgotPassword)
        {
            string data = JsonConvert.SerializeObject(forgotPassword);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Auth/ForgetPassword", content).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            Response<string> res = JsonConvert.DeserializeObject<Response<string>>(result);
            if (response.IsSuccessStatusCode)
            {
                TempData["Title"] = $"Password has been reset successfully";
                TempData["Message"] = $"Password has been sent to {res.Message}.";
                return RedirectToAction("Login");
            }
            this.SetTempAlert(TempAlertCode.Error, "Invalid Employee Code");
            return View();
        }
        #endregion

        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            //TempData["alert"] = TempAlertCode.Ok;
            //TempData["alertMessage"] = "LogOut Successfull";
            return RedirectToAction(nameof(Login));
        }
        #endregion

        [Route("get-captcha-image")]
        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 36;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

    }
}
