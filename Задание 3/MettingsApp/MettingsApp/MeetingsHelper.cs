using MettingsApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp
{
    public static class MeetingsHelper
    {
        public static IEnumerable<Meeting> GetMeetings(DateTime? from = null, DateTime? to = null)
        {
            if (from == null && to == null)
                return GetAllMeetings();

            if (to == null)
                return GetMeetingsOnDate(from.Value);

            return from > to 
                ? GetMeetingsFromTo(from.Value, to.Value) 
                : GetMeetingsFromTo(to.Value, from.Value);
        }

        public static IEnumerable<Meeting> GetAllMeetings()
        {
            return AppData.Meetings;
        }

        public static IEnumerable<Meeting> GetMeetingsOnDate(DateTime date)
        {
            return AppData.Meetings.Where(m => m.GetStartDate() == date.Date);
        }

        public static IEnumerable<Meeting> GetMeetingsFromTo(DateTime fromDate, DateTime toDate)
        {
            return AppData.Meetings.Where(m => m.GetStartDate() >= fromDate.Date && m.GetStartDate() < toDate.AddDays(1).Date);
        }

        public static List<string> AddMeetingsOnDateInfoToItems(DateTime date, List<string> Items)
        {
            var meetings = GetMeetingsOnDate(date);

            if (!meetings.Any())
            {
                Items.Add($"На {date:dd.MM.yyyy} других встреч нет");
                return Items;
            }
            
            Items.Add($"Другие встречи на {date.Date:dd.MM.yyyy} ({meetings.Count()}): ");
            return AddMeetingsInfoToItems(meetings, Items);
        }

        public static List<string> AddMeetingsInfoToItems(IEnumerable<Meeting> meetings, List<string> Items)
        {
            Items.AddRange(meetings.Select((m, i) => $"{i + 1}. {m}"));
            return Items;
        }

        public static void WriteMeetingsToFile(IEnumerable<Meeting> meetings, string fileName)
        {
            File.WriteAllLines(fileName + ".txt", meetings.Select((m, i) => $"{i + 1}. {m}" + Environment.NewLine));
        }

        public static Meeting? GetOverlappingMeeting(DateTime meetingStartDate, string? meetingName = null)
        {
            return AppData.Meetings.FirstOrDefault(m => (meetingStartDate >= m.GetStartDateTime() && meetingStartDate < m.GetEndDateTime()) && m.GetName() != meetingName);
        }

        public static Meeting? GetOverlappingMeeting(DateTime meetingStartDate, DateTime meetingEndDate, string? meetingName = null)
        {
            return AppData.Meetings.FirstOrDefault(m => meetingStartDate < m.GetEndDateTime() && m.GetStartDateTime() < meetingEndDate && m.GetName() != meetingName);
        }       

        public static DateTime ParseMeetingDate(string input)
        {
            if (!DateTime.TryParseExact(input, "dd'.'MM'.'yy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime date)
                || date < DateTime.Today)
                throw new Exception($"Введена неверная дата: \"{input}\"");
            return date;
        }

        public static DateTime ParseMeetingStartTime(string input, DateTime meetingDate, string? name = null)
        {
            if (!TimeSpan.TryParseExact(input, @"hh\:m", null, out var startTime) || startTime < TimeSpan.Zero)
                throw new Exception($"Неверный формат ввода. Введена строка: \"{input}\". Попробуйте ещё раз");
                
            var startDateTime = meetingDate + startTime;

            var meeting = GetOverlappingMeeting(startDateTime, meetingName:name);

            if (meeting != null)
                throw new Exception($"Время начала встречи {startDateTime:HH:mm} пересекается с другой встречей: {meeting}");
            
            return startDateTime;
        }

        public static DateTime ParseMeetingDuration(string input, DateTime startDate, string? meetingName = null)
        {
            if (!TimeSpan.TryParseExact(input, @"h\:m", null, out var duration) || duration <= TimeSpan.Zero)
                throw new Exception($"Неверный формат ввода. Введена строка: \"{input}\"");

            var endDate = startDate + duration;
            if (endDate.Date != startDate.Date)
                throw new Exception($"Встреча должна заканчиваться в тот же день, в который началась. \n" +
                        $"Дата начала встречи: {startDate}. Задана продолжительность: {duration:HH:mm}. Вычислены дата и время окончания встречи: {endDate}\n");

            var meeting = GetOverlappingMeeting(startDate, endDate, meetingName);

            if (meeting != null)
                throw new Exception($"Встреча с {startDate:g} по {endDate:HH:mm} пересекается с другой встречей: {meeting}");

            return endDate;
        }

        public static DateTime ParseDateInput(string input)
        {
            if (!DateTime.TryParseExact(input, "dd'.'MM'.'yy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime date))
                throw new Exception($"Введена неверная дата: \"{input}\"");
            return date;
        }
    }
}
