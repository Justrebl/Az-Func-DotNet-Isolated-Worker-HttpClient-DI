using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzFuncDotnet7
{
    public class TriggerTest
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public TriggerTest(ILoggerFactory loggerFactory, IHttpClientFactory httpClientFactory)
        {
            _logger = loggerFactory.CreateLogger<TriggerTest>();
            _httpClientFactory = httpClientFactory;
        }

        [Function("TriggerTest")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            int userId = 1;

            var httpClient = _httpClientFactory.CreateClient("JsonPlaceholder");
            string resultFromApi = await httpClient.GetStringAsync($"todos/{userId}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(resultFromApi);

            return response;
        }
    }
}