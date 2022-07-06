using FRMObjects.model;
using Microsoft.AspNetCore.Mvc;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolStepController : Controller
    {
        private readonly ILogger<ToolStepController> _logger;

        public ToolStepController(ILogger<ToolStepController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetToolSteps")]
        public ToolStep? Get(long id)
        {
            using (FRMContext context = new())
            {
                ToolStep? item;

                // Special case of a get call for a new object
                if (id == -1)
                {
                    item = new ToolStep
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
                    item = context.ToolSteps.Find(id);
                }

                return item;
            }
        }
    }
}
