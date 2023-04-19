using MettingsApp.Data;
using MettingsApp.Menus.RemindersMenu;
using MettingsApp.Menus.ViewMeetingsMenus.EditMeetingSelectMenus;

namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewAndEditMeetingsGenericMenu : SubMenu
    {
        private readonly IEnumerable<Meeting> meetings;
        public override string Title => "Просмотр встреч";

        public ViewAndEditMeetingsGenericMenu(Menu fromMenu, DateTime? start = null, DateTime? end = null) : base(fromMenu)
        {
            meetings = MeetingsHelper.GetMeetings(start, end);

            if (meetings.Any())
                Items = MeetingsHelper.AddMeetingsInfoToItems(meetings, Items);
            else 
                Items.Add("Встреч не найдено");

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

                    //потенциально можно добавить запрос имени итогового файла
                    MeetingsHelper.WriteMeetingsToFile(meetings, "Встречи");
                    Console.WriteLine("Встречи успешно сохранены в файл \"Встречи.txt\"");
                    return this;
                case '0':
                    Console.Clear();
                    return FromMenu;
                default:
                    throw new Exception("Неверный ввод");
            }
        }
    }
}
