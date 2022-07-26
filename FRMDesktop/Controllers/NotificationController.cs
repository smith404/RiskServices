using FRMObjects;
using FRMObjects.model;
using Microsoft.AspNetCore.Mvc;

namespace FRMDesktop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public NotificationController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetNotifiationCount")]
        public int GetNotifiationCount(string user, bool processed = false)
        {
            using (FRP_LandingContext context = new())
            {
                int? count = context.Notifications?.Count(item => item.Owner == user && item.Processed == processed);

                if (count == null)
                {
                    return 0;
                }
                else
                {
                    return (int)count;
                }
            }
        }

        [HttpGet("GetNotifiations")]
        public List<Notification> GetNotifiations(string user, bool processed = false)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    return context.Notifications.Where(item => item.Owner == user && item.Processed == processed).ToList();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return new List<Notification>();
        }

        [HttpPut("ProcessNotifiation")]
        public List<Notification> ProcessNotifiation(Notification item)
        {
            try
            {
                using (FRP_LandingContext context = new())
                {
                    item.Processed = true;
                    item.ProcessedOn = DateTime.Now;
                    context.Notifications.Update(item);
                    int count = context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return new List<Notification>();
        }
    }
}
