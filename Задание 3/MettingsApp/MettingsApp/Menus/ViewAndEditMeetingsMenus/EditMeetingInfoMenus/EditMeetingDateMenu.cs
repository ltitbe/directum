using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus
{
    internal class EditMeetingDateMenu : SubMenu
    {
        private readonly Meeting meeting;

        public EditMeetingDateMenu(Meeting meeting, Menu fromMenu) : base(fromMenu)
        {
            this.meeting = meeting;

            Items.AddRange(new[]
           {
                $"Введите новую дату встречи {meeting} (напр. {DateTime.Now.Date:dd.MM.yy})",
                "\n0. Назад"
            });
        }

        public override string Title => "Изменение даты встречи";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            var oldMeetingDate = meeting.GetStartDateTime();

            var newDate = MeetingsHelper.ParseMeetingDate(input);            

            var newStartDate = newDate + meeting.GetStartDateTime().TimeOfDay;
            var newEndDate = newDate + meeting.GetEndDateTime().TimeOfDay;

            if (MeetingsHelper.GetOverlappingMeeting(newStartDate, newEndDate, meeting.GetName()) != null)
                throw new Exception($"Заданный промежуток для встречи {newStartDate:f} – {newEndDate:HH:mm} пересекается с уже существующей встречей");

            //обновить напоминания
            MeetingsHelper.UpdateReminders(oldMeetingDate, newStartDate);

            meeting.SetDates(newStartDate, newEndDate);
            Console.WriteLine("Дата встречи успешно изменена. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
