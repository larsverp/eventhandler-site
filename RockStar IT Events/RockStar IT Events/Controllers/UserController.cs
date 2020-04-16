using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }

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
    }
}