using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.ViewModels;
using RockStar_IT_Events.Controllers;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        //Will be used to login, still needs to be implemented.
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
    }
}