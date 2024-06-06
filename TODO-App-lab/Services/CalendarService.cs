using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net;
using NodaTime;
using CommunityToolkit.Maui.Core;

namespace TODO_App_lab.Services
{
    public class CalendarService
    {
        public void GenerateICalClicked(string title, DateTime startDate, DateTime? endDate = null)
        {
            string summary = title;

            var end = (endDate.HasValue) ?
                new CalDateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, endDate.Value.Hour, endDate.Value.Minute, 0) :
                new CalDateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute + 1, 0);

            RecurrencePattern recurrence = null;
            if (endDate.HasValue)
            {
                recurrence = new RecurrencePattern
                {
                    Frequency = FrequencyType.Daily, // Ustaw odpowiednią częstotliwość, jeśli jest wymagana
                    Interval = 1,
                    Until = endDate.Value
                };
            }

            var icalEvent = new CalendarEvent
            {
                Summary = summary,
                Start = new CalDateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0),
                End = end,
                IsAllDay = false,
                RecurrenceRules = recurrence != null ? new List<RecurrencePattern>() { recurrence } : null
            };

            var calendar = new Calendar();
            calendar.Events.Add(icalEvent);

            var serializer = new CalendarSerializer();
            string icalContent = serializer.SerializeToString(calendar);

            string filePath = "event.ics";

            string popoverTitle = "Read ical file";
            string file = System.IO.Path.Combine(FileSystem.CacheDirectory, filePath);

            System.IO.File.WriteAllText(file, icalContent);

            Launcher.Default.OpenAsync(new OpenFileRequest(popoverTitle, new ReadOnlyFile(file))).ConfigureAwait(false);


        }
    }
}
