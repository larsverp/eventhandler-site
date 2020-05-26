using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using RockStar_IT_Events.ViewModels;
using Event = Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        private IHttpContextAccessor contextAccessor;
        private readonly EventApi eventApi;
        private readonly HostApi hostApi;
        private readonly TicketsApi ticketsApi;
        private readonly CategoryApi categoryApi;

        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi();
            hostApi = new HostApi();
            ticketsApi = new TicketsApi();
            categoryApi = new CategoryApi();
        }

        public async Task<IActionResult> Index()
        {
            List<Event.Event> events = await eventApi.GetAllEvents();

            return View(events);
        }

        public async Task<IActionResult> Event(string id)
        {
            var e = eventApi.GetEvent(id);
            var host = hostApi.GetHost(e.host_id);
            var category = await categoryApi.GetAllCategoriesFromEvent(id);

            var model = new IndividualEventModel
            {
                eEvent = e,
                Host = host,
                Categories = category
            };
            return View(model);
        }

        public async Task<IActionResult> FavoriteEvents()
        {
            var events = await eventApi.ReturnAllFavoriteEvents(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventToFavorites(string id)
        {
            await eventApi.AddEventToFavorites(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveEventFromFavorites(string id)
        {
            await eventApi.RemoveEventFromFavorites(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeForEvent(string id)
        {
            await ticketsApi.SubscribeForEvent(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return Ok();
            return RedirectToAction("Event", "Event", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> UnsubscribeForEvent(string id)
        {
            await ticketsApi.UnsubscribeForEvent(id,"", contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return RedirectToAction("Event", "Event", new { id = id });
        }
    }
}