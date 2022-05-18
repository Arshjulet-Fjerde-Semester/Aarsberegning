using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class YearCalculator
    {
        public List<Holiday> Holidays { get; set; }
        public List<Holiday> DisplayHolidays { get; set; }
        public Calculations Calc { get; set; }
        public Excel Excel { get; set; }
        public Epiphany Epiphany { get; set; }
        public Easter Easter { get; set; }
        public Trinity Trinity { get; set; }
        public Christmas Christmas { get; set; }

        public YearCalculator()
        {
            Calc = new Calculations(DateTime.Now.Year);
            DisplayHolidays = new List<Holiday>();
            Excel = new Excel();
            Holidays = Excel.GetAllHolidays();
        }

        public void CalculateChurchYear(int year)
        {
            Calc.CalculateVariables(year);
            InstantiateSeasonObjects(year);
            AddAllHolidaysToDisplay();
        }
        private void InstantiateSeasonObjects(int year)
        {
            Epiphany = new Epiphany(year, Calc, Holidays);
            Easter = new Easter(year, Calc, Holidays);
            Trinity = new Trinity(year, Calc, Holidays);
            Christmas = new Christmas(year, Calc, Holidays);
        }
        private void AddAllHolidaysToDisplay()
        {
            if (DisplayHolidays.Count != 0)
            {
                DisplayHolidays.Clear();
            }

            DisplayHolidays.AddRange(Epiphany.GetAllEpiphanyHolidays());
            DisplayHolidays.AddRange(Easter.GetAllEasterHolidays());
            DisplayHolidays.AddRange(Trinity.GetAllTrinityHolidays());
            DisplayHolidays.AddRange(Christmas.GetAllChristmasHolidays());
        }
    }
}
