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

                StringBuilder outputBuilder = new StringBuilder(ConnectionBuilder.GetSQLConnectionString());

                string connectionString = ConnectionBuilder.GetSQLConnectionString();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.ExecuteNonQuery();
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
