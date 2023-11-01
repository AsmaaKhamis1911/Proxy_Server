using Microsoft.AspNetCore.Mvc;
using Proxy_Server.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_Server.Controllers
{
    public class ProxyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        [Route("api/proxy")]
        public async Task<IActionResult> ProxyRequest([FromBody] ProxyRequestModel requestModel)
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = new HttpMethod(requestModel.Method),
                    RequestUri = new Uri(requestModel.Url)
                };

                foreach (var header in requestModel.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

                if (!string.IsNullOrEmpty(requestModel.Body))
                {
                    requestMessage.Content = new StringContent(requestModel.Body, Encoding.UTF8, "application/json");
                }

                var response = await client.SendAsync(requestMessage);

                var responseContent = await response.Content.ReadAsStringAsync();

                return Content(responseContent, response.Content.Headers.ContentType.MediaType);

            }

        }  
    
    }
}
