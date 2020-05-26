using System;

namespace RockStar_IT_Events
{
    public static class DateTimeHelper
    {
        static readonly string[] months = new string[]{ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "Decemeber" };
        static readonly string[] days = new string[]{ "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        public static string ConvertToSimpleDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy hh:mm");
        }
    }
}