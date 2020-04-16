<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.ViewModels;
>>>>>>> develop

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
<<<<<<< HEAD

        public string userName = "test@test.com";
        public string passWord = "password";
=======
        [HttpPost]
>>>>>>> develop
        public IActionResult Login()
        {
            return View();
        }
<<<<<<< HEAD
=======

        [HttpGet]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //hier cookies opslaan
                return RedirectToAction("Index", "Event");
            }

            return View();
        }
>>>>>>> develop
    }
}