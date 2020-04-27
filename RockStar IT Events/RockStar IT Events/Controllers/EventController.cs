using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using System.Collections.Generic;
using Event = Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        private IHttpContextAccessor contextAccessor;
        private readonly EventApi eventApi;
        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi();
        }
        public IActionResult Index()
        {
            List<Event.Event> events = eventApi.GetAllEvents();

            return View(events);
        }

        public IActionResult Event(string id)
        {
            var e = eventApi.GetEvent(id);
            return View(e);
        }
    }
}