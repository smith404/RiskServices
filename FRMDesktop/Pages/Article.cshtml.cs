using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FRMDesktop.Pages
{
    public class ArticleModel : PageModel
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public ArticleModel(IConfiguration configuration)
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
