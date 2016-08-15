using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace Common
{
    public static class DateTimeExtension
    {
        public static int GetWeekOfYear(this DateTime date)
        {
            var culture = CultureInfo.CurrentCulture;
            return culture.Calendar.GetWeekOfYear(
                date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        public static DateTime GetNextMonday(this DateTime date)
        {
            var date2 = date.AddDays(1).Date;
            int days = ((int)DayOfWeek.Monday - (int)date2.DayOfWeek + 7) % 7;
            return date2.AddDays(days);
        }

        public static string ToDate(this DateTime? date)
        {
            return date == null ? "" : date.Value.ToString("dd.MM.yyyy");
        }

        public static string ToDate(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}