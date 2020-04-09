using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.Models;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            CookieController test = new CookieController();
            test.ReadCookie();
            DataLayer dataLayer = new DataLayer();
            var events = dataLayer.GetAllEvents().OrderBy(e => e.id).ToList();
            return View(events);
        }

        public IActionResult Event(int id)
        {
            DataLayer dataLayer = new DataLayer();
            var e = dataLayer.GetEvent(id);
            return View(e);
        }

        public IActionResult Manage()
        {
            //check if admin is logged in
            return Content("Manage page");
        }
    }
}