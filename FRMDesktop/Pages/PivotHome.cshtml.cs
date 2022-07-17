using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FRMDesktop.Pages
{
    public class PivotHomeModel : PageModel
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public PivotHomeModel(IConfiguration configuration)
        {
            Configuration = configuration;
            ResultId = "";
        }

        public string ResultId { get; set; }

        public void OnGet(string guid)
        {
            ResultId = guid;
        }
    }
}
