using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Data;
using Rockstar.Models;
using RockStar_IT_Events.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Controllers
{
    [AdminCheckFilter]
    public class CategoryController : Controller
    {
        private readonly string BearerTokenInCookie;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly CategoryApi categoryApi;

        public CategoryController(IHttpContextAccessor contextAccessor, IWebHostEnvironment e, IHttpClientFactory clientFactory)
        {
            BearerTokenInCookie = contextAccessor.HttpContext.Request.Cookies["BearerToken"];
            webHostEnvironment = e;
            categoryApi = new CategoryApi(clientFactory.CreateClient("event-handler"));
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueImageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Thumbnail.FileName)}";
                    var fileName = Path.Combine(webHostEnvironment.WebRootPath, uniqueImageName);
                    model.Thumbnail.CopyTo(new FileStream(fileName, FileMode.Create));

                    Category category = new Category
                    {
                        name = model.Name,
                        description = model.Description,
                        thumbnail = "https://teameventhandler.azurewebsites.net/" + uniqueImageName,
                    };
                    await categoryApi.CreateCategory(category, BearerTokenInCookie);
                    return RedirectToAction("Index", "Admin");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View();
        }

        public async Task<IActionResult> Categories()
        {
            var allCategories = await categoryApi.GetAllCategories();
            return View(allCategories);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await categoryApi.RemoveCategory(id, BearerTokenInCookie);
            return RedirectToAction("Categories", "Category");
        }
    }
}