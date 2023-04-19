using MettingsApp.Data;
using MettingsApp.Menus.ViewMeetingsMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewMeetingsMainMenu : SubMenu
    {
        public ViewMeetingsMainMenu(Menu fromMenu, DateTime? date = null) : base(fromMenu)
        {
            Items.AddRange(new[]
            {
                "1. Просмотреть все встречи",
                "2. Просмотреть встречи за один день",
                "3. Просмотреть встречи за промежуток между датами",
                "\n0. Назад"
            });
        }

        public override string Title => "Просмотр встреч";

        public override Menu HandleInput(string input)
        {
            switch (input[0])
            {
                case '1':
                    return new ViewAndEditMeetingsGenericMenu(this);
                case '2':
                    return new ViewMeetingsOnDateMenu(this);
                case '3':
                    return new ViewMeetingsFromToMenu(this);
                case '0':
                    return FromMenu;
                default:
                    throw new Exception("Введена неверная команда");
            }
        }
    }
}
