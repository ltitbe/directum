namespace MettingsApp.Menus.ViewAndEditMeetingsMenus.ViewMeetingsMenus
{
    internal class ViewMeetingsMainMenu : SubMenu
    {
        public override string Title => "Просмотр встреч";

        public ViewMeetingsMainMenu(Menu fromMenu) : base(fromMenu)
        {
            Items.AddRange(new[]
            {
                "1. Просмотреть все встречи",
                "2. Просмотреть встречи за один день",
                "3. Просмотреть встречи за промежуток между датами",
                "\n0. Назад"
            });
        }        

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
                    Console.Clear();
                    return FromMenu;
                default:
                    throw new Exception("Введена неверная команда");
            }
        }
    }
}
