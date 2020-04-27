using System;

namespace Rockstar.Models
{
    public class Event
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public string date { get; set; }

        public string thumbnail { get; set; }

        public int seats { get; set; }
        public string postal_code { get; set; }

        public string hnum { get; set; }

        public bool notification { get; set; }
    }
}
