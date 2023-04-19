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

            //валидируем, что введены две корректные даты через пробел
            var from = MeetingsHelper.ParseDateInput(tokens[0]);
            var to = MeetingsHelper.ParseDateInput(tokens[1]);

            return new ViewAndEditMeetingsGenericMenu(this, from, to);
        }
    }
}
