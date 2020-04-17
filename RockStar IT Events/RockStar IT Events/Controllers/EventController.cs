using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.Models;
using RockStar_IT_Events.Controllers;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            DataLayer dataLayer = new DataLayer();
            var events = dataLayer.GetAllEvents().OrderBy(e => e.id).ToList();
            return View(events);
        }

        public IActionResult Event(string id)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventModel model)
        {
            if (ModelState.IsValid)
            {
                DataLayer dataLayer = new DataLayer();
                
                var e = new Event()
                {
                    title = model.Title,
                    description = model.Description,
                    date = "2020-10-10 12:12:12",
                    thumbnail = model.Thumbnail,
                    seats = model.TotalSeats,
                    postal_code = model.PostalCode,
                    hnum = model.HouseNumber,
                    notification = model.SendNotifications
                };

                dataLayer.Create(e);
            }

            return View();
        }
    }
}