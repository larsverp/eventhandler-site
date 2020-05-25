using System;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class AdminController : Controller
    {
        private readonly EventApi eventApi;
        private readonly HostApi hostApi;
        private readonly UserApi userApi;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;


        public AdminController(IHttpContextAccessor contextAccessor, IWebHostEnvironment e)
        {
            eventApi = new EventApi();
            hostApi = new HostApi();
            userApi = new UserApi();
            webHostEnvironment = e;
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
                    string uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Picture.FileName)}";
                    var fileName = Path.Combine(webHostEnvironment.WebRootPath, uniqueImageName);
                    model.Picture.CopyTo(new FileStream(fileName, FileMode.Create));

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
                        thumbnail = "https://localhost:44324/" + uniqueImageName,
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
                string uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Picture.FileName)}";
                var fileName = Path.Combine(webHostEnvironment.WebRootPath, uniqueImageName);
                model.Picture.CopyTo(new FileStream(fileName, FileMode.Create));
                var e = new Event
                {
                    title = model.Title,
                    description = model.Description,
                    begin_date= model.StartDate.ToString("dd-MM-yyyy hh:mm:ss"),
                    end_date = model.EndDate.ToString("dd-MM-yyyy hh:mm:ss"),
                    thumbnail = "https://localhost:44324/" + uniqueImageName,
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

        public async Task<IActionResult> GetUsers()
        {
            var users = await userApi.ReturnAllUsers(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);

            return View(users);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                await userApi.RemoveUser(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
                return RedirectToAction("GetUsers");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        public IActionResult EditUser()
        {
            return View(new Rockstar.Models.User { email = "test3", first_name = "jan", last_name = "jansen", postal_code = "1244AB" });
        }

        public IActionResult Tickets()
        {
            return View();
        }

        public IActionResult MailingList()
        {
            return View();
        }
    }
}