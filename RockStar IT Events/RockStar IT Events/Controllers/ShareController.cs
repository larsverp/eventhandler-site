using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;
using RockStar_IT_Events.ViewModels;
using Rockstar.Models;

namespace RockStar_IT_Events.Controllers
{
    public class ShareController : Controller
    {
        public ActionResult GoogleCalendar(Event @event)
        {
            string eventName = HttpUtility.UrlEncode(@event.title);
            string eventDetails = HttpUtility.UrlEncode(@event.description);
            string eventLocation = HttpUtility.UrlEncode(@event.postal_code);

            int eventDaysDuration = 1;

            DateTime eventStartDate = DateTime.Now;
            string startString = eventStartDate.ToLongDateString();
            DateTime eventEndDate = DateTime.Now.AddDays(eventDaysDuration);
            string endString = eventEndDate.ToLongDateString();

            string calendar = "https://www.google.com/calendar/render?action=TEMPLATE&text=" + eventName + "&dates=" + eventStartDate + "/" + eventEndDate + "&details=" + eventDetails + "location=" + eventLocation + "&sf=true&output=xml";

            return Redirect(calendar);
        }

        public ActionResult LinkedInLink(string url, Event @event)
        {

            string _url = HttpUtility.UrlEncode(url);
            string _title = HttpUtility.UrlEncode(@event.title);

            string shareArticle = "https://linkedin.com/shareArticle?mini=true&url=" + _url + "&title=" + _title;

            return Redirect(shareArticle);
        }

        public ActionResult CreateCalendarFile(Event @event)
        {
            DateTime eventStartDate = DateTime.Now;
            DateTime eventEndDate = DateTime.Now.AddDays(2);

            //string location = "Amsterdam";
            string summary = "samenvatting";
            int eventDuration = 30; // event time in minutes

            StringBuilder sb = new StringBuilder();
            string dateFormat = "yyyyMMddTHHmmssZ";

            string dateNow = DateTime.Now.ToUniversalTime().ToString(dateFormat);

            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Team Rockstars/EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");

            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine("DTSTART:" + eventStartDate.ToUniversalTime().ToString(dateFormat));
            sb.AppendLine("DTEND:" + eventEndDate.ToUniversalTime().ToString(dateFormat));
            sb.AppendLine("DTSTAMP:" + dateNow);
            sb.AppendLine("UID:" + Guid.NewGuid().ToString());
            sb.AppendLine("CREATED:" + dateNow);
            sb.AppendLine("DESCRIPTION:" + @event.description);
            sb.AppendLine("LAST-MODIFIED:" + dateNow);
            sb.AppendLine("LOCATION:" + @event.postal_code); //location here;
            sb.AppendLine("SEQUENCE:0");
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:" + summary); //short summary?
            sb.AppendLine("TRANSP:OPAQUE");

            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine("TRIGGER:" + "-PT30M");
            sb.AppendLine("REPEAT:" + "0");
            sb.AppendLine("DURATION:" + "PT" + eventDuration.ToString() + "M");
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:" + @event.title);
            sb.AppendLine("END:VALARM");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");

            byte[] icsBytes = Encoding.UTF8.GetBytes(sb.ToString());

            return File(icsBytes, "text/calendar", @event.title + ".ics");
        }
    }
}