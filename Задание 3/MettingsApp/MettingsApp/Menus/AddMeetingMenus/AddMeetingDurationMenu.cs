using MettingsApp.Data;

namespace MettingsApp.Menus.AddMeetingMenus
{
    public class AddMeetingDurationMenu : SubMenu
    {
        public override string Title => "Ввод продолжительности встречи";

        private readonly DateTime startDate;
        private readonly string meetingName;

        public AddMeetingDurationMenu(string meetingName, DateTime meetingDate, Menu fromMenu) : base(fromMenu)
        {
            this.meetingName = meetingName;
            this.startDate = meetingDate;

            Items = MeetingsHelper.AddMeetingsOnDateInfoToItems(meetingDate, Items);

            Items.AddRange(new []{ 
                $"Название встречи: {meetingName}. Дата встречи: {meetingDate}\nВведите продолжительность встречи (напр. 01:30)", 
                "\n0. Назад"});    
        }

        public override Menu HandleInput(string input)
        {
            if (input == "0")
            {
                Console.Clear();
                return FromMenu;
            }

            var endDate = MeetingsHelper.ParseAndValidateMeetingDuration(input, startDate);
            
            AppData.Meetings.Add(new Meeting(meetingName, startDate, endDate));
            Console.WriteLine($"Встреча \"{meetingName}\" {startDate.Date:D} с {startDate:HH:mm} по {endDate:HH:mm} добавлена\nДля продолжения нажмите любую клавишу: ");
            Console.ReadKey();
            return new MainMenu();            
        }
    }
}
