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
                step = new ToolStep
                {
                    Description = "New Step",
                    StepType = "TBD",
                    Definition = "New Step",
                    HasOutput = true,
                    TestObject = "",
                    DatasetName = "dataset",
                    Message = "New Step",
                    Format = "TDB",
                    Active = true,
                };
            }
            else
            {
                using (FRMContext context = new())
                {
                    step = context.ToolSteps.Find(id);
                }
            }

            return step;
        }

        [HttpPut("ToolStep")]
        public ToolStep PutStep(ToolStep step)
        {
            using (FRMContext context = new())
            {
                context.ToolSteps.Update(step);
                int count = context.SaveChanges();
            }

            return step;
        }

        [HttpPost("ToolStep")]
        public ToolStep PostStep(ToolStep step)
        {
            using (FRMContext context = new())
            {
                context.ToolSteps.Add(step);
                int count = context.SaveChanges();
            }

            return step;
        }

        [HttpDelete("ToolStep")]
        public ToolStep DeleteStep(ToolStep step)
        {
            using (FRMContext context = new())
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

            using (FRMContext context = new())
            {
                item = context.ToolExecutionLogs.Find(id);
            }

            return item;
        }

        [HttpPost("ToolExecution")]
        public ToolExecutionLog PostQueueExecution(ToolExecutionLog item)
        {
            using (FRMContext context = new())
            {
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
#pragma warning disable CS8604 // Possible null reference argument.
                        StorageAccountHelper.WriteContentToBlob(containerClient, item.GUID.ToString(), "request.json", item.RunConfiguration);
#pragma warning restore CS8604 // Possible null reference argument.
                    }

                    // Put execution message on queue
                    QueueServiceClient queueServiceClient = StorageAccountHelper.GetQueueServiceClient();
                    string? queue = Environment.GetEnvironmentVariable("storage-account-tool-queue");
                    if (queue != null)
                    {
                        QueueClient queueClient = StorageAccountHelper.GetQueueClient(queueServiceClient, queue);
#pragma warning disable CS8604 // Possible null reference argument.
                        queueClient.SendMessage(Base64Encode(item.GUID.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
                    }
                }
            }

            return item;
        }
    }
}
