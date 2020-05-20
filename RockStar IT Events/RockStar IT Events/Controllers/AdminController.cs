using System;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class AdminController : Controller
    {
        private readonly EventApi eventApi;
        private readonly HostApi hostApi;
        private readonly IHttpContextAccessor contextAccessor;

        public AdminController(IHttpContextAccessor contextAccessor)
        {
            eventApi = new EventApi();
            hostApi = new HostApi();
            this.contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            List<Event> events = eventApi.GetAllEvents();
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(string id)
        {
            Event e = eventApi.GetEvent(id);
            var hosts = await hostApi.GetAllHosts();
            EventModel model = new EventModel
            {
                Id = e.id,
                Title = e.title,
                Description = e.description,
                EndDate = DateTime.Parse(e.end_date),
                StartDate = DateTime.Parse(e.begin_date),
                Thumbnail = e.thumbnail,
                TotalSeats = e.seats,
                PostalCode = e.postal_code,
                HouseNumber = e.hnum,
                SendNotifications = e.notification,
                SpeakerId = e.host_id,
                Speakers = hosts.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Event e = new Event
                    {
                        id = model.Id,
                        title = model.Title,
                        description = model.Description,
                        begin_date= model.StartDate.ToString("dd-MM-yyyy hh:mm:ss"),
                        end_date = model.EndDate.ToString("dd-MM-yyyy hh:mm:ss"),
                        hnum = model.HouseNumber,
                        notification = model.SendNotifications,
                        postal_code = model.PostalCode,
                        seats = model.TotalSeats,
                        thumbnail = "https://images.unsplash.com/photo-1588615419957-bf66d53c6b49?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1267&q=80",
                        host_id = model.SpeakerId,
                        categories = new List<string> { "452f25f4-3339-4791-bc87-f157e913c771" }
                    };

                    string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                    await eventApi.Update(e, cookie);

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
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
        public async Task<IActionResult> CreateEvent()
        {
            if (contextAccessor.HttpContext.Request.Cookies["BearerToken"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            var hosts = await hostApi.GetAllHosts();
            EventModel model = new EventModel
            {
                Speakers = hosts.ToList()
            };

            return View(model);
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
                    begin_date= model.StartDate.ToString("dd-MM-yyyy hh:mm:ss"),
                    end_date = model.EndDate.ToString("dd-MM-yyyy hh:mm:ss"),
                    thumbnail = "https://images.unsplash.com/photo-1588615419957-bf66d53c6b49?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1267&q=80",
                    seats = model.TotalSeats,
                    postal_code = model.PostalCode,
                    hnum = model.HouseNumber,
                    notification = model.SendNotifications,
                    host_id = model.SpeakerId,
                    categories = new List<string> { "452f25f4-3339-4791-bc87-f157e913c771" },
                    
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

            var hosts = await hostApi.GetAllHosts();
            model = new EventModel
            {
                Speakers = hosts.ToList()
            };

            return View(model);
        }

        public IActionResult GetUsers()
        {
            List<User> users = new List<User>();
            users.Add(new Rockstar.Models.User { email = "test", first_name = "jan", last_name = "jansen", postal_code= "1244AB"}); ;
            users.Add(new Rockstar.Models.User { email = "test2", first_name = "jan", last_name = "jansen",  postal_code = "1244AB" }); ;
            users.Add(new Rockstar.Models.User { email = "test3", first_name = "jan", last_name = "jansen", insertion="van de", postal_code = "1244AB" }); ;

            return View(users);
        }

        public IActionResult EditUser()
        {
            return View(new Rockstar.Models.User { email = "test3", first_name = "jan", last_name = "jansen", postal_code = "1244AB" });
        }
    }
}