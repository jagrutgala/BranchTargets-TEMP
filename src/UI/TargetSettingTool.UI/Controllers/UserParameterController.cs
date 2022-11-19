using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models.Common;
using TargetSettingTool.UI.Models.UserParameter;

namespace TargetSettingTool.UI.Controllers
{
    public class UserParameterController : Controller
    {
        private readonly ILogger<UserParameterController> _logger;
        private HttpClient _client;

        public UserParameterController(
            ILogger<UserParameterController> logger,
            IConfiguration configuration
        )
        {
            _logger = logger;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserParametersByBranchIdJson(Guid branchId)
        {
            // Build Query for Uri
            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["branchId"] = branchId.ToString();

            // Build Uri with path & query
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/UserParameter/GetUserParametersByBranchId";
            uriBuilder.Query = requestQuery.ToString();

            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<List<UserParameterVm>> responseData = JsonConvert.DeserializeObject<Response<List<UserParameterVm>>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Unable to fetch data");
            }
            return Json(responseData.Data);
        }
    }
}
