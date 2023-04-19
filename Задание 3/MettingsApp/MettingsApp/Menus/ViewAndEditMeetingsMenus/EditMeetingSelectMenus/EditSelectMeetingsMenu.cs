using MettingsApp.Data;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus
{
    internal class EditSelectMeetingsMenu : SubMenu
    {
        public override string Title => "Выбор встречи для изменения";

        private readonly IEnumerable<Meeting> meetings;

        public EditSelectMeetingsMenu(IEnumerable<Meeting> meetings, Menu fromMenu) : base(fromMenu)
        {
            this.meetings = meetings;

            Items = MeetingsHelper.AddMeetingsInfoToItems(meetings, Items);

            Items.AddRange(new[]
            {
                "\nВведите номер встречи для изменения",
                "\n0. Назад"
            });
        }        

        public override Menu HandleInput(string input)
        {
            if (input == "0")
                return FromMenu;

            //проверка валидности выбора встречи для изменения
            var meeting = MeetingsHelper.ParseAndValidateMeetingSelectInput(input, meetings);
                        
            return new SelectInfoToEditMenu(meeting, this);
        }
    }
}
