using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewMeetingsOnDateMenu : SubMenu
    {
        public override string Title => "Выбор даты встреч";

        public ViewMeetingsOnDateMenu(Menu fromMenu) : base(fromMenu)
        {
            Items.AddRange(new[]
            {
                $"Введите дату встреч для просмотра (например: {DateTime.Now.Date:dd.MM.yy})",
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

            var date = MeetingsHelper.ParseDateInput(input);

            return new ViewAndEditMeetingsGenericMenu(this, date);
        }
    }
}
