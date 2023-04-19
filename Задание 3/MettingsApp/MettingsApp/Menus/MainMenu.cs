using MettingsApp.Data;
using MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus;

namespace MettingsApp.Menus
{
    public class MainMenu : Menu
    {
        public override string Title  => "Главное меню"; 
       
        public MainMenu() : base()
        {
            UpdateItems();
        }

        public override Menu HandleInput(string input)
        {
            UpdateItems();
            switch (input[0])
            {
                case '1':
                    return new AddMeetingNameMenu(this);
                case '2':
                    return new ViewMeetingsMainMenu(this);
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    throw new Exception("Введена неверная команда");
            }
            return this;
        }

        //показать напоминания
        public void ShowReminders()
        {
            //удалить те напоминания, встречи которых уже прошли
            AppData.Reminders.RemoveAll(r => r.GetMeetingDateTime() < DateTime.Now);

            foreach (var r in AppData.Reminders)
            {
                if (r.GetDate() <= DateTime.Now && r.GetMeetingDateTime() >= DateTime.Now)
                    Items.Add(r.ToString());
            }
        }

        public void UpdateItems()
        {
            //Метод добавляет напоминания к пунктам меню
            ShowReminders();
            Items.AddRange(new[]
            {
                "\n1. Добавить новую встречу",
                "2. Просмотреть встречи",
                "\n0. Выход"
            });
        }
    }
}
