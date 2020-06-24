using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        private readonly UserApi userApi;
        private readonly HostApi hostApi;

        public UserController(IHttpClientFactory clientFactory)
        {
            userApi = new UserApi(clientFactory.CreateClient("event-handler"));
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
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
                try
                {
                    var token = await userApi.Login(model.EmailAddress, model.Password);
                    if (token == null)
                    {
                        ModelState.AddModelError("", "Incorrect EmailAddress-Password combination");
                        return View();
                    }

                    CookieOptions options = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1),
                        Secure = true,
                        HttpOnly = true,
                        IsEssential = true
                    };

                    Response.Cookies.Append("BearerToken", token, options);
                    var role = await userApi.GetRole(token);
                    HttpContext.Session.SetString("Role", role);
                    return RedirectToAction("Index", "Event");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
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
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User
                    {
                        first_name = model.FirstName,
                        email = model.EmailAddress,
                        insertion = model.Insertion,
                        last_name = model.LastName,
                        password = model.Password,
                        postal_code = model.PostalCode
                    };
                    await userApi.Signup(user);
                    return RedirectToAction("Validate", "User", new {user.email});
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["BearerToken"] != null)
            {
                Response.Cookies.Delete("BearerToken");
            }
            HttpContext.Session.Clear();
            return RedirectToAction("", "Event");
        }

        [HttpGet]
        public IActionResult Validate(string email)
        {
            ValidateViewModel model = new ValidateViewModel
            {
                Email = email
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Validate(ValidateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userApi.ValidateUser(model.Email, model.Code.ToString());
                    return RedirectToAction("Login");
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Onjuiste combinatie");
                }
            }
            return View();
        }

        public async Task<IActionResult> MyHosts()
        {
            var hosts = await hostApi.GetFollowingHosts(HttpContext.Request.Cookies["BearerToken"]);
            return View(hosts);
        }

        public IActionResult Host()
        {
            Host host = new Host();

            host.first_name = "Jan";
            host.last_name = "Jansen";
            host.description = "orem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.";
            host.picture = "https://c8.alamy.com/comp/CRJ923/young-customer-service-man-with-brace-doing-the-ok-sign-CRJ923.jpg";

            return View(host);
        }
    }
}