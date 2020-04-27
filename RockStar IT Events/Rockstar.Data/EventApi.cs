using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rockstar.Models;

namespace Rockstar.Data
{
    public class EventApi
    {
        private WebRequest webRequest;
        private WebResponse webResponse;
        private StreamReader streamReader;

        public List<Event> GetAllEvents()
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events");

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string webResponseString = streamReader.ReadToEnd();
            List<Event> events = JsonConvert.DeserializeObject<List<Event>>(webResponseString);

            return events;
        }

        public Event GetEvent(string id)
        {
            webRequest = WebRequest.Create("https://eventhandler-api.herokuapp.com/api/events/" + id);

            webResponse = webRequest.GetResponse();
            streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string json = streamReader.ReadToEnd();

            Event output = JsonConvert.DeserializeObject<Event>(json);

            return output;
        }

        public async Task Create(Event e, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(e);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/events";
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.PostAsync(url, data);

            checkResponse(response.StatusCode);
        }

        public async Task Delete(string id, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/events/" + id;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.DeleteAsync(url);
        }

        public async Task Update(Event updatedEvent, string cookieValue)
        {
            var json = JsonConvert.SerializeObject(updatedEvent);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/events/" + updatedEvent.id;
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

            var response = await client.PutAsync(url, data);

            checkResponse(response.StatusCode);
        }

        private void checkResponse(HttpStatusCode code)
        {
            if (code == HttpStatusCode.OK)
            {
                throw new ArgumentException($"Something went wrong : {(int)code}");
            }
        }
    }
}
