using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using RockStar_IT_Events.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Event = Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        private IHttpContextAccessor contextAccessor;
        private readonly EventApi eventApi;
        public EventController(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
            eventApi = new EventApi();
        }
        public IActionResult Index()
        {
            List<Event.Event> events = eventApi.GetAllEvents();

            return View(events);
        }

        public IActionResult Event(string id)
        {
            var e = eventApi.GetEvent(id);
            return View(e);
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
                var e = new Event.Event()
                {
                    title = model.Title,
                    description = model.Description,
                    date = model.StartDate.ToString(),
                    thumbnail = model.Thumbnail,
                    seats = model.TotalSeats,
                    postal_code = model.PostalCode,
                    hnum = model.HouseNumber,
                    notification = model.SendNotifications
                };
                
                string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                
                try
                {
                    await eventApi.Create(e, cookie);
                    return RedirectToAction("Index", "Event");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", exception.Message);
                }
            }

            return View();
        }

        public async Task<IActionResult> DeleteEvent(string id)
        {
            string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];

            try
            {
                await eventApi.Delete(id, cookie);
                return RedirectToAction("Index", "Event");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public async Task<IActionResult> Update(Event.Event updatedEvent)
        {
            string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
            try
            {
                await eventApi.Update(updatedEvent, cookie);
                return RedirectToAction("Index", "Event");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View();
        }
    }
}