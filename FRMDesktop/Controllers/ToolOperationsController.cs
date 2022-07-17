using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using FRMObjects;
using FRMObjects.model;
using Microsoft.AspNetCore.Mvc;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolOperationsController : Controller
    {
        private readonly ILogger<ToolOperationsController> _logger;

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public ToolOperationsController(ILogger<ToolOperationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ToolStep")]
        public ToolStep? GetStep(long id)
        {
            ToolStep? step;

            if (id == -1)
            {
                step = new();
                //string? userName = HttpContext.User.Identity?.Name;
                //step.Description = (userName != null) ? userName : "Unknown User";
            }
            else
            {
                using (FRP_LandingContext context = new())
                {
                    step = context.ToolSteps.Find(id);
                }
            }

            return step;
        }

        [HttpPut("ToolStep")]
        public ToolStep PutStep(ToolStep step)
        {
            using (FRP_LandingContext context = new())
            {
                context.ToolSteps.Update(step);
                int count = context.SaveChanges();
            }

            return step;
        }

        [HttpPost("ToolStep")]
        public ToolStep PostStep(ToolStep step)
        {
            using (FRP_LandingContext context = new())
            {
                context.ToolSteps.Add(step);
                int count = context.SaveChanges();
            }

            return step;
        }

        [HttpDelete("ToolStep")]
        public ToolStep DeleteStep(ToolStep step)
        {
            using (FRP_LandingContext context = new())
            {
                context.ToolSteps.Remove(step);
                int count = context.SaveChanges();
            }

            return step;
        }

        [HttpGet("ToolExecution")]
        public ToolExecutionLog? GetQueueExecution(long id)
        {
            ToolExecutionLog? item;

            using (FRP_LandingContext context = new())
            {
                item = context.ToolExecutionLogs.Find(id);
            }

            return item;
        }

        [HttpGet("ReadToolExecution")]
        public ToolExecutionLog? ReadToolExecution(string guid)
        {
            ToolExecutionLog? item;

            using (FRP_LandingContext context = new())
            {
                item = context.ToolExecutionLogs.Where(item => item.Guid == guid).SingleOrDefault();
            }

            return item;
        }

        [HttpPost("ToolExecution")]
        public ToolExecutionLog PostQueueExecution(ToolExecutionLog item)
        {
            using (FRP_LandingContext context = new())
            {
                // Set the system fields
                item.Guid = Guid.NewGuid().ToString();
                string? userName = HttpContext.User.Identity?.Name;
                item.Requestor = (userName != null) ? userName : "Unknown User";
                item.Status = "Q";
                item.RequestedTimestamp = DateTime.Now;

                context.ToolExecutionLogs.Add(item);
                int count = context.SaveChanges();

                // We should have inserted one line
                if (count == 1)
                {
                    // Create the blob
                    BlobServiceClient blobServiceClient = StorageAccountHelper.GetBlobServiceClient();
                    string? container = Environment.GetEnvironmentVariable("storage-account-blob-container");
                    if (container != null)
                    {
                        BlobContainerClient containerClient = StorageAccountHelper.GetBlobContainerClient(blobServiceClient, container);
                        StorageAccountHelper.WriteContentToBlob(containerClient, item.Guid.ToString(), "request.json", item.RunConfiguration);
                    }

                    // Put execution message on queue
                    QueueServiceClient queueServiceClient = StorageAccountHelper.GetQueueServiceClient();
                    string? queue = Environment.GetEnvironmentVariable("storage-account-tool-queue");
                    if (queue != null)
                    {
                        QueueClient queueClient = StorageAccountHelper.GetQueueClient(queueServiceClient, queue);
                        queueClient.SendMessage(Base64Encode(item.Guid.ToString()));
                    }
                }
            }

            return item;
        }
    }
}
