using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using System.Collections.Generic;
using RockStar_IT_Events.ViewModels;
using Event = Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        private IHttpContextAccessor contextAccessor;
        private readonly EventApi eventApi;
        private readonly HostApi hostApi;
        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi();
            hostApi = new HostApi();
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
    }
}