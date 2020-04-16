using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RockStar_IT_Events.Models;

namespace RockStar_IT_Events
{
    public class DataLayer
    {
        public List<Event> GetAllEvents()
        {
            WebRequest request = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events");

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseString = reader.ReadToEnd();
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(responseString);

            return events;
        }


        public Event GetEvent(int id)
        {
            WebRequest request = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events/" + id);

            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string json = reader.ReadToEnd();

            Event output = JsonConvert.DeserializeObject<Event>(json);

            return output;
        }
    }
}
