using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockStar_IT_Events.Models
{
    public class Event
    {
        public string EventTitle { get; private set; }

        public string EventDescription { get; private set; }

        public DateTime EventTime { get; private set; }

        public string EventThumbnail { get; private set; }

        public int MaxAvailableSeats { get; private set; }

        public int AvailableSeats
        {
            get { return MaxAvailableSeats - OccupiedSeats; }
        }

        private int OccupiedSeats = 0;

        public string EventLocation { get; private set; }

        public bool EventNotifications { get; private set; }

        public List<Category> EventCategories { get; private set; }

        public Event(string eventTitle, string eventDescription, DateTime eventTime, string eventThumbnail, int maxavailableSeats, string eventLocation, bool eventNotifications, List<Category> eventCategories)
        {
            EventTitle = eventTitle;
            EventDescription = eventDescription;
            EventTime = eventTime;
            EventThumbnail = eventThumbnail;
            MaxAvailableSeats = maxavailableSeats;
            EventLocation = eventLocation;
            EventNotifications = eventNotifications;
            EventCategories = eventCategories;
        }

        public void ChangeEventTitle(string newEventTitle)
        {
            EventTitle = newEventTitle;
        }

        public void ChangeEventDescription(string newEventDescription)
        {
            EventDescription = newEventDescription;
        }

        public void ChangeEventDateTime(DateTime newEventTime)
        {
            EventTime = newEventTime;
        }

        public void ChangeEventThumbnail(string newEventThumbnail)
        {
            EventThumbnail = newEventThumbnail;
        }

        public void ChangeMaxAvailableSeats(int newMaxAvailableSeats)
        {
            MaxAvailableSeats = newMaxAvailableSeats;
        }

        public bool UserEventSignUp()
        {
            if (AvailableSeats == MaxAvailableSeats)
            {
                return false;
            }
            
            OccupiedSeats++;
            return true;
            
        }

        public void ChangeEventLocation(string newEventLocation)
        {
            EventLocation = newEventLocation;
        }

        public void SetEventNotifications(bool newEventNotifications)
        {
            EventNotifications = newEventNotifications;
        }

        public void SetEventCategories(List<Category> newEventCategories)
        {
            EventCategories = newEventCategories;
        }

    }
}
