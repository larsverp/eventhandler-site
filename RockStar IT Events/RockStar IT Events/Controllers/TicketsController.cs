using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using RockStar_IT_Events.ViewModels;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsApi ticketsApi;
        private readonly EventApi eventApi;
        private readonly IHttpContextAccessor contextAccessor;

        public TicketsController(IHttpContextAccessor contextAccessor,
            IHttpClientFactory clientFactory)
        {
            ticketsApi = new TicketsApi(clientFactory.CreateClient("event-handler"));
            eventApi = new EventApi(clientFactory.CreateClient("event-handler"));
            this.contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await ticketsApi.GetAllTickets(contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            var events = await eventApi.GetAllEvents();
            var models = tickets.Select(t => new TicketsIndexModel
            {
                Event = events.FirstOrDefault(e => e.id == t.event_id),
                Ticket = t
            });

            return View(models);
        }
    }
}