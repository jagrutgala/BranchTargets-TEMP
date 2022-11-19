using System.Net;

using Newtonsoft.Json;

using TargetSettingTool.UI.Models.Common;

namespace TargetSettingTool.UI.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next; _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                await ConvertException(context);
            }
        }

        private Task ConvertException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
            string result = GetErrorMessage("Internal server error occurred contact dev team.");
            context.Response.StatusCode = (int)httpStatusCode;
            return context.Response.WriteAsync(result);
        }

        private string GetErrorMessage(string message)
        {
            var response = new Response<string>();
            response.Succeeded = false;
            response.Errors = new List<string>();
            response.Errors.Add(message);
            return JsonConvert.SerializeObject(response);
        }
    }
}
