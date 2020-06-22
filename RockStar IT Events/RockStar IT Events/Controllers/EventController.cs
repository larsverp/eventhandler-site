using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RockStar_IT_Events.ViewModels;
using Event = Rockstar.Models;
using Rockstar.Models;

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

            var eEvent = eventApi.GetEvent(model.Event.id);
            model = new UnsubscribeEvent
            {
                Event = eEvent
            };

            return View(model);
        }

        public IActionResult RateEvent()
        {
            var eEvent = new Event.Event();

            eEvent.title = "TestEvent";
            eEvent.description = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.";
            eEvent.thumbnail = "https://c8.alamy.com/comp/EPF1YW/nun-with-handgun-isolated-on-white-EPF1YW.jpg";
            eEvent.host_id = "1";

            return View(eEvent);
        }
    }
}