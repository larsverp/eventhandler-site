using System.Collections.Generic;
using Rockstar.Models;

namespace RockStar_IT_Events.ViewModels
{
    public class IndividualEventModel
    {
        public Event eEvent { get; set; }

        public Host Host { get; set; }

        public List<Category> Categories { get; set; }

        public List<Review> Reviews { get; set; }
    }
}