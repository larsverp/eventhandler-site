using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.Controllers
{
    public class ErrorController : Controller
    {
        [Route("ErrorMessage/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            ErrorViewModel model = new ErrorViewModel();
            switch (statusCode)
            {
                case 404:
                    model.ErrorMessage = "YOU SEEM TO BE LOST";
                    break;
                default:
                    model.ErrorMessage = "SOMETHING WENT WRONG";
                    break;
            }
            return View(model);
        }
    }
}