using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var DAL = new DataLayer();
                string token = await DAL.GetBearerToken(model.username, model.password);
                if (token == null)
                {
                    ModelState.AddModelError("", "Incorrect username-password combination");
                    return View();
                }
                HttpContext.Session.SetString("BearerToken", token);
                return RedirectToAction("Index", "Event");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterModel model)
        {
            return View();
        }

        private async void AddCookies(string username, string password, CookieOptions options)
        {
            //Response.Cookies.Append("test", "test", options);
            DataLayer dataLayer = new DataLayer();
            Task<string> task = dataLayer.GetBearerToken(username, password);
            Response.Cookies.Append("test", "test", options);
            string value = await task;

            var client = new HttpClient();
            CookieOptions oo = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("sadf", "asdf", oo);
            //Response.Cookies.Append("test", value, options);
            }
    }
}