using MettingsApp.Data;
using System.Globalization;

namespace MettingsApp
{
    public static class MeetingsHelper
    {
        //Вспомогательный класс с методами преобразования и получения данных

        //Получает список встреч
        public static IEnumerable<Meeting> GetMeetings(DateTime? from = null, DateTime? to = null)
        {
            if (from == null && to == null)
                return GetAllMeetings();

            if (to == null)
                return GetMeetingsOnDate(from.Value);

            return from < to 
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

        //Добавляет встречи на определённую дату к строковым пунктам меню. Для отображения пользователю
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

        //Ищет пересекающиеся встречи по одной дате. Чтобы встреча не начиналась посреди другой
        public static Meeting? GetOverlappingMeeting(DateTime meetingStartDate, string? meetingName = null)
        {
            return AppData.Meetings.FirstOrDefault(m => (meetingStartDate >= m.GetStartDateTime() && meetingStartDate < m.GetEndDateTime()) 
            && m.GetName() != meetingName); //эта проверка срабатывает при редактировании, проверяем уникальности по имени встречи. Возможно, стоит использовать id
        }

        //Ищет пересекающиеся встречи по двум датам (начало и конец)
        public static Meeting? GetOverlappingMeeting(DateTime meetingStartDate, DateTime meetingEndDate, string? meetingName = null)
        {
            return AppData.Meetings.FirstOrDefault(m => meetingStartDate < m.GetEndDateTime() && m.GetStartDateTime() < meetingEndDate && m.GetName() != meetingName);
        }       

        //получаем дату в формате 01.01.70
        public static DateTime ParseMeetingDate(string input)
        {
            if (!DateTime.TryParseExact(input, "dd'.'MM'.'yy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime date)
                || date < DateTime.Today) //нельзя создавать в прошлом
                throw new Exception($"Введена неверная дата: \"{input}\"");
            return date;
        }

        //Получаем время в формате "13:30". И проверяем, не занято ли оно уже другой встречей 
        public static DateTime ParseAndValidateMeetingStartTime(string input, DateTime meetingDate, string? name = null)
        {
            if (!TimeSpan.TryParseExact(input, @"hh\:m", null, out var startTime) || startTime < TimeSpan.Zero)
                throw new Exception($"Неверный формат ввода. Введена строка: \"{input}\". Попробуйте ещё раз");
                
            var startDateTime = meetingDate + startTime;

            var meeting = GetOverlappingMeeting(startDateTime, meetingName:name);

            if (meeting != null)
                throw new Exception($"Время начала встречи {startDateTime:HH:mm} пересекается с другой встречей: {meeting}");
            
            return startDateTime;
        }

        //Получаем время окончания встречи. Проверяем, не заняты ли обе даты вместе
        public static DateTime ParseAndValidateMeetingDuration(string input, DateTime startDate, string? meetingName = null)
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

        //получаем дату в формате 01.01.70
        public static DateTime ParseDateInput(string input)
        {
            if (!DateTime.TryParseExact(input, "dd'.'MM'.'yy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                                            out DateTime date))
                throw new Exception($"Введена неверная дата: \"{input}\"");
            return date;
        }

        public static Meeting ParseAndValidateMeetingSelectInput(string input, IEnumerable<Meeting> meetings)
        {
            if (!int.TryParse(input, out var result) || result > meetings.Count() || result < 1)
                throw new Exception("Неверный ввод");

            var meeting = AppData.Meetings.First(m => m.GetStartDateTime() == meetings.ToArray()[result - 1].GetStartDateTime());

            return meeting;
        }

        public static void UpdateReminders(DateTime oldMeetingDate, DateTime newDate)
        {
            var reminders = AppData.Reminders.Where(m => m.GetMeetingDateTime() == oldMeetingDate);
            var diff = newDate - oldMeetingDate;
            foreach (var r in reminders)
               r.SetDate(r.GetDate() + diff);
        }
    }
}
