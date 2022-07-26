using FRMObjects;
using FRMObjects.model;
using Microsoft.AspNetCore.Mvc;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("NewSearchItem")]
        public SearchItem? NewSearchItem()
        {
            return new();
        }

        [HttpGet("SearchItem")]
        public SearchItem? GetSearchItems(long id)
        {
            SearchItem? item;

            if (id == -1)
            {
                item = new();
            }
            else
            {
                using (FRP_LandingContext context = new())
                {
                    item = context.SearchItems.Find(id);
                }
            }

            return item;
        }

        [HttpGet("AllSearchItems")]
        public List<SearchItem> GetSearchItems()
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    return context.SearchItems.ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return new();
        }

        [HttpGet("SearchItems")]
        public List<SearchItem> GetSearchItems(string domain)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    return context.SearchItems.Where(item => item.Domain == domain).ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return new List<SearchItem>();
        }

        [HttpPost("SearchItem")]
        public SearchItem PostSearchItems(SearchItem item)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    context.SearchItems.Add(item);
                    int count = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return item;
        }

        [HttpPut("SearchItem")]
        public SearchItem PutSearchItems(SearchItem item)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    context.SearchItems.Update(item);
                    int count = context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return item;
        }


    }
}
