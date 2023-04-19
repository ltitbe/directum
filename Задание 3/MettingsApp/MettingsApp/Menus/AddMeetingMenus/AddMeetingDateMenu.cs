using MettingsApp.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.AddMeetingMenus
{
    //Меню добавления даты встречи
    internal class AddMeetingDateMenu : SubMenu
    {
        public override string Title => "Выбор даты встречи";

        private string meetingName { get; set; }

        public AddMeetingDateMenu(string name, Menu fromMenu) : base(fromMenu)
        {
            meetingName = name;
            Items.AddRange(new[]
            {
                $"Название встречи: {meetingName}\nДля продолжения укажите дату встречи (например: {DateTime.Now.Date:dd.MM.yy})",
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

            var date = MeetingsHelper.ParseMeetingDate(input);

            return new AddMeetingTimeMenu(meetingName, date, this);            
        }
    }
}
