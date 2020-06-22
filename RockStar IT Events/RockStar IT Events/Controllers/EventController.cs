using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using RockStar_IT_Events.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        public EventController(
            IHttpContextAccessor contextAccessor,
            IHttpClientFactory clientFactory)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi(clientFactory.CreateClient("event-handler"));
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
            ticketsApi = new TicketsApi(clientFactory.CreateClient("event-handler"));
            categoryApi = new CategoryApi(clientFactory.CreateClient("event-handler"));
        }

        public async Task<IActionResult> Index()
        {
            List<Event.Event> events = await eventApi.GetAllEvents();
            return View(events);
        }

        public async Task<IActionResult> Event(string id)
        {
            var e = await eventApi.GetEvent(id);
            var host = await hostApi.GetHost(e.host_id);
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
        }

        [HttpPost]
        public async Task<IActionResult> FollowHost(string id, string eventId)
        {
            await hostApi.FollowHost(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);

            return RedirectToAction("Event", "Event", new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFollowingHost(string id, string eventId)
        {
            await hostApi.UnfollowHost(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);

            return RedirectToAction("Event", "Event", new { id = eventId });
        }

        [HttpGet]
        public async Task<IActionResult> UnsubscribeForEvent(string id)
        {
            var eEvents = await eventApi.GetAllEvents();
            var eEvent = eEvents.FirstOrDefault(e => e.id == id);
            var model = new UnsubscribeEvent
            {
                Event = eEvent
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UnsubscribeForEvent(UnsubscribeEvent model)
        {
            if (ModelState.IsValid)
            {
                await ticketsApi.UnsubscribeForEvent(model.Event.id, model.Reason, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
                return RedirectToAction("Index", "Event");
            }

            var eEvent = await eventApi.GetEvent(model.Event.id);
            model = new UnsubscribeEvent
            {
                Event = eEvent
            };

            return View(model);
        }

        public async Task<IActionResult> RateEvent(string id)
        {
            var eEvent = await eventApi.GetEvent(id);
            return View(eEvent);
        }
    }
}