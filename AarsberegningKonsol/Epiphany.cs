using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Epiphany
    {
        public int AmountOfEpiphanyHolidays { get; set; }
        public List<Holiday> EpiphanyHolidays { get; set; }
        public List<Holiday> ExcelHolidays { get; set; }
        public Calculations Calc { get; set; }
        public int Year { get; set; }


        public Epiphany(int year, Calculations calc, List<Holiday> holidays)
        {
            EpiphanyHolidays = new List<Holiday>();
            ExcelHolidays = holidays;
            Calc = calc;
            Year = year;
            CalculateAmountOfEpiphanySundays();
        }
        public List<Holiday> GetAllEpiphanyHolidays()
        {
            EpiphanyHolidays.Add(GetNewYearsDay());
            EpiphanyHolidays.Add(GetFirstEpiphanyHoliday());

            if (NeedAdjustedRemainingEpiphanyHolidays())
            {
                EpiphanyHolidays.AddRange(GetAdjustedRemainingEpiphanyHolidays());
            }
            else
            {
                EpiphanyHolidays.AddRange(GetRemainingEpiphanyHolidays());

            }

            return EpiphanyHolidays;
        }
        private Holiday GetNewYearsDay()
        {
            ExcelHolidays[0].Date = new DateTime(Year, 1, 1);
            return ExcelHolidays[0];
        }

        private void CalculateAmountOfEpiphanySundays()
        {
            if (Calc.IsFirstEpiphanyHolidayOnSeventhOrEigthOfJanuary())
            {
                AmountOfEpiphanyHolidays = ((Calc.GetEasterDate(Year).DayOfYear - 70) / 7) - 1;
            }
            else
            {
                AmountOfEpiphanyHolidays = (Calc.GetEasterDate(Year).DayOfYear - 70) / 7;
            }
        }

        private Holiday GetFirstEpiphanyHoliday()
        {
            if (Calc.IsFirstEpiphanyHolidayOnEighthOfJanuary())
            {
                ExcelHolidays[3].Date = new DateTime(Year, 1, 8 - Calc.LeapYearAdjustment);
                return ExcelHolidays[3]; //ExcelHolidays[3] = First Sunday After Epiphany
            }
            else if (Calc.IsFirstEpiphanyHolidayOnSeventhOfJanuary())
            {
                ExcelHolidays[3].Date = new DateTime(Year, 1, 7 - Calc.LeapYearAdjustment);
                return ExcelHolidays[3]; //ExcelHolidays[3] = First Sunday After Epiphany
            }
            else if (Calc.IsYearBefore1992(Year))
            {
                ExcelHolidays[1].Date = new DateTime(Year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.LeapYearAdjustment);
                return ExcelHolidays[1]; ////ExcelHolidays[1] = Sunday After New Year
            }
            else
            {
                ExcelHolidays[2].Date = new DateTime(Year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.LeapYearAdjustment);
                return ExcelHolidays[2]; //ExcelHolidays[3] = Epiphany
            }
        }

        private List<Holiday> GetRemainingEpiphanyHolidays()
        {
            var remainingHolidays = new List<Holiday>();

            for (int i = 0; i < AmountOfEpiphanyHolidays; i++)
            {
                //If final loop and is before 1992
                if (i == AmountOfEpiphanyHolidays - 1 && !Calc.IsYearBefore1992(Year))
                {
                    ExcelHolidays[9].Date = GetFirstEpiphanyHoliday().Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(ExcelHolidays[9]); //ExcelHolidays[9] = Final Sunday After Epiphany
                }
                else
                {
                    ExcelHolidays[3 + i].Date = GetFirstEpiphanyHoliday().Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(ExcelHolidays[3 + i]); //ExcelHolidays[3 + i] = i Sunday After Epiphany
                }
            }
            return remainingHolidays;
        }

        private List<Holiday> GetAdjustedRemainingEpiphanyHolidays()
        {
            var remainingHolidays = new List<Holiday>();

            for (int i = 0; i < AmountOfEpiphanyHolidays; i++)
            {
                //If final loop and is before 1992
                if (i == AmountOfEpiphanyHolidays - 1 && Calc.IsYearBefore1992(Year))
                {
                    ExcelHolidays[9].Date = GetFirstEpiphanyHoliday().Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(ExcelHolidays[9]); //ExcelHolidays[9] = Final Sunday After Epiphany
                }
                else
                {
                    ExcelHolidays[3 + i + 1].Date = GetFirstEpiphanyHoliday().Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(ExcelHolidays[3 + i + 1]); //ExcelHolidays[3 + i + 1] = i Sunday After Epiphany
                }
            }
            return remainingHolidays;
        }


        private bool NeedAdjustedRemainingEpiphanyHolidays()
        {
            if (Calc.IsFirstEpiphanyHolidayOnSeventhOrEigthOfJanuary())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime GetDateOfFinalEpiphanyHoliday()
        {
            var easter = new Holiday(Calc.GetEasterDate(Year));

            if (IsFinalEpiphanyHolidayInFebruaryOrJanuary())
            {
                return Calc.GetEasterDate(Year).AddDays(-70 - Calc.LeapYearAdjustment);
            }
            else
            {
                return Calc.GetEasterDate(Year).AddDays(-70);
            }
        }
        private bool IsFinalEpiphanyHolidayInFebruaryOrJanuary()
        {
            if (Calc.GetEasterDate(Year).Month < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
