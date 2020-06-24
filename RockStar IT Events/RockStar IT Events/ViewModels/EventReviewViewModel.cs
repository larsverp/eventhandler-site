using Rockstar.Models;

namespace RockStar_IT_Events.ViewModels
{
    public class EventReviewViewModel
    {
        public Event Event { get; set; }
        public bool Star1 { get; set; }
        public bool Star2 { get; set; }
        public bool Star3 { get; set; }
        public bool Star4 { get; set; }
        public bool Star5 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}