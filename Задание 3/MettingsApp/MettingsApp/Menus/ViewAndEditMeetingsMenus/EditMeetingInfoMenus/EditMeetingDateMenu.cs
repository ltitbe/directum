﻿using MettingsApp.Data;
using MettingsApp.Menus.AddMeetingMenus;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                $"Введите новую дату встречи {meeting}",
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

            var date = MeetingsHelper.ParseMeetingDate(input);

            var newStartDate = date + meeting.GetStartDateTime().TimeOfDay;
            var newEndDate = date + meeting.GetEndDateTime().TimeOfDay;

            if (MeetingsHelper.GetOverlappingMeeting(newStartDate, newEndDate, meeting.GetName()) != null)
                throw new Exception($"Заданный промежуток для встречи {newStartDate:f} – {newEndDate:HH:mm} пересекается с уже существующей встречей");

            meeting.SetDates(newStartDate, newEndDate);
            Console.WriteLine("Дата встречи успешно изменена. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
