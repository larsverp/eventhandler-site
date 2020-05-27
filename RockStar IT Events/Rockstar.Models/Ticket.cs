namespace Rockstar.Models
{
    public class Ticket
    {
        public string id { get; set; }
        public string event_id { get; set; }
        public string user_id { get; set; }
        public bool scanned { get; set; }
        public bool unsubscribe { get; set; }
        public string reason { get; set; }
    }
}