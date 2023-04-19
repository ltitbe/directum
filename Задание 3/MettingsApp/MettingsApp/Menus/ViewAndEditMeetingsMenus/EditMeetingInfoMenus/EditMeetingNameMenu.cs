using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus
{
    internal class EditMeetingNameMenu : SubMenu
    {
        public override string Title => "Изменить название встречи";

        private readonly Meeting meeting;

        public EditMeetingNameMenu(Meeting meeting, Menu fromMenu) : base(fromMenu)
        {
            this.meeting = meeting;

            Items.AddRange(new[]
            {
                $"Введите новое название встречи {meeting}",
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

            meeting.SetName(input);
            Console.WriteLine("Название встречи успешно изменено. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
