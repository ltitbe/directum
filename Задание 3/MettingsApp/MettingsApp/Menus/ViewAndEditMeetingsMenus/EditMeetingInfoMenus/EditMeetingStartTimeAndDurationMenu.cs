using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus
{
    internal class EditMeetingStartTimeAndDurationMenu : SubMenu
    {
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

        public override string Title => "Изменение времени начала и продолжительности встречи";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            var tokens = input.Split(' ');

            var startDate = MeetingsHelper.ParseMeetingStartTime(tokens[0], meeting.GetStartDate(), meeting.GetName());
            var endDate = MeetingsHelper.ParseMeetingDuration(tokens[1], startDate, meeting.GetName());

            meeting.SetDates(startDate, endDate);
            Console.WriteLine("Время встречи успешно изменено. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
