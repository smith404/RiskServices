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

        [HttpGet("Page")]
        public HubPage? GetPage(string guid)
        {
            HubPage? page;

            if (guid.Equals("new"))
            {
                page = new();
            }
            else
            {
                using (FRP_LandingContext context = new())
                {
                    page = context.HubPages.Where(item => item.Guid == guid).SingleOrDefault();
                }
            }

            return page;
        }

        [HttpPut("Page")]
        public HubPage PutPage(HubPage page)
        {
            using (FRP_LandingContext context = new())
            {
                context.HubPages.Update(page);
                int count = context.SaveChanges();
            }

            return page;
        }

        [HttpPost("Page")]
        public HubPage PostPage(HubPage page)
        {
            using (FRP_LandingContext context = new())
            {
                if (page.Guid == null)
                {
                    page.Guid = Guid.NewGuid().ToString();
                }
                context.HubPages.Add(page);
                int count = context.SaveChanges();
            }

            return page;
        }

        [HttpDelete("Page")]
        public HubPage DeletePage(HubPage page)
        {
            using (FRP_LandingContext context = new())
            {
                context.HubPages.Remove(page);
                int count = context.SaveChanges();
            }

            return page;
        }

        [HttpGet("PageSections")]
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

        [HttpGet("NewSection")]
        public HubPageSection? GetNewSection(long id)
        {
            HubPageSection? section;

            section = new()
            {
                HubPageId = id
            };

            return section;
        }

        [HttpGet("PageSection")]
        public HubPageSection? GetPageSection(long id)
        {
            HubPageSection? section;

            if (id == -1)
            {
                section = new();
            }
            else
            {
                using (FRP_LandingContext context = new())
                {
                    section = context.HubPageSections.Find(id);
                }
            }

            return section;
        }

        [HttpPut("PageSection")]
        public HubPageSection PutPageSection(HubPageSection section)
        {
            using (FRP_LandingContext context = new())
            {
                context.HubPageSections.Update(section);
                int count = context.SaveChanges();
            }

            return section;
        }

        [HttpPost("PageSection")]
        public HubPageSection PostPageSection(HubPageSection section)
        {
            using (FRP_LandingContext context = new())
            {
                context.HubPageSections.Add(section);
                int count = context.SaveChanges();
            }

            return section;
        }

        [HttpDelete("PageSection")]
        public HubPageSection DeletePageSection(HubPageSection section)
        {
            using (FRP_LandingContext context = new())
            {
                context.HubPageSections.Remove(section);
                int count = context.SaveChanges();
            }

            return section;
        }
    }
}
