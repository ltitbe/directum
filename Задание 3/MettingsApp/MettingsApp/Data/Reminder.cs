namespace MettingsApp.Data
{
    public class Reminder
    {
        private readonly Meeting meeting;
        private DateTime date;

        public Reminder(Meeting meeting, DateTime date)
        {
            this.meeting = meeting;
            this.date = date;
        }

        public override string ToString()
        {
            return $"! Напоминание о встрече: {meeting}";
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public DateTime GetMeetingDateTime()
        {
            return this.meeting.GetStartDateTime();
        }

        public void SetDate(DateTime date)
        {
            this.date = date;
        }
    }
}
