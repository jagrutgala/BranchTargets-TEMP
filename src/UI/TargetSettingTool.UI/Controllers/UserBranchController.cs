using System.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models;
using TargetSettingTool.UI.Models.Common;

namespace TargetSettingTool.UI.Controllers
{
    public class UserBranchController : Controller
    {
        private readonly ILogger<UserBranchController> _logger;
        HttpClient _client;

        public UserBranchController(ILogger<UserBranchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
        }

        #region Ajax Json Endpoints
        public async Task<JsonResult> GetBranchesByUserIdJson(Guid userId)
        {
            // Build Query for Uri
            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["userId"] = userId.ToString();

            // Build Uri with path & query
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/UserBranch/GetBranchesByUserId";
            uriBuilder.Query = requestQuery.ToString();

            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<List<UserBranchVm>> responseData = JsonConvert.DeserializeObject<Response<List<UserBranchVm>>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Unable to fetch data");
            }
            return Json(responseData.Data);
        }
        #endregion

    }
}
