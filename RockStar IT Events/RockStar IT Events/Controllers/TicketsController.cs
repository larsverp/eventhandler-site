using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsApi ticketsApi;
        private readonly EventApi eventApi;
        private readonly IHttpContextAccessor contextAccessor;

        public TicketsController(IHttpContextAccessor contextAccessor)
        {
            ticketsApi = new TicketsApi();
            eventApi = new EventApi();
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

        public async Task<IActionResult> DownloadPdf(string id)
        {
            await ticketsApi.GetPdf(id, contextAccessor.HttpContext.Request.Cookies["BearerToken"]);
            //var stream = new FileStream(@url, FileMode.Open);
            return Ok();
            //return new FileStreamResult(stream, "application/pdf");
        }
    }
}