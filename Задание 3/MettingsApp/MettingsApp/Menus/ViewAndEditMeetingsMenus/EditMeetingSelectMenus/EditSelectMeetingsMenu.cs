using MettingsApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus
{
    internal class EditSelectMeetingsMenu : SubMenu
    {
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

        public override string Title => "Выбор встречи для изменения";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
                return FromMenu;

            var meeting = MeetingsHelper.ParseAndValidateMeetingSelectInput(input, meetings);
                        
            return new SelectInfoToEditMenu(meeting, this);
        }
    }
}
