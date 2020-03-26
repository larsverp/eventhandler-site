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

        public void ChangeTitle(string newEventTitle)
        {
            EventTitle = newEventTitle;
        }

        public void ChangeDescription(string newEventDescription)
        {
            EventDescription = newEventDescription;
        }

        public void ChangeDateTime(DateTime newEventTime)
        {
            EventTime = newEventTime;
        }

        public void ChangeMaxAvailableSeats(int newMaxAvailableSeats)
        {
            MaxAvailableSeats = newMaxAvailableSeats;
        }

        public bool UserSignUp()
        {
            if (AvailableSeats == MaxAvailableSeats)
            {
                return false;
            }
            
            OccupiedSeats++;
            return true;
            
        }

        public void ChangeLocation(string newEventLocation)
        {
            EventLocation = newEventLocation;
        }

        public void SetEventNotifications(bool newEventNotifications)
        {
            EventNotifications = newEventNotifications;
        }

        public void SetCategories(List<Category> newEventCategories)
        {
            EventCategories = newEventCategories;
        }

    }
}
