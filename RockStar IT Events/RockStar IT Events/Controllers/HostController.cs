using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class HostController : Controller
    {
        private readonly HostApi hostApi;
        private readonly string BearerTokenInCookie;

        public HostController(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
        {
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
            BearerTokenInCookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
        }

        public async Task<IActionResult> Details(string id)
        {
            Host host = await hostApi.GetHost(id);
            return View(host);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Host model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await hostApi.CreateHost(model, BearerTokenInCookie);
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View();
        }
    }
}