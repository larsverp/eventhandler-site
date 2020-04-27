﻿using System;
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
                EndDate = e.date,
                StartDate = e.date,
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
        public async Task<IActionResult> EditEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Event e = new Event()
                    {
                        id = model.Id,
                        title = model.Title,
                        description = model.Description,
                        date = model.StartDate,
                        hnum = model.HouseNumber,
                        notification = model.SendNotifications,
                        postal_code = model.PostalCode,
                        seats = model.TotalSeats,
                        thumbnail = model.Thumbnail
                    };

                    string cookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
                    await eventApi.Update(e, cookie);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return RedirectToAction("Index");
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
    }
}