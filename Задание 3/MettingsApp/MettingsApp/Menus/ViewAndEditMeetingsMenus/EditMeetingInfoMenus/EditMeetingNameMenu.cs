using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingInfoMenus
{
    internal class EditMeetingNameMenu : SubMenu
    {
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

        public override string Title => "Изменить название встречи";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
                return FromMenu;

            meeting.SetName(input);
            Console.WriteLine("Название встречи успешно изменено. Для продолжения нажмите любую клавишу");
            Console.ReadKey();

            return new ViewMeetingsMainMenu(new MainMenu());
        }
    }
}
