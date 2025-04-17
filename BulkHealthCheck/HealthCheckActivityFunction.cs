using Microsoft.Azure.Functions.Worker;

namespace BulkHealthCheck
{
    public class HealthCheckActivityFunction
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HealthCheckActivityFunction(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [Function(nameof(HealthCheckActivityFunction))]
        public async Task<bool> CheckApiHealth([ActivityTrigger] string url, FunctionContext context)
        {
            var httpClient = this.httpClientFactory.CreateClient("HealthCheckClient");

            // TODO: Think of additional headers to be passed if needed.
            var response = await httpClient.GetAsync(url);

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
