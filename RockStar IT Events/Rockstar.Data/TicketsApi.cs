using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rockstar.Models;

namespace Rockstar.Data
{
    public class TicketsApi
    {
        public async Task SubscribeForEvent(string eventId, string cookieValue)
        {
            string json = JsonConvert.SerializeObject(new EventIdClass { event_id = eventId });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/tickets";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PostAsync(url, data);

                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task UnsubscribeForEvent(string eventId, string cookieValue)
        {
            //TODO: ADD REASON
            var url = "https://eventhandler-api.herokuapp.com/api/tickets/" + eventId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task<List<Event>> GetAllTickets(string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/tickets";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.GetStringAsync(url);

                List<Event> events = JsonConvert.DeserializeObject<List<Event>>(response);
                return events;
            }
        }
    }
}