using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;

namespace RockStar_IT_Events.Controllers
{
    [AdminCheckFilter]
    public class AdminController : Controller
    {
        private readonly EventApi eventApi;
        private readonly HostApi hostApi;
        private readonly UserApi userApi;
        private readonly CategoryApi categoryApi;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string bearerTokenInCookie;

        public AdminController(
            IWebHostEnvironment e,
            IHttpClientFactory clientFactory,
            IHttpContextAccessor contextAccessor)
        {
            eventApi = new EventApi(clientFactory.CreateClient("event-handler"));
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
            userApi = new UserApi(clientFactory.CreateClient("event-handler"));
            categoryApi = new CategoryApi(clientFactory.CreateClient("event-handler"));
            webHostEnvironment = e;
            this.contextAccessor = contextAccessor;
            bearerTokenInCookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
        }

        public async Task<IActionResult> Index()
        {
            List<Event> events = await eventApi.GetAllEventsForRockstarAccount(bearerTokenInCookie);
            return View(events);
        }

        [HttpGet]
        public async Task<IActionResult> EditEvent(string id)
        {
            Event e = await eventApi.GetEvent(id);
            var hosts = await hostApi.GetAllHosts();
            var cats = await categoryApi.GetAllCategories();
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
                Speakers = hosts.ToList(),
                Categories = cats
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
                    string pathName;
                    if (model.Picture != null)
                    {
                        string uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Picture.FileName)}";
                        var fileName = Path.Combine(webHostEnvironment.WebRootPath, uniqueImageName);
                        model.Picture.CopyTo(new FileStream(fileName, FileMode.Create));
                        pathName = "https://teameventhandler.azurewebsites.net/" + uniqueImageName;
                    }
                    else
                    {
                        var events = await eventApi.GetAllEvents();
                        pathName = events.FirstOrDefault(e => e.id == model.Id).thumbnail;
                    }

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
                        thumbnail = pathName,
                        host_id = model.SpeakerId,
                        categories = model.CategoryId
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

            Event ev = await eventApi.GetEvent(model.Id);
            var hosts = await hostApi.GetAllHosts();
            var cats = await categoryApi.GetAllCategories();
            EventModel m = new EventModel
            {
                Id = ev.id,
                Title = ev.title,
                Description = ev.description,
                EndDate = DateTime.Parse(ev.end_date),
                StartDate = DateTime.Parse(ev.begin_date),
                Thumbnail = ev.thumbnail,
                TotalSeats = ev.seats,
                PostalCode = ev.postal_code,
                HouseNumber = ev.hnum,
                SendNotifications = ev.notification,
                SpeakerId = ev.host_id,
                Speakers = hosts.ToList(),
                Categories = cats
            };

            return View(m);
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
            var categories = await categoryApi.GetAllCategories();
            EventModel model = new EventModel
            {
                Speakers = hosts.ToList(),
                Categories = categories
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
                    thumbnail = "http://i436732core.venus.fhict.nl/" + uniqueImageName,
                    seats = model.TotalSeats,
                    postal_code = model.PostalCode,
                    hnum = model.HouseNumber,
                    notification = model.SendNotifications,
                    host_id = model.SpeakerId,
                    categories = model.CategoryId,
                    rockstar = model.Rockstar
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

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var users = await userApi.ReturnAllUsers(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            var user = users.FirstOrDefault(u => u.Id == id);
            var model = new UserWithoutPassword
            {
                email = user.email,
                first_name = user.first_name,
                Id = user.Id,
                insertion = user.insertion,
                last_name = user.last_name,
                postal_code = user.postal_code
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserWithoutPassword model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await userApi.UpdateUser(model, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
                    return RedirectToAction("GetUsers", "Admin");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            var users = await userApi.ReturnAllUsers(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            var user = users.FirstOrDefault(u => u.Id == model.Id);
            return View(user);
        }
    }
}