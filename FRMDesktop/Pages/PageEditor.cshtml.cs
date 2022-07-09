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
        }

        public int PageId { get; set; }

        public void OnGet(int id)
        {
            PageId = id;
        }
    }
}
