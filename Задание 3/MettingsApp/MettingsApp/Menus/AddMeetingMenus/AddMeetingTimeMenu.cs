using MettingsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.AddMeetingMenus
{
    internal class AddMeetingTimeMenu : SubMenu
    {
        public override string Title => "Выбор времени начала встречи";
        private readonly DateTime meetingDate;
        private readonly string meetingName;

        public AddMeetingTimeMenu(string meetingName, DateTime meetingDate, Menu fromMenu) : base(fromMenu)
        {
            this.meetingName = meetingName;
            this.meetingDate = meetingDate;

            Items = MeetingsHelper.AddMeetingsOnDateInfoToItems(meetingDate, Items);

            Items.AddRange(new[] { $"\nНазвание встречи: {meetingName}. Дата встречи: {meetingDate.Date:dd.MM.yyyy}\nДля продолжения укажите время начала встречи (напр. 13:30)", "0. Назад" });
        }

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            var startDateTime = MeetingsHelper.ParseMeetingStartTime(input, meetingDate);

            return new AddMeetingDurationMenu(meetingName, startDateTime, this);
        }
    }
}
