using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FRMDesktop.Pages
{
    public class PageEditorModel : PageModel
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public PageEditorModel(IConfiguration configuration)
        {
            Configuration = configuration;
            PageId = "";
        }

        public string PageId { get; set; }

        public void OnGet(string guid)
        {
            PageId = guid;
        }
    }
}
