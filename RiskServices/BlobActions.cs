using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RiskServices
{
    public static class BlobActions
    {
        [FunctionName("AddContainer")]
        public static async Task<IActionResult> AddContainer(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string containerName = data?.container;

                if (string.IsNullOrEmpty(containerName))
                {
                    return new BadRequestObjectResult("Please specify a container");
                }

                StringBuilder outputBuilder = new StringBuilder();

                BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionBuilder.GetStorageAccountConnectionString());

                // Create the container and return a container client object
                BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                return new OkObjectResult(outputBuilder.ToString());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [FunctionName("UploadBlob")]
        public static async Task<IActionResult> UploadBlob(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string containerName = data?.container;
                string filePath = data?.path;

                if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(filePath))
                {
                    return new BadRequestObjectResult("Please specify a container and file path");
                }

                StringBuilder outputBuilder = new StringBuilder();

                BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionBuilder.GetStorageAccountConnectionString());

                // Create the container and return a container client object
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Get a reference to a blob
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(filePath);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                outputBuilder.AppendLine("Uploading to Blob storage as blob: " + blobClient.Uri);

                // Upload data from the local file
                await blobClient.UploadAsync(filePath, true);

                return new OkObjectResult(outputBuilder.ToString());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
