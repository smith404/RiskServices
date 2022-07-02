using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RiskServices.model;

namespace RiskServices
{
    public static class ToolDefinitionOperations
    {
        [FunctionName("ToolDefinition")]
        public static async Task<IActionResult> ToolDefinitionCRUD(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", "put", "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("ToolDefinition function called for: " + req.Method);

            try
            {
                using (FRMContext context = new())
                {
                    if (req.Method == HttpMethods.Get)
                    {
                        string eid = req.Query["eid"];
                        long.TryParse(eid, out long id);
                        ToolDefinition item = await context.ToolDefinitions.FindAsync(id);

                        OkObjectResult result = new OkObjectResult(item);
                        result.ContentTypes.Add("application/json");
                        return result;
                    }
                    else if (req.Method == HttpMethods.Post)
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                        ToolDefinition item = JsonConvert.DeserializeObject<ToolDefinition>(requestBody);
                        context.ToolDefinitions.Add(item);
                        int count = await context.SaveChangesAsync();

                        OkObjectResult result = new OkObjectResult(item);
                        result.ContentTypes.Add("application/json");
                        return result;
                    }
                    else if (req.Method == HttpMethods.Put)
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                        ToolDefinition item = JsonConvert.DeserializeObject<ToolDefinition>(requestBody);
                        context.ToolDefinitions.Update(item);
                        int count = await context.SaveChangesAsync();

                        OkObjectResult result = new OkObjectResult(item);
                        result.ContentTypes.Add("application/json");
                        return result;
                    }
                    else if (req.Method == HttpMethods.Delete)
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                        ToolDefinition item = JsonConvert.DeserializeObject<ToolDefinition>(requestBody);
                        context.ToolDefinitions.Remove(item);
                        int count = await context.SaveChangesAsync();

                        return new OkObjectResult("Rows affected: " + count); ;
                    }
                    else
                    {
                        return new BadRequestObjectResult("Unsupported HTTP Verb");
                    }
                }
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
