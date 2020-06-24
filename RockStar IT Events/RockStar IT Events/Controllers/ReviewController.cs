using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class ReviewController : Controller
    {
        private readonly EventApi eventApi;
        private readonly ReviewApi reviewApi;
        private readonly string bearerTokenInCookie;

        public ReviewController(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            eventApi = new EventApi(clientFactory.CreateClient("event-handler"));
            reviewApi = new ReviewApi();
            bearerTokenInCookie = httpContextAccessor.HttpContext.Request.Cookies["BearerToken"];
        }

        [HttpGet]
        public async Task<IActionResult> Event(string id)
        {
            var eEvent = await eventApi.GetEvent(id);
            var model = new EventReviewViewModel
            {
                Event = eEvent
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Event(EventReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Review review = new Review
                    {
                        title = model.Title,
                        description = model.Description,
                        event_id = model.Event.id,
                        rating = GetRating(model)
                    };
                    await reviewApi.CreateReview(review, bearerTokenInCookie);
                    return RedirectToAction("Event", "Event", new {model.Event.id});
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

        private int GetRating(EventReviewViewModel model)
        {
            if (model.Star5)
                return 5;
            else if (model.Star4)
                return 4;
            else if (model.Star3)
                return 3;
            else if (model.Star2)
                return 2;
            else if (model.Star1)
                return 1;
            return 0;
        }
    }
}