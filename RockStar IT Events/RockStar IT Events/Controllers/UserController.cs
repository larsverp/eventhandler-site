using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        private readonly UserApi userApi;

        public UserController(IHttpClientFactory clientFactory)
        {
            userApi = new UserApi(clientFactory.CreateClient("event-handler"));    
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
                    var token = await userApi.Login(model.username, model.password);
                    if (token == null)
                    {
                        ModelState.AddModelError("", "Incorrect username-password combination");
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
                    return RedirectToAction("Validate", "User", new {email = user.email});
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
                catch(Exception e)
                {
                    ModelState.AddModelError("", "Onjuiste combinatie");
                }
            }

            return View();
        }


        public IActionResult MyHosts()
        {
            List<Host> hosts = new List<Host>();


            var host = new Host()
            {
                first_name = "jan",
                last_name = "jansen",
                description = "Jan Jansen was de zoon van een verkoopleider van de kinderschoenenfabriek Nimco en wilde van jongs af aan schoenontwerper worden. Tijdens zijn militaire dienst werkte hij op zaterdagen in een schoenenzaak in Nijmegen. Daar sneed hij toen schoenen open om te zien hoe ze technisch in elkaar zaten. Daarna ging hij stage lopen bij een schoenenfabriek in Brabant, leerde tekenen op de avondopleiding van de kunstacademie in Eindhoven en ging naar de schoenmakersvakschool in Waalwijk. Voor zijn (aanstaande) vrouw en muze Tonny maakt Jansen in 1959 zijn eerste schoen en in 1961 ontwerpt hij de 'Me' voor zichzelf. In 1962 ging hij een half jaar naar een schoenatelier in Rome om te leren hoe schoenen met de hand gemaakt moesten worden.",
                picture = "https://www.nieuws030.nl/uploads/resized/12/mei2018_024-jansen-1.jpg"
            };
            
             hosts.Add(host);

            host = new Host()
            {
                first_name = "Frank",
                last_name = "Lammers",
                description = "Lammers begon zijn carrière op het Strabrecht College te Geldrop. In 1995 studeerde Lammers af aan de toneelschool van Amsterdam, waarna hij in 1998 de korte film Kort Rotterdams: Temper, Temper schreef en regisseerde. In deze film speelden zijn goede vriend Fedja van Huêt en de toen nog onbekende Halina Reijn en Monic Hendrickx. Lammers maakte in datzelfde jaar zijn debuut in de Route 2000-film fl 19,99 in een korte rol als dakloze. In 2000 kreeg hij een grotere rol in de film Wilde Mossels.",
                picture = "https://media.nu.nl/m/fs4xymka0pij_wd1280.jpg/frank-lammers-streamt-toneelstuk-over-karl-marx-live-via-facebook.jpg"
            };
            
             hosts.Add(host);

            host = new Host()
            {
                first_name = "Patty",
                last_name = "Brard",
                description = "Na de havo (Lodewijk Makeblijde College) volgde Brard een opleiding directiesecretaresse bij Schoevers. Korte tijd later kreeg ze een baan als secretaresse aangeboden bij de publieke omroep. Hier werd ze in 1977 ontdekt door Hans van Hemert en gevraagd voor de nieuw op te richten en uit drie zangeressen bestaande meidengroep Luv', waarmee ze meerdere nationale hits had. In 1980 stapte ze uit de groep. In 1981 scoorde ze een bescheiden hitje met haar eerste solo-lp All this way. In 1982 bracht ze een tweede lp uit, maar dit werd geen succes. In 1985 bracht ze nog een lp uit.In 2003 richtte Brard een nieuwe meidengroep op,Enuv,met de voormalige Frogettes Anouskha de Wolff en Marian van Noort.Het werd geen succes en de band ging al na een paar maanden uit elkaar.",
                picture = "https://upload.wikimedia.org/wikipedia/commons/3/32/Patty_Brard2.jpg"
            };
            
             hosts.Add(host);
            
            return View(hosts);
        }
    }
}