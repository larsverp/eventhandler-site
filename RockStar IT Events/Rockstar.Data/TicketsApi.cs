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
    public class TicketsApi
    {
        private readonly HttpClient client;

        public TicketsApi(HttpClient httpClient)
        {
            client = httpClient;
        }

        public async Task SubscribeForEvent(string eventId, string bearerToken)
        {
            var jsonEventIdContainer = new Dictionary<string, string>
            {
                { "event_id", eventId }
            };

            string json = JsonConvert.SerializeObject(jsonEventIdContainer);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync("tickets", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task UnsubscribeForEvent(string eventId, string reason, string bearerToken)
        {
            var userDetails = new Dictionary<string, string>
            {
                { "reason", reason}
            };

            var json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.PostAsync($"tickets/unsubscribe/{eventId}", data))
            {
                if (response.IsSuccessStatusCode)
                    return;
                throw new ArgumentException(response.ReasonPhrase);
            }
        }

        public async Task<List<Ticket>> GetAllTickets(string bearerToken)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            using (HttpResponseMessage response = await client.GetAsync("tickets"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<Ticket>>();
                throw new ArgumentException(response.ReasonPhrase);
            }
        }
    }
}