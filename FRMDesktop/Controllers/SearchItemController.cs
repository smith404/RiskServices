using Microsoft.AspNetCore.Mvc;
using FRMObjects.model;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchItemController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public SearchItemController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSearchableItems")]
        public IEnumerable<SearchableItem> Get()
        {
            return Enumerable.Range(1, 1).Select(index => new SearchableItem
            {
                Id = 11,
                Title = "Best Ever Home Page",
                KeyWords = "The Best Ever Home Page",
                Summary = "The Best Ever Home Page",
                TargetType = "Page",
                Target = "www.google.com"
            })
            .ToArray();
        }
    }
}
