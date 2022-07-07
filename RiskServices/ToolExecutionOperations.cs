using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FRMObjects.model;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using FRMObjects;

namespace RiskServices
{
    public static class ToolExecutionOperations
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [FunctionName("ToolExecutionOperations")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ToolExecution function called for: " + req.Method);

            try
            {
                using (FRMContext context = new())
                {
                    if (req.Method == HttpMethods.Get)
                    {
                        string eid = req.Query["eid"];
                        long.TryParse(eid, out long id);
                        ToolExecutionLog item = await context.ToolExecutionLogs.FindAsync(id);

                        OkObjectResult result = new OkObjectResult(item);
                        result.ContentTypes.Add("application/json");
                        return result;
                    }
                    else if (req.Method == HttpMethods.Post)
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                        ToolExecutionLog item = JsonConvert.DeserializeObject<ToolExecutionLog>(requestBody);
                        context.ToolExecutionLogs.Add(item);
                        int count = await context.SaveChangesAsync();

                        // We should have inserted one line
                        if (count == 1)
                        {
                            // Create the blob
                            BlobServiceClient blobServiceClient = StorageAccountHelper.GetBlobServiceClient();
                            string container = Environment.GetEnvironmentVariable("storage-account-blob-container");
                            BlobContainerClient containerClient = StorageAccountHelper.GetBlobContainerClient(blobServiceClient, container);
                            await StorageAccountHelper.WriteContentToBlobAsync(containerClient, item.GUID.ToString(), "request.json", item.RunConfiguration);

                            // Put execution message on queue
                            QueueServiceClient queueServiceClient = StorageAccountHelper.GetQueueServiceClient();
                            string queue = Environment.GetEnvironmentVariable("storage-account-tool-queue");
                            QueueClient queueClient = StorageAccountHelper.GetQueueClient(queueServiceClient, queue);
                            queueClient.SendMessage(Base64Encode(item.GUID.ToString()));
                        }

                        OkObjectResult result = new OkObjectResult(item);
                        result.ContentTypes.Add("application/json");
                        return result;
                    }
                    else if (req.Method == HttpMethods.Delete)
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                        ToolExecutionLog item = JsonConvert.DeserializeObject<ToolExecutionLog>(requestBody);
                        context.ToolExecutionLogs.Remove(item);
                        int count = await context.SaveChangesAsync();

                        return new OkObjectResult("Rows affected: " + count); ;
                    }
                    else
                    {
                        return new BadRequestObjectResult("Unsupported HTTP Verb");
                    }
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
