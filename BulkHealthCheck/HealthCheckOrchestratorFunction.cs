using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace BulkHealthCheck
{
    public class HealthCheckOrchestratorFunction
    {
        [Function(nameof(HealthCheckOrchestratorFunction))]
        public async Task RunOrchestrator(
            [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var healthCheckUrls = new List<string>();

            // Dummy limit here. This ideally is based on DB.
            var limit = 100000;
            for (var i = 0; i < limit; i++)
            {
                // Move URL to config
                healthCheckUrls.Add("https://microsoft.com");
            }

            var healthCheckTasks = healthCheckUrls.Select(url => context.CallActivityAsync<bool>(nameof(HealthCheckActivityFunction), url));

            await Task.WhenAll(healthCheckTasks);
        }
    }
}
