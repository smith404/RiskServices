using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

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
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                outputBuilder.Append(reader["Id"]);
                                outputBuilder.Append(": ");
                                outputBuilder.Append(reader["Name"]);
                                outputBuilder.AppendLine();
                            }

                        }

                    }
                }
                return new OkObjectResult(outputBuilder.ToString());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
