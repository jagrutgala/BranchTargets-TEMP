using System.Net.Mime;
using System.Text;
using System.Web;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models;
using TargetSettingTool.UI.Models.Branch;
using TargetSettingTool.UI.Models.Common;

namespace TargetSettingTool.UI.Controllers
{
    public class BranchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BranchController> _logger;
        private readonly HttpClient _client;

        public BranchController(IMapper mapper, ILogger<BranchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(configuration.GetValue<string>("Api:TargetSettingTool"));
        }

        #region Read
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            HttpResponseMessage response = await _client.GetAsync($"{_client.BaseAddress}/Branch/GetAllBranches");
            string data = await response.Content.ReadAsStringAsync();
            Response<List<BranchVm>> responseData = JsonConvert.DeserializeObject<Response<List<BranchVm>>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Unable to fetch data");
            }
            return View(responseData.Data);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> AddBranch()
        {
            await PopulateRegionItems();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchCreateRequestModel bcrm)
        {
            bcrm.LoggedInUserId = Guid.NewGuid(); // Get user id from session
            HttpContent content = new StringContent(JsonConvert.SerializeObject(bcrm), Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage response = await _client.PostAsync($"{_client.BaseAddress}/Branch/CreateBranch", content);
            string data = await response.Content.ReadAsStringAsync();
            Response<Guid> responseData = JsonConvert.DeserializeObject<Response<Guid>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Failed to create Branch");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "New branch created Successfully.");
            }
            return RedirectToAction(nameof(GetAllBranches));
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> EditBranch(Guid id)
        {
            await PopulateRegionItems();

            // Build Api Call Query
            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["id"] = id.ToString();
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/Branch/GetBranchById";
            uriBuilder.Query = requestQuery.ToString();

            // Make Api Call
            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<BranchVm> responseData = JsonConvert.DeserializeObject<Response<BranchVm>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Unable to fetch data.");
                return RedirectToAction(nameof(GetAllBranches));
            }
            BranchUpdateRequestModel branchRequestModel = _mapper.Map<BranchUpdateRequestModel>(responseData.Data);
            return View(branchRequestModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditBranch(BranchUpdateRequestModel burm)
        {
            burm.LoggedInUserId = Guid.NewGuid(); // Get user id from session
            HttpContent content = new StringContent(JsonConvert.SerializeObject(burm), Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/Branch/UpdateBranch", content);
            string data = await response.Content.ReadAsStringAsync();
            Response<bool> responseData = JsonConvert.DeserializeObject<Response<bool>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Failed to update Branch");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "Branch updated Successfully.");
            }
            return RedirectToAction(nameof(GetAllBranches));
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> RemoveBranch(Guid id)
        {
            BranchDeleteRequestModel bdrm = new BranchDeleteRequestModel()
            {
                Id = id,
                LoggedInUserId = Guid.NewGuid() // Get user id from session
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(bdrm), Encoding.UTF8, MediaTypeNames.Application.Json);
            HttpResponseMessage response = await _client.PutAsync($"{_client.BaseAddress}/Branch/DeleteBranch", content);
            string data = await response.Content.ReadAsStringAsync();
            Response<bool> responseData = JsonConvert.DeserializeObject<Response<bool>>(data);
            if (!responseData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Failed to delete Branch.");
            }
            else
            {
                this.SetTempAlert(TempAlertCode.Ok, "Branch deleted Successfully.");
            }
            return RedirectToAction(nameof(GetAllBranches));
        }
        #endregion

        #region Remote Checks
        public async Task<JsonResult> IsBranchNameAvailable(string name)
        {
            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["name"] = name;
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/Branch/GetBranchByName";
            uriBuilder.Query = requestQuery.ToString();

            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<BranchVm> responseData = JsonConvert.DeserializeObject<Response<BranchVm>>(data);
            if (!responseData.Succeeded)
            {
                return Json(true);
            }
            return Json(false);
        }
        public async Task<JsonResult> IsBranchNameAvailableEdit(string name, Guid id)
        {
            var requestQueryId = HttpUtility.ParseQueryString("");
            requestQueryId["id"] = id.ToString();
            var uriBuilderId = new UriBuilder(_client.BaseAddress);
            uriBuilderId.Path += "/Branch/GetBranchById";
            uriBuilderId.Query = requestQueryId.ToString();

            HttpResponseMessage responseId = await _client.GetAsync(uriBuilderId.ToString());
            string dataId = await responseId.Content.ReadAsStringAsync();
            Response<BranchVm> responseDataId = JsonConvert.DeserializeObject<Response<BranchVm>>(dataId);


            var requestQuery = HttpUtility.ParseQueryString("");
            requestQuery["name"] = name;
            var uriBuilder = new UriBuilder(_client.BaseAddress);
            uriBuilder.Path += "/Branch/GetBranchByName";
            uriBuilder.Query = requestQuery.ToString();

            HttpResponseMessage response = await _client.GetAsync(uriBuilder.ToString());
            string data = await response.Content.ReadAsStringAsync();
            Response<BranchVm> responseData = JsonConvert.DeserializeObject<Response<BranchVm>>(data);
            if (!responseData.Succeeded || responseDataId.Data.Name == name)
            {
                return Json(true);
            }
            return Json(false);
        }
        #endregion

        public async Task PopulateRegionItems()
        {
            // Build Api Call Query
            var uriBuilderRegion = new UriBuilder(_client.BaseAddress);
            uriBuilderRegion.Path += "/User/GetUsersWhereRoleIsRegion";

            // Make Api Call
            HttpResponseMessage responseRegion = await _client.GetAsync(uriBuilderRegion.ToString());
            string regionData = await responseRegion.Content.ReadAsStringAsync();
            Response<List<UserVm>> responseRegionData = JsonConvert.DeserializeObject<Response<List<UserVm>>>(regionData);
            if (!responseRegionData.Succeeded)
            {
                this.SetTempAlert(TempAlertCode.Error, "Sorry! Something went wrong ");
            }
            ViewBag.RegionItems = new SelectList(responseRegionData.Data, "Id", "Name");
            if (responseRegionData.Data.Count == 0)
            {
                ViewBag.RegionItems = new List<SelectListItem>() { new SelectListItem { Text = "No Regions Available To Assign", Value = "" } };
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
