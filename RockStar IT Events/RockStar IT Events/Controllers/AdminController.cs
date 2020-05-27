using System;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class AdminController : Controller
    {
        private readonly EventApi eventApi;
        private readonly IHttpContextAccessor contextAccessor;

        public AdminController(IHttpContextAccessor contextAccessor)
        {
            eventApi = new EventApi();
            this.contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            List<Event> events = eventApi.GetAllEvents();
            return View(events);
        }

        [HttpGet]
        public IActionResult EditEvent(string id)
        {
            Event e = eventApi.GetEvent(id);
            EventModel model = new EventModel
            {
                Id = e.id,
                Description = e.description,
                EndDate = DateTime.Parse(e.end_date),
                StartDate = DateTime.Parse(e.begin_date),
                HouseNumber = e.hnum,
                PostalCode = e.postal_code,
                SendNotifications = e.notification,
                Thumbnail = e.thumbnail,
                Title = e.title,
                TotalSeats = e.seats
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditEvent(EventModel e)
        {
            if (ModelState.IsValid)
            {
                Event ev = new Event();
                try
                {
<<<<<<< HEAD
                    ev.title = e.Title;
                    ev.description = e.Description;
                    ev.date = e.StartDate.ToString("dd-MM-yyyy") + "00:00:00";
                    ev.thumbnail = e.Thumbnail;
                    ev.seats = e.TotalSeats;
                    ev.postal_code = e.PostalCode;
                    ev.hnum = e.HouseNumber;
                    ev.notification = e.SendNotifications;
=======
                    Event e = new Event()
                    {
                        id = model.Id,
                        title = model.Title,
                        description = model.Description,
                        begin_date= model.StartDate.ToString("dd-MM-yyyy")+ "00:00:00",
                        end_date = model.EndDate.ToString("dd-MM-yyyy") + "00:00:00",
                        hnum = model.HouseNumber,
                        notification = model.SendNotifications,
                        postal_code = model.PostalCode,
                        seats = model.TotalSeats,
                        thumbnail = model.Thumbnail
                    };

>>>>>>> 09432a88c91ce4f4b49649bf11433ae1027e6793
                    string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                    await eventApi.Update(ev, cookie);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteEvent(string id)
        {
            try
            {
                string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                await eventApi.Delete(id, cookie);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            if (contextAccessor.HttpContext.Request.Cookies["BearerToken"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                var e = new Event
                {
                    title = model.Title,
                    description = model.Description,
                    begin_date= model.StartDate.ToString("dd-MM-yyyy") + "00:00:00",
                    end_date = model.EndDate.ToString("dd-MM-yyyy") + "00:00:00",
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

        [HttpGet]
        public IActionResult GetPastEvents()
        {
            var AllEvents = eventApi.GetAllEvents();
            List<EventModel> PastEventModels = new List<EventModel>();
            foreach (var Event in AllEvents)
            {
                if(DateTime.Now > Convert.ToDateTime(Event.date)){
                    EventModel model = new EventModel
                    {
                        Id = Event.id,
                        Description = Event.description,
                        EndDate = DateTime.Parse(Event.date),
                        StartDate = DateTime.Parse(Event.date),
                        HouseNumber = Event.hnum,
                        PostalCode = Event.postal_code,
                        SendNotifications = Event.notification,
                        Thumbnail = Event.thumbnail,
                        Title = Event.title,
                        TotalSeats = Event.seats
                    };
                    PastEventModels.Add(model);
                }
            }
            return View(PastEventModels);
        }
    }
}