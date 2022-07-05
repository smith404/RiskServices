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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "GetToolStepItems")]
        public ToolStep? Get(long id)
        {
            using (FRMContext context = new())
            {
                ToolStep item;

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
                    ToolStep? tempItem = context.ToolSteps.Find(id);
                    if (tempItem == null)
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
                        item = tempItem;
                    }
                }

                return item;
            }
        }
    }
}
