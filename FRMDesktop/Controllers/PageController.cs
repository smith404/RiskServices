using FRMObjects;
using FRMObjects.model;
using Microsoft.AspNetCore.Mvc;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public PageController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetPage")]
        public HubPage? GetPage(long id)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    return context.HubPages.Find(id);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        [HttpGet("GetPageSections")]
        public List<HubPageSection>? GetPageSections(long id)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    return context.HubPageSections.Where(section => section.HubPageId == id).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
