using Rockstar.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RockStar_IT_Events
{
    public class EventHelperClass
    {
        public static async Task<bool> EventIsFavorited(string EventId, string cookieValue)
        {
            using (HttpClient client = new HttpClient())
            {
                EventApi api = new EventApi(client);
                var events = await api.ReturnAllFavoriteEvents(cookieValue);
                return events.FirstOrDefault(e => e.id == EventId) != null;
            }
        }

        public static async Task<bool> EventIsInTickets(string eventId, string cookieValue)
        {
            using (HttpClient client = new HttpClient())
            {
                TicketsApi api = new TicketsApi(client);
                var tickets = await api.GetAllTickets(cookieValue);
                tickets = tickets.Where(t => t.unsubscribe == false).ToList();
                return tickets.FirstOrDefault(t => t.event_id == eventId) != null;
            }
        }

        public static async Task<bool> IsFollowingHost(string hostId, string cookieValue)
        {
            using (HttpClient client = new HttpClient())
            {
                HostApi api = new HostApi(client);
                var hosts = await api.GetFollowingHosts(cookieValue);
                return hosts.FirstOrDefault(h => h.id == hostId) != null;
            }
        }
    }
}