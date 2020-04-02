using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {

        public string userName = "test@test.com";
        public string passWord = "password";
        public IActionResult Login()
        {
            return View();
        }
    }
}