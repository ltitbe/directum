using MettingsApp.Data;
using MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus;

namespace MettingsApp.Menus.RemindersMenu
{
    internal class ReminderSelectMeetingMenu : SubMenu
    {
        private readonly IEnumerable<Meeting> meetings;
        public override string Title => "Добавить напоминание";

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

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }                

            var meeting = MeetingsHelper.ParseAndValidateMeetingSelectInput(input, meetings);

            return new AddReminderTimeMenu(meeting, this);
        }
    }
}
