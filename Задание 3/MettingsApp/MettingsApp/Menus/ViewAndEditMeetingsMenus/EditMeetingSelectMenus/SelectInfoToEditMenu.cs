using MettingsApp.Data;
using MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus
{
    internal class SelectInfoToEditMenu : SubMenu
    {
        private readonly Meeting meeting;

        public SelectInfoToEditMenu(Meeting meeting, Menu fromMenu) : base(fromMenu)
        {
            this.meeting = meeting;

            Items.AddRange(new[]
            {
                "\nВыберите, какую информацию о встрече изменить: ",
                "1. Название встречи",
                "2. Дату встречи",
                "3. Время встречи",
                "\n0. Назад"
            });
        }

        public override string Title => "Что изменить";

        public override Menu HandleInput(string input)
        {
            switch (input[0])
            {
                case '1':
                    return new EditMeetingNameMenu(meeting, this);
                case '2':
                    return new EditMeetingDateMenu(meeting, this);
                case '3':
                    return new EditMeetingStartTimeAndDurationMenu(meeting, this);
                case '0':
                    return FromMenu;
                default:
                    throw new Exception("Неверный ввод");
            }
        }
    }
}
