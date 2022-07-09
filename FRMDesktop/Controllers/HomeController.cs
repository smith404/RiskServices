﻿using FRMObjects;
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

        [HttpGet("GetSearchItems")]
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

        [HttpPost("WriteSearchItem")]
        public SearchItem WriteSearchItems(SearchItem item)
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

        [HttpPut("UpdateSearchItem")]
        public SearchItem UpdateSearchItems(SearchItem item)
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
    }
}
