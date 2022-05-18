using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Easter
    {
        public List<Holiday> EasterHolidays { get; set; }
        public List<Holiday> ExcelHolidays { get; set; }
        public Calculations Calc { get; set; }
        public Epiphany Epiphany { get; set; }
        public int Year { get; set; }

        public Easter(int year, Calculations calc, List<Holiday> holidays)
        {
            EasterHolidays = new List<Holiday>();
            ExcelHolidays = holidays;
            Calc = calc;
            Year = year;
            Epiphany = new Epiphany(year, calc, holidays);
        }

        public List<Holiday> GetAllEasterHolidays()
        {
            EasterHolidays.AddRange(GetNineFirstEasterHolidays());
            EasterHolidays.Add(GetMaundyThursday());
            EasterHolidays.Add(GetGoodFriday());
            EasterHolidays.Add(GetEaster());
            EasterHolidays.Add(GetEasterMonday());
            EasterHolidays.Add(GetFirstSundayAfterEaster());
            EasterHolidays.Add(GetSecondSundayAfterEaster());
            EasterHolidays.Add(GetThirdSundayAfterEaster());
            EasterHolidays.Add(GetPrayerDay());
            EasterHolidays.Add(GetFourthSundayAfterEaster());
            EasterHolidays.Add(GetFifthSundayAfterEaster());
            EasterHolidays.Add(GetChristsAscension());
            EasterHolidays.Add(GetSixthSundayAfterEaster());
            EasterHolidays.Add(GetPentecost());
            EasterHolidays.Add(GetWhitMonday());

            return EasterHolidays;
        }

        private List<Holiday> GetNineFirstEasterHolidays()
        {
            var nineFirstEasterHolidays = new List<Holiday>();

            for (int i = 1; i < 10; i++)
            {
                ExcelHolidays[9 + i].Date = Epiphany.GetDateOfFinalEpiphanyHoliday().AddDays(7 * i); //holidays[i + 9] is the order of the easter holidays in the list.
                nineFirstEasterHolidays.Add(ExcelHolidays[9 + i]);
            }
            return nineFirstEasterHolidays;
        }
        private Holiday GetMaundyThursday()
        {
            ExcelHolidays[19].Date = Calc.GetEasterDate(Year).AddDays(-3);
            return ExcelHolidays[19];
        }
        private Holiday GetGoodFriday()
        {
            ExcelHolidays[20].Date = Calc.GetEasterDate(Year).AddDays(-2);
            return ExcelHolidays[20];
        }
        private Holiday GetEaster()
        {
            ExcelHolidays[21].Date = Calc.GetEasterDate(Year);
            return ExcelHolidays[21];
        }
        private Holiday GetEasterMonday()
        {
            ExcelHolidays[22].Date = Calc.GetEasterDate(Year).AddDays(1);
            return ExcelHolidays[22];
        }
        private Holiday GetFirstSundayAfterEaster()
        {
            ExcelHolidays[23].Date = Calc.GetEasterDate(Year).AddDays(7);
            return ExcelHolidays[23];
        }
        private Holiday GetSecondSundayAfterEaster()
        {
            ExcelHolidays[24].Date = Calc.GetEasterDate(Year).AddDays(14);
            return ExcelHolidays[24];
        }
        private Holiday GetThirdSundayAfterEaster()
        {
            ExcelHolidays[25].Date = Calc.GetEasterDate(Year).AddDays(21);
            return ExcelHolidays[25];
        }
        private Holiday GetPrayerDay()
        {
            ExcelHolidays[26].Date = Calc.GetEasterDate(Year).AddDays(26);
            return ExcelHolidays[26];
        }
        private Holiday GetFourthSundayAfterEaster()
        {
            ExcelHolidays[27].Date = Calc.GetEasterDate(Year).AddDays(28);
            return ExcelHolidays[27];
        }
        private Holiday GetFifthSundayAfterEaster()
        {
            ExcelHolidays[28].Date = Calc.GetEasterDate(Year).AddDays(35);
            return ExcelHolidays[28];
        }
        private Holiday GetChristsAscension()
        {
            ExcelHolidays[29].Date = Calc.GetEasterDate(Year).AddDays(39);
            return ExcelHolidays[29];
        }
        private Holiday GetSixthSundayAfterEaster()
        {
            ExcelHolidays[30].Date = Calc.GetEasterDate(Year).AddDays(42);
            return ExcelHolidays[30];
        }
        private Holiday GetPentecost()
        {
            ExcelHolidays[31].Date = Calc.GetEasterDate(Year).AddDays(49);
            return ExcelHolidays[31];
        }
        public Holiday GetWhitMonday()
        {
            ExcelHolidays[32].Date = Calc.GetEasterDate(Year).AddDays(50);
            return ExcelHolidays[32];
        }

    }
}
