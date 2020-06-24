using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class HostController : Controller
    {
        private readonly HostApi hostApi;

        public HostController(IHttpClientFactory clientFactory)
        {
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
        }

        public async Task<IActionResult> Details(string id)
        {
            Host host = await hostApi.GetHost(id);
            return View(host);
        }
    }
}