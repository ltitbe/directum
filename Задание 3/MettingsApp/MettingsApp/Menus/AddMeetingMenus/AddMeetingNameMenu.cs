using MettingsApp.Data;
using MettingsApp.Menus.AddMeetingMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus
{
    internal class AddMeetingNameMenu : SubMenu
    {
        public override string Title => $"Добавить встречу";

        public AddMeetingNameMenu(Menu fromMenu) : base(fromMenu)
        {
            Items.AddRange(new[]
            {
                "Введите название встречи, чтобы продолжить",
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

            return new AddMeetingDateMenu(input, this);
        }
    }
}
