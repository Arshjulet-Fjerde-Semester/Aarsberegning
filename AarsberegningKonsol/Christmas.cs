using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Christmas
    {
        public List<Holiday> ChristmasHolidays { get; set; }
        public List<Holiday> ExcelHolidays { get; set; }
        public Calculations Calc { get; set; }
        public Trinity Trinity { get; set; }
        public int Year { get; set; }
        public int DaysAfterSecondChristmasDayToChristmasSunday { get; set; }

        public Christmas(int year, Calculations calc, List<Holiday> holidays)
        {
            ChristmasHolidays = new List<Holiday>();
            ExcelHolidays = holidays;
            Calc = calc;
            Year = year;
            Trinity = new Trinity(year, calc, holidays);
            CalculateDaysAfterSecondChristmasDayToChristmasSunday();
        }
        public List<Holiday> GetAllChristmasHolidays()
        {
            if (IsChristmasSundayThisYear())
            {
                ChristmasHolidays.AddRange(GetAdventSundays());
                ChristmasHolidays.Add(GetChristmasDay());
                ChristmasHolidays.Add(GetSecondChristmasDay());
                ChristmasHolidays.Add(GetChristmasSunday());
            }
            else
            {
                ChristmasHolidays.AddRange(GetAdventSundays());
                ChristmasHolidays.Add(GetChristmasDay());
                ChristmasHolidays.Add(GetSecondChristmasDay());
            }
            return ChristmasHolidays;
        }
        private List<Holiday> GetAdventSundays()
        {
            var adventSundays = new List<Holiday>();

            for (int i = 0; i < 4; i++)
            {
                ExcelHolidays[61 + i].Date = Trinity.FinalTrinityHoliday.Date.AddDays(7 + 7 * i);
                adventSundays.Add(ExcelHolidays[61 + i]);
            }
            return adventSundays;
        }
        private Holiday GetChristmasDay()
        {
            ExcelHolidays[65].Date = new DateTime(Year, 12, 25);
            return ExcelHolidays[65];
        }
        private Holiday GetSecondChristmasDay()
        {
            ExcelHolidays[66].Date = new DateTime(Year, 12, 26);
            return ExcelHolidays[66];
        }
        private Holiday GetChristmasSunday()
        {
            ExcelHolidays[67].Date = Trinity.FinalTrinityHoliday.Date.AddDays(35);
            return ExcelHolidays[67];
        }
        private void CalculateDaysAfterSecondChristmasDayToChristmasSunday()
        {
            DaysAfterSecondChristmasDayToChristmasSunday = Calc.CalendarNr % 7;
        }
        private bool IsChristmasSundayThisYear()
        {
            if (DaysAfterSecondChristmasDayToChristmasSunday == 0 ||
                DaysAfterSecondChristmasDayToChristmasSunday == 6)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
