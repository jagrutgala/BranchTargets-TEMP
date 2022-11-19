using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using TargetSettingTool.UI.Models.BranchTarget;
using Newtonsoft.Json;
using TargetSettingTool.UI.Models.Common;
using System.Web;

namespace TargetSettingTool.UI.Controllers
{
    public class BranchTargetController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BranchTargetController> _logger;
        private readonly HttpClient _client;

        public BranchTargetController(IMapper mapper, ILogger<BranchTargetController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
        }

        [HttpGet]
        public async Task<JsonResult> GetAllTargetsJson(Guid branchId, DateTime startDate)
        {
            // Build Query for Uri
            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["branchId"] = branchId.ToString();
            requestQuery["startDate"] = startDate.ToString();

            // Build Uri with path & query
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/BranchTarget/GetAllTargets";
            uriBuilder.Query = requestQuery.ToString();

            // Actual Api Call
            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<List<BranchTargetVm>> responseData = JsonConvert.DeserializeObject<Response<List<BranchTargetVm>>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Unable to fetch data");
            }
            return Json(responseData.Data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTargets()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTargets(BranchTargetGetRequestVm btgrVm)
        {
            return RedirectToAction(nameof(UpdateTargets));
        }

    }
}
