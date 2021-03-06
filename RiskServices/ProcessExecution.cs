using FRMObjects;
using FRMObjects.model;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Linq;

namespace RiskServices
{
    public class ProcessExecution
    {
        [FunctionName("ProcessExecution")]
        public void Run([QueueTrigger("tool-executions", Connection = "queue-connection-string")] CloudQueueMessage theQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {theQueueItem}");

            using (FRP_LandingContext context = new())
            {
                string guid = theQueueItem.AsString;

                ToolExecutionLog item = context.ToolExecutionLogs.First(u => u.Guid.ToString() == guid);
                if (item != null)
                {
                    item.Status = "P";
                    item.RunStartTimestamp = DateTime.Now;

                    context.SaveChanges();
                }
            }
            // Create a processor object and process it
        }
    }
}
