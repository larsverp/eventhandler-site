using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.Models;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        private IHttpContextAccessor contextAccessor;
        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }
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
            if (contextAccessor.HttpContext.Request.Cookies["BearerToken"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventModel model)
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
                string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                try
                {
                    await dataLayer.Create(e, cookie);
                    return RedirectToAction("Index", "Event");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }

            return View();
        }

        public IActionResult adminpanel()
        {
            DataLayer layer = new DataLayer();
            List<Event> models = new List<Event>(layer.GetAllEvents());
            return View(models);
        }

        public async Task<IActionResult> DeleteEvent()
        {
            DataLayer dataLayer = new DataLayer();

            string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
            try
            {
                string id = "9e8ee412-78cd-478c-aae2-5e5b8c33e755";
                await dataLayer.Delete(id, cookie);
                return RedirectToAction("Index", "Event");
            }
            catch (Exception exception)
            {
                throw new ArgumentException("Error: " + exception.Message);
            }

            return RedirectToAction("Index", "Event");
        }

        public async Task<IActionResult> Update()
        {
            DataLayer dataLayer = new DataLayer();

            string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
            try
            {
                string id = "51a67d93-9647-4972-a383-5cc1ed5a68ed";
                await dataLayer.Update(id, cookie);
                return RedirectToAction("Index", "Event");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error: " + e.Message);
            }
        }
    }
}