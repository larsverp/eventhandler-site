using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Models
{
    public class Event
    {
        public int id { get; private set; }

        public string title { get; private set; }

        public string description { get; private set; }

        public DateTime date { get; private set; }

        public string thumbnail { get; private set; }

        public int seats { get; private set; }

        public int AvailableSeats
        {
            get { return seats - OccupiedSeats; }
        }

        private int OccupiedSeats = 0;

        public string postal_code { get; private set; }

        public string hnum { get; private set; }

        public bool notification { get; private set; }

        public List<Category> Categories { get; private set; }

        public Event(int eventId, string eventTitle, string eventDescription, DateTime eventDate, string eventThumbnail, int maxavailableSeats, string eventPostalCode, string eventHnum, bool eventNotifications, List<Category> eventCategories)
        {
            id = eventId;
            title = eventTitle;
            description = eventDescription;
            date = eventDate;
            thumbnail = eventThumbnail;
            seats = maxavailableSeats;
            postal_code = eventPostalCode;
            hnum = eventHnum;
            notification = eventNotifications;
            Categories = eventCategories;
        }

        public void ChangeEventTitle(string newEventTitle)
        {
            title = newEventTitle;
        }

        public void ChangeEventDescription(string newEventDescription)
        {
            description = newEventDescription;
        }

        public void ChangeEventDateTime(DateTime newEventDate)
        {
            date = newEventDate;
        }

        public void ChangeEventThumbnail(string newEventThumbnail)
        {
            thumbnail = newEventThumbnail;
        }

        public void ChangeMaxAvailableSeats(int newMaxAvailableSeats)
        {
            seats = newMaxAvailableSeats;
        }

        public bool UserEventSignUp()
        {
            if (AvailableSeats == seats)
            {
                return false;
            }
            
            OccupiedSeats++;
            return true;
            
        }

        public void ChangeEventLocation(string newEventPostalCode)
        {
            postal_code = newEventPostalCode;
        }

        public void SetEventNotifications(bool newEventNotifications)
        {
            notification = newEventNotifications;
        }

        public void SetEventCategories(List<Category> newEventCategories)
        {
            Categories = newEventCategories;
        }

    }
}
