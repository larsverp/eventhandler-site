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
    }
}