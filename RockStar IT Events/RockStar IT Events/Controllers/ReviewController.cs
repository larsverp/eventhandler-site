using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;

namespace RockStar_IT_Events.Controllers
{
    public class ReviewController : Controller
    {
        private readonly EventApi eventApi;
        private readonly ReviewApi reviewApi;

        public ReviewController(IHttpClientFactory clientFactory)
        {
            eventApi = new EventApi(clientFactory.CreateClient("event-handler"));
            reviewApi = new ReviewApi();
        }

        [HttpGet]
        public async Task<IActionResult> Event(string id)
        {
            var eEvent = await eventApi.GetEvent(id);
            return View(eEvent);
        }
    }
}