using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RockStar_IT_Events.Models;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class EventController : Controller
    {
        //Retreives all events.
        public IActionResult Index()
        {
            DataLayer dataLayer = new DataLayer();
            var events = dataLayer.GetAllEvents().OrderBy(e => e.id).ToList();
            return View(events);
        }

        //Shows the event with id #.
        public IActionResult Event(string id)
        {
            DataLayer dataLayer = new DataLayer();
            var e = dataLayer.GetEvent(id);
            return View(e);
        }

        //Checks if the admin is logged in.
        public IActionResult Create()
        {
            return View();
        }

        //Creates an event.
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
                string cookie = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMmQ2NjYyZDMxN2I3OTBkZmY5MjEyNGM5ZTZlZGIxMTZiYzljMGQyNjJmODQ0YjU0NzIwMGQ1OTQ1ZWQ0MTFhMGZiZmJhNTk5YzI4M2Q0MGUiLCJpYXQiOjE1ODc1NDUxNjQsIm5iZiI6MTU4NzU0NTE2NCwiZXhwIjoxNjE5MDgxMTY0LCJzdWIiOiIxIiwic2NvcGVzIjpbInJvY2tzdGFyIl19.g6IJif5fqcINC5 - Q8OF8VoqqrGMt - TzO7iYKxiTMsCWjTUwBuwyJi1lsvb0Y0SBcVypf6TW49QfhPvKyMSk6PIbUrHxzv3Q_VRO1vAc_DQSmGLKofNNp8HW - PnNY6782Nh6ruznHTAPeNpVrAd9CAqcl67MqQj9fS8IzTTabIaftjBCzqJMa2tlMact3mUsmeebdVvNzcOeYyL3Kxqg6AYPOwyHhlHuoG14a_z0qz6QI8mXz4vsDeXVH6IlZOgNScnpFe_8G - oIgfKtR33Ss7YSQvHuqnWyHfiDLwFwYRblINRngdQJP8KoyGqElqvaczw9VAniEUVbICnaiIAFGFb_LCPWSmHKi63yWkX8BJkY7Lk40UfvWCGKCcWm86PmCOjVFoeVf - eAT9 - 59eTL62OMxj7FejhsiJbRnnOlTOt5m8vlmKN2qNte0jOwVRFb--OvBaaJB0drGWuvXj8zYX9zHkvPD1Soi7ky1rT66XBDv07Xa5p5_Qw23LgijZSZr7kqrlPBFA - E2DcVz91X5XMU9jrJR5UC - pRB2PkgjEUMG - SRJyVnYBwmDKjOZo1roeK918hlrFyOz4U4QLGJcCiYpBXnKSdxbkpcltytCCQ4oQqXqLljW6JzInN086n4hGdWPAElB5wygeMDUrx3LkkFbITKdtrwXWTHfgHANiAk";

                dataLayer.Create(e, cookie);
            }
            return View();
        }
    }
}