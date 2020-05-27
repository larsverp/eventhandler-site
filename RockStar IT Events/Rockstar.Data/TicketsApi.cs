using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
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

        public async Task UnsubscribeForEvent(string eventId, string reason, string cookieValue)
        {
            var userDetails = new Dictionary<string, string>()
            {
                { "reason", reason}
            };

            var json = JsonConvert.SerializeObject(userDetails);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://eventhandler-api.herokuapp.com/api/tickets/unsubscribe/" + eventId;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.PostAsync(url, data);
                var x = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode == false)
                    throw new ArgumentException("Something went wrong");
            }
        }

        public async Task<List<Ticket>> GetAllTickets(string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/tickets";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);

                var response = await client.GetStringAsync(url);

                List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(response);
                return tickets;
            }
        }

        public async Task GetPdf(string eventId, string cookieValue)
        {
            var url = "https://eventhandler-api.herokuapp.com/api/download/ticket/" + eventId;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cookieValue);
            var response = await client.GetAsync(@url);

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var fileInfo = new FileInfo("myPackage.pdf");
                using (var fileStream = fileInfo.OpenWrite())
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
        }
    }
}