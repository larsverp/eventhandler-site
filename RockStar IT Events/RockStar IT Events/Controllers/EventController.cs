using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Event(int id)
        {
            return View();
        }

        public IActionResult Manage()
        {
            //check if admin is logged in
            return View();
        }
    }
}