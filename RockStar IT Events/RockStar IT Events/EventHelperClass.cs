using System;
using Rockstar.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RockStar_IT_Events
{
    public class EventHelperClass
    {
        public static async Task<bool> EventIsFavorited(string EventId, string cookieValue)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://eh-api.larsvanerp.com/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (client)
            {
                EventApi api = new EventApi(client);
                var events = await api.ReturnAllFavoriteEvents(cookieValue);
                return events.FirstOrDefault(e => e.id == EventId) != null;
            }
        }

        public static async Task<bool> EventIsInTickets(string eventId, string cookieValue)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://eh-api.larsvanerp.com/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (client)
            {
                TicketsApi api = new TicketsApi(client);
                var tickets = await api.GetAllTickets(cookieValue);
                tickets = tickets.Where(t => t.unsubscribe == false).ToList();
                bool eventIsInTickets = tickets.FirstOrDefault(t => t.event_id == eventId) != null;
                return eventIsInTickets;
            }
        }

        public static async Task<bool> IsFollowingHost(string hostId, string cookieValue)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://eh-api.larsvanerp.com/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (client)
            {
                HostApi api = new HostApi(client);
                var hosts = await api.GetFollowingHosts(cookieValue);
                bool isFollowingThisHost = hosts.FirstOrDefault(h => h.id == hostId) != null;
                return isFollowingThisHost;
            }
        }
    }
}