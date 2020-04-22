using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RockStar_IT_Events.ViewModels
{
    public class EventModel
    {
        [Required(ErrorMessage = "Title must be entered")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description must be entered")]
        public string Description { get; set; }

        [Display(Name = "Start of the Event")]
        [Required(ErrorMessage = "Value for start of the event is invalid")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End of the Event")]
        [Required(ErrorMessage = "Value for end of the event is invalid")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Thumbnail")]
        [Required(ErrorMessage = "Thumbnail must be entered")]
        public string Thumbnail { get; set; }

        [Display(Name = "Total seats")]
        [Required(ErrorMessage = "Total seats must be entered")]
        public int TotalSeats { get; set; }

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "Postal code must be entered")]
        public string PostalCode { get; set; }

        [Display(Name = "House number")]
        [Required(ErrorMessage = "House number must be entered")]
        public string HouseNumber { get; set; }

        [Display(Name = "Send notifications")]
        [Required(ErrorMessage = "Notifications must be entered")]
        public bool SendNotifications { get; set; }
    }
}
