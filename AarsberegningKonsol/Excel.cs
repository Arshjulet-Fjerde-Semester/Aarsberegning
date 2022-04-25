using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Excel
    {
        public List<Holiday> GetAllHolidays()
        {
            var holidays = new List<Holiday>();
            using (StreamReader sr = new StreamReader("../../../Holidays.csv"))
            {
                string? headerLineSkipped = sr.ReadLine();
                string? line;
                string[] columns;
                while ((line = sr.ReadLine()) != null)
                {
                    columns = line.Split(',');
                    holidays.Add(new Holiday(columns[1]));
                }
            }
            return holidays;
        }
    }
}
