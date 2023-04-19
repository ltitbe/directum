using MettingsApp.Data;

namespace MettingsApp.Menus.RemindersMenu
{
    //Меню добавления напоминания. Задаём время до события
    internal class AddReminderTimeMenu : SubMenu
    {
        private readonly Meeting meeting;

        public AddReminderTimeMenu(Meeting meeting, Menu fromMenu) : base(fromMenu)
        {
            this.meeting = meeting;
                        
            Items.AddRange(new[]
            {
                $"\nВстреча {meeting}",
                "\nВведите, за какое время до начала встречи необходимо уведомить (напр. \"01:00\")",
                "\n0. Назад"
            });
        }

        public override string Title => "Создать напоминание";

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            if (!TimeSpan.TryParseExact(input, @"h\:m", null, out var time) || time <= TimeSpan.Zero)
                throw new Exception($"Неверный формат ввода. Введена строка: \"{input}\"");

            var fromTime = meeting.GetStartDateTime() - time;

            AppData.Reminders.Add(new Reminder(meeting, fromTime));
            Console.WriteLine($"Напоминание о встрече {meeting} добавлено. Будет отображаться с {fromTime:g}. Для продолжения нажмите любую клавишу");
            Console.ReadKey();
            return new MainMenu();
        }
    }
}
