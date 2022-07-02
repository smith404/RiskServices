using System;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RiskServices
{
    public class ProcessExecution
    {
        [FunctionName("ProcessExecution")]
        public void Run([QueueTrigger("tool-executions", Connection = "queue-connection-string")] CloudQueueMessage myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
