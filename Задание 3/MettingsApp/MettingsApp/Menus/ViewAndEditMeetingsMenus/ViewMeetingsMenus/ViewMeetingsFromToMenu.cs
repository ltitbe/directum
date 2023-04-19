using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MettingsApp.Menus.ViewMeetingsMenus;

namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewMeetingsFromToMenu : SubMenu
    {
        public ViewMeetingsFromToMenu(Menu fromMenu) : base(fromMenu)
        {
            Items.AddRange(new[]
           {
                $"Введите две даты через пробел (например: {DateTime.Now.Date:dd.MM.yy} {DateTime.Now.AddDays(5).Date:dd.MM.yy})",
                "\n0. Назад"
            });
        }

        public override string Title => "Ввести даты для просмотра встреч";

        public override Menu HandleInput(string input)
        {
            var tokens = input.Trim().Split(' ');
            var from = MeetingsHelper.ParseDateInput(tokens[0]);
            var to = MeetingsHelper.ParseDateInput(tokens[1]);
            return new ViewAndEditMeetingsGenericMenu(this, from, to);
        }
    }
}
