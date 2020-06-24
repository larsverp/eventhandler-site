using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RockStar_IT_Events.ViewModels
{
    public class HostCreateViewModel
    {
        [Required(ErrorMessage = "Firstname is not entered")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is not entered")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Description is not entered")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Picture is not entered")]
        public IFormFile Picture { get; set; }
    }
}