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
                options.IsEssential = true;

                Response.Cookies.Append("BearerToken", token, options);

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
            try
            {
                Rockstar.Models.User user = new User()
                {
                    first_name = model.FirstName,
                    email = model.EmailAddress,
                    insertion = model.Insertion,
                    last_name = model.LastName,
                    password = model.Password,
                    postal_code = model.PostalCode
                };
                await userApi.Signup(user);
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["BearerToken"] != null)
            {
                Response.Cookies.Delete("BearerToken");
            }
            return RedirectToAction("", "Event");
        }
    }
}