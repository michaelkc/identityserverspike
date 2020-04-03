using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc2.Models;
using Newtonsoft.Json;
using SharedConfiguration;

namespace Mvc2.Controllers
{
    public class ApiCallController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _context;
        private IHttpClientFactory _clientFactory;

        public ApiCallController(ILogger<HomeController> logger, IHttpContextAccessor context, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _context = context;
            _clientFactory = clientFactory;
        }

        [Authorize]
        public async Task<IActionResult> AccessToken()
        {
            var token = await _context.HttpContext.GetTokenAsync("access_token");
            return Content(token);
        }

        [Authorize]
        public async Task<IActionResult> GetApi1()
        {
            var token = await _context.HttpContext.GetTokenAsync("access_token");
            using var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var url = Api1.BaseUrl + "test";
            var sb = new StringBuilder();

            sb.AppendLine("Calling " + url);
            
            var response = await client.GetAsync(url);
            var responseContent = await response.Content.ReadAsStringAsync();
            sb.AppendLine("Response: " + (int) response.StatusCode);
            sb.AppendLine("Response Content:");
            sb.AppendLine(ReformatJson(responseContent));
            return Content(sb.ToString());
        }

        private static string ReformatJson(string json)
        {
            dynamic _ = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(_, Formatting.Indented);
        }

    }
}
