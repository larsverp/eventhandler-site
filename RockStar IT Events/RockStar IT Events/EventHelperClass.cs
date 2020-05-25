using System.Linq;
using System.Threading.Tasks;
using Rockstar.Data;

namespace RockStar_IT_Events
{
    public class EventHelperClass
    {
        public static async Task<bool> EventIsFavorited(string EventId, string cookieValue)
        {
            EventApi api = new EventApi();
            var events = await api.ReturnAllFavoriteEvents(cookieValue);
            return events.FirstOrDefault(e => e.id == EventId) != null;
        }

        public static async Task<bool> EventIsInTickets(string eventId, string cookieValue)
        {
            TicketsApi api = new TicketsApi();
            var tickets = await api.GetAllTickets(cookieValue);
            tickets = tickets.Where(t => t.unsubscribe == false).ToList();
            return tickets.FirstOrDefault(t => t.event_id == eventId) != null;

        }
    }
}