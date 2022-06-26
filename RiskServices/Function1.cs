using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Data.SqlClient;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace RiskServices
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string queryString = data?.queryString;

                if (string.IsNullOrEmpty(queryString))
                {
                    return new BadRequestObjectResult("Please specify a query");
                }

                StringBuilder outputBuilder = new StringBuilder();

                using (SqlConnection connection = new SqlConnection(ConnectionBuilder.GetSQLConnectionString()))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        await command.ExecuteReaderAsync();
                    }

                    // Create a BlobServiceClient object which will be used to create a container client
                    BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionBuilder.GetStorageAccountConnectionString());

                    //Create a unique name for the container
                    string containerName = "quickstartblobs" + Guid.NewGuid().ToString();

                    // Create the container and return a container client object
                    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

                    // Create a local file in the ./data/ directory for uploading and downloading
                    string localPath = "./data/";
                    string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
                    string localFilePath = Path.Combine(localPath, fileName);

                    // Write text to the file
                    await File.WriteAllTextAsync(localFilePath, "Hello, World!");

                    // Get a reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);

                    outputBuilder.AppendLine("Uploading to Blob storage as blob: " + blobClient.Uri);

                    // Upload data from the local file
                    await blobClient.UploadAsync(localFilePath, true);

                    outputBuilder.AppendLine("Listing blobs...");

                    // List all blobs in the container
                    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                    {
                        outputBuilder.AppendLine("\t" + blobItem.Name);
                    }

                    outputBuilder.AppendLine("Listing containers...");

                    // List all blobs in the container
                    await foreach (BlobContainerItem blobContainer in blobServiceClient.GetBlobContainersAsync())
                    {
                        outputBuilder.AppendLine("\t" + blobContainer.Name);
                    }

                }

                return new OkObjectResult(outputBuilder.ToString());

            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
