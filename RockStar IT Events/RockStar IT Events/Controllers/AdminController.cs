using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class AdminController : Controller
    {
        private readonly EventApi eventApi;

        public AdminController()
        {
            eventApi = new EventApi();
        }

        public IActionResult Index()
        {
            List<Event> events = eventApi.GetAllEvents();
            return View(events);
        }
    }
}