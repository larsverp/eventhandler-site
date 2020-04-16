using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.ViewModels;
using RockStar_IT_Events.Controllers;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {

        public string userName = "test@test.com";
        public string passWord = "password";

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                CookieController cookieController = new CookieController();
                CookieController.GetAccessToken(model.username, model.password);
                return RedirectToAction("Index", "Event");
            }

            return View();
        }
    }
}