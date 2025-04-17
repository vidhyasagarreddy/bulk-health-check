using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;

namespace BulkHealthCheck
{
    public class HealthCheckStarterFunction
    {

        //[Function(nameof(HealthCheckStarterFunction))]
        //public async Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, [DurableClient] DurableTaskClient client)
        //{
        //    var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(HealthCheckOrchestratorFunction));
        //}

        [Function(nameof(HealthCheckStarterFunction))]
        public async Task RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            var instanceId = await client.ScheduleNewOrchestrationInstanceAsync(nameof(HealthCheckOrchestratorFunction));
        }
    }
}
