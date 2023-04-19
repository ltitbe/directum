using MettingsApp.Data;
using MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.RemindersMenu
{
    internal class ReminderSelectMeetingMenu : SubMenu
    {
        private readonly IEnumerable<Meeting> meetings;

        public ReminderSelectMeetingMenu(IEnumerable<Meeting> meetings, Menu fromMenu) : base(fromMenu)
        {
            this.meetings = meetings;

            Items = MeetingsHelper.AddMeetingsInfoToItems(meetings, Items);

            Items.AddRange(new[]
            {
                "\nВведите номер встречи для добавления напоминания",
                "\n0. Назад"
            });
        }

        public override string Title => "Добавить напоминание";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
                return FromMenu;

            if (!int.TryParse(input, out var result) || result > meetings.Count() || result < 1)
                throw new Exception("Неверный ввод");

            var meeting = AppData.Meetings.First(m => m.GetStartDateTime() == meetings.ToArray()[result - 1].GetStartDateTime());

            return new AddReminderTimeMenu(meeting, this);
        }
    }
}
