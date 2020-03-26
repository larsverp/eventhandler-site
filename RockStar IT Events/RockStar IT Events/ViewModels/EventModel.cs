using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.ViewModels
{
    public class EventModel
    {
        [Required(ErrorMessage = "Title must be entered")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must be entered")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date must be entered")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Thumbnail must be entered")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Total seats must be entered")]
        public int TotalSeats { get; set; }

        [Required(ErrorMessage = "Postal code must be entered")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "House number must be entered")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Notifications must be entered")]
        public bool SendNotifications { get; set; }
    }
}
