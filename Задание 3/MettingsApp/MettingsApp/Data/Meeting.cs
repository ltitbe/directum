using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MettingsApp.Data
{
    public class Meeting
    {
        private string name;
        private DateTime startDate;
        private DateTime endDate;

        public Meeting(string name, DateTime startDate, DateTime endDate)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public override string ToString()
        {
            return $"{startDate:D} \"{name}\" {startDate:HH:mm} – {endDate:HH:mm}";
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

       

        public DateTime GetStartDate()
        {
            return this.startDate.Date;
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            SetStartDateTime(startDate);
            SetEndDateTime(endDate);
        }

        public void SetStartDateTime(DateTime date)
        {
            this.startDate = date;
        }

        public DateTime GetStartDateTime()
        {
            return this.startDate;
        }

        public DateTime GetEndDateTime()
        {
            return this.endDate;
        }

        public void SetEndDateTime(DateTime date)
        {
            endDate = date;
        }
    }
}
