using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using RockStar_IT_Events.Models;
using System.Threading.Tasks;

namespace RockStar_IT_Events
{
    public class DataLayer
    {
        private WebRequest webRequest;
        private WebResponse webResponse;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        
        public List<Event> GetAllEvents()
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events");

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(webResponseString);

            return events;
        }


        public Event GetEvent(int id)
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events/" + id);

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string json = streamReader.ReadToEnd();

            Event output = JsonConvert.DeserializeObject<Event>(json);

            return output;
        }

        public string GetAccessToken(/*string userName, string passWord*/)
        {
            string _api = "https://eventhandler-api.herokuapp.com/api/users/login";
            webRequest = WebRequest.Create(_api);
            webRequest.Method = "POST";
            
            streamWriter = new StreamWriter(webRequest.GetRequestStream(), Encoding.UTF8);
            string userName = "{username : test@test.com}";
            string passWord = "{password : password}";
            string jsonString = new JsonSerializer().Serialize();
            
            string requestData = JsonTe;
            
        }
    }
}
