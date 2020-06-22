using Newtonsoft.Json;
using Rockstar.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Rockstar.Data
{
    public class EventApi
    {
        private readonly HttpClient client;
        
        public EventApi(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task <List<Event>> GetAllEvents()
        {
            using (HttpResponseMessage response = await client.GetAsync("events/preview"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Event>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<Event> GetEvent(string id)
        {
            using (HttpResponseMessage response = await client.GetAsync($"events/{id}"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Event>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task Create(Event e, string bearerToken)
        {
            var json = JsonConvert.SerializeObject(e);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync("events", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task Delete(string eventId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.DeleteAsync($"events/{eventId}"))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task Update(Event updatedEvent, string bearerToken)
        {
            var json = JsonConvert.SerializeObject(updatedEvent);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PutAsync($"events/{updatedEvent.id}", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<Event>> ReturnAllFavoriteEvents(string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.GetAsync("favorites"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Event>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task AddEventToFavorites(string eventId, string bearerToken)
        {
            var jsonEventIdContainer = new Dictionary<string, string>
            {
                {"event_id", eventId} 
            };

            string json = JsonConvert.SerializeObject(jsonEventIdContainer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await this.client.PostAsync("favorites", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task RemoveEventFromFavorites(string eventId, string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await this.client.DeleteAsync($"favorites/{eventId}"))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }
    }
}
