using MettingsApp.Data;
using MettingsApp.Menus.RemindersMenu;
using MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewAndEditMeetingsGenericMenu : SubMenu
    {
        private readonly IEnumerable<Meeting> meetings;
        public override string Title => "Просмотр встреч";

        public ViewAndEditMeetingsGenericMenu(Menu fromMenu, DateTime? start = null, DateTime? end = null) : base(fromMenu)
        {
            meetings = MeetingsHelper.GetMeetings(start, end);

            Items = MeetingsHelper.AddMeetingsInfoToItems(meetings, Items);

            Items.AddRange(new[]
            {
                "\n1. Изменить информацию о встрече",
                "2. Добавить напоминание о встрече",
                "3. Сохранить информацию о встречах в текстовый файл",
                "\n0. Назад"
            });
        }

        public override Menu HandleInput(string input)
        {
            switch (input[0])
            {
                case '1':
                    return new EditSelectMeetingsMenu(meetings, this);
                case '2':
                    return new ReminderSelectMeetingMenu(meetings, this);
                case '3':
                    if (!meetings.Any())
                        throw new Exception("Нет встреч для сохранения в файл");

                    MeetingsHelper.WriteMeetingsToFile(meetings, "Встречи");
                    Console.WriteLine("Встречи успешно сохранены в файл \"Встречи.txt\"");
                    return this;
                case '0':
                    return FromMenu;
                default:
                    throw new Exception("Неверный ввод");
            }
        }
    }
}
