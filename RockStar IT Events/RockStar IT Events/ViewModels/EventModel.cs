using System;
using System.ComponentModel.DataAnnotations;

namespace RockStar_IT_Events.ViewModels
{
    public class EventModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title must be entered")]
        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description must be entered")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Start of the Event")]
        [Required(ErrorMessage = "Value for start of the event is invalid")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End of the Event")]
        [Required(ErrorMessage = "Value for end of the event is invalid")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Thumbnail")]
        [Required(ErrorMessage = "Thumbnail must be entered")]
        [DataType(DataType.ImageUrl)]
        public string Thumbnail { get; set; }

        [Display(Name = "Total seats")]
        [Required(ErrorMessage = "Total seats must be entered")]
        public int TotalSeats { get; set; }

        [Display(Name = "Postal code")]
        [Required(ErrorMessage = "Postal code must be entered")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Display(Name = "House number")]
        [Required(ErrorMessage = "House number must be entered")]
        public string HouseNumber { get; set; }

        [Display(Name = "Send notifications")]
        [Required(ErrorMessage = "Notifications must be entered")]
        public bool SendNotifications { get; set; }
    }
}
