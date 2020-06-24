using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class TinderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Succes()
        {
            return View();
        }
    }
}