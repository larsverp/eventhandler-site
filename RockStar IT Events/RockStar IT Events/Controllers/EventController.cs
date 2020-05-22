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

        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi();
            hostApi = new HostApi();
            ticketsApi = new TicketsApi();
        }

        public IActionResult Index()
        {
            List<Event.Event> events = eventApi.GetAllEvents();

            return View(events);
        }

        public IActionResult Event(string id)
        {
            var e = eventApi.GetEvent(id);
            var host = hostApi.GetHost(e.host_id);

            var model = new IndividualEventModel
            {
                eEvent = e,
                Host = host
            };
            return View(model);
        }

        public async Task<IActionResult> FavoriteEvents()
        {
            var events = await eventApi.ReturnAllFavoriteEvents(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);

            return View(events);
        }

        public async Task<IActionResult> AddEventToFavorites(string id)
        {
            await eventApi.AddEventToFavorites(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return RedirectToAction("Event", "Event", new { id = id.ToString()});
        }

        public async Task<IActionResult> RemoveEventFromFavorites(string id)
        {
            await eventApi.RemoveEventFromFavorites(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return RedirectToAction("Event", "Event", new { id = id });
        }

        public async Task<IActionResult> SubscribeForEvent(string id)
        {
            await ticketsApi.SubscribeForEvent(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return RedirectToAction("Event", "Event", new { id = id });
        }

        public async Task<IActionResult> UnsubscribeForEvent(string id)
        {
            await ticketsApi.UnsubscribeForEvent(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            return RedirectToAction("Event", "Event", new { id = id });
        }
    }
}