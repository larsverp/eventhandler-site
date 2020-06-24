using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RockStar_IT_Events.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Name not entered")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description not entered")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Thumbnail not entered")]
        public IFormFile Thumbnail { get; set; }
    }
}