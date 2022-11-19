using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.UI.Controllers
{
    public enum TempAlertCode
    {
        Ok = 0, Error = 1, Warning = 2
    }

    public static class ControllerExtension
    {
        public static void SetTempAlert(this Controller controller, TempAlertCode alert, string alertMessage = "")
        {
            controller.TempData["alert"] = (int)alert;
            controller.TempData["alertMessage"] = alertMessage;
        }
    }
}
