using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FRMDesktop.Pages
{
    public class ToolStepDetailModel : PageModel
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public ToolStepDetailModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string? ServerRoot { get; set; }

        public int ItemId { get; set; }

        public void OnGet(int eid)
        {
            ItemId = eid;
            ServerRoot = Configuration["risk-services-root"];
        }
    }
}
