﻿using System.Collections.Generic;

namespace Rockstar.Models
{
    public class Event
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string host_id { get; set; }
        public string begin_date { get; set; }
        public string end_date { get; set; }
        public string thumbnail { get; set; }
        public int seats { get; set; }
        public int available_seats { get; set; }
        public string postal_code { get; set; }
        public string hnum { get; set; }
        public bool notification { get; set; }
        public bool rockstar { get; set;}
        public List<string> categories { get; set; }
        public string street { get; set; }
        public string city { get; set; }
    }
}
