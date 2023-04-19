using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus
{
    internal class EditMeetingStartTimeAndDurationMenu : SubMenu
    {
        public override string Title => "Изменение времени начала и продолжительности встречи";

        private readonly Meeting meeting;

        public EditMeetingStartTimeAndDurationMenu(Meeting meeting, Menu fromMenu) : base(fromMenu)
        {
            this.meeting = meeting;

            Items.AddRange(new[]
           {
                $"Введите новое время начала и продолжительность встречи {meeting} через пробел (напр. \"16:00 02:00\")",
                "\n0. Назад"
            });
        }

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            var oldMeetingDate = meeting.GetStartDateTime();   

            //ожидаем ввод двух значений времени через пробел
            var tokens = input.Split(' ');

            //проверяем валидность введённых значений времени
            var startDate = MeetingsHelper.ParseAndValidateMeetingStartTime(tokens[0], meeting.GetStartDate(), meeting.GetName());
            var endDate = MeetingsHelper.ParseAndValidateMeetingStartTime(tokens[1], meeting.GetStartDate(), meeting.GetName());

            meeting.SetDates(startDate, endDate);

            //обновить напоминания            
            MeetingsHelper.UpdateReminders(oldMeetingDate, startDate);

            Console.WriteLine("Время встречи успешно изменено. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
