using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;
using System;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        private readonly UserApi userApi;
        public UserController()
        {
            userApi = new UserApi();    
        }

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
                string token = await userApi.Login(model.username, model.password);
                if (token == null)
                {
                    ModelState.AddModelError("", "Incorrect username-password combination");
                    return View();
                }
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);
                options.Secure = true;
                options.HttpOnly = true;

                Response.Cookies.Append("BearerToken", token);

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
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            Rockstar.Models.User user = new User()
            {
                first_name = "Ruben",
                email = "rubenfricke@gmail.com",
                insertion = "iets",
                last_name = "Fricke",
                password = "RubenFricke",
                postal_code = "5711BZ"
            };
            await userApi.Signup(user);
            return View();
        }
    }
}