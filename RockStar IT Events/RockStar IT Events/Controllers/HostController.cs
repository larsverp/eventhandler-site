using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;

namespace RockStar_IT_Events.Controllers
{
    [AdminCheckFilter]
    public class HostController : Controller
    {
        private readonly HostApi hostApi;
        private readonly string BearerTokenInCookie;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HostController(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor, IWebHostEnvironment e)
        {
            hostApi = new HostApi(clientFactory.CreateClient("event-handler"));
            BearerTokenInCookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
            webHostEnvironment = e;
        }

        public async Task<IActionResult> Details(string id)
        {
            Host host = await hostApi.GetHost(id);
            return View(host);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Picture.FileName)}";
                    var fileName = Path.Combine(webHostEnvironment.WebRootPath, uniqueImageName);
                    model.Picture.CopyTo(new FileStream(fileName, FileMode.Create));

                    var host = new Host
                    {
                        first_name = model.FirstName,
                        last_name = model.LastName,
                        description = model.Description,
                        picture = "https://teameventhandler.azurewebsites.net/" + uniqueImageName
                    };
                    await hostApi.CreateHost(host, BearerTokenInCookie);
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