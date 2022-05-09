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
        public Calculations Calc { get; set; }

        public Epiphany(int year, Calculations calc)
        {
            EpiphanyHolidays = new List<Holiday>();
            Calc = calc;
            CalculateAmountOfEpiphanySundays(year);
        }

        private void CalculateAmountOfEpiphanySundays(int year)
        {
            if (Calc.IsFirstEpiphanyHolidayOnSeventhOrEigthOfJanuary())
            {
                AmountOfEpiphanyHolidays = ((Calc.GetEasterDate(year).DayOfYear - 70) / 7) - 1;
            }
            else
            {
                AmountOfEpiphanyHolidays = (Calc.GetEasterDate(year).DayOfYear - 70) / 7;
            }
        }

        private Holiday GetFirstEpiphanyHoliday(List<Holiday> holidays, int year)
        {
            if (Calc.IsFirstEpiphanyHolidayOnEighthOfJanuary())
            {
                holidays[3].Date = new DateTime(year, 1, 8 - Calc.LeapYearAdjustment);
                return holidays[3]; //holidays[3] = First Sunday After Epiphany
            }
            else if (Calc.IsFirstEpiphanyHolidayOnSeventhOfJanuary())
            {
                holidays[3].Date = new DateTime(year, 1, 7 - Calc.LeapYearAdjustment);
                return holidays[3]; //holidays[3] = First Sunday After Epiphany
            }
            else if (Calc.IsYearBefore1992(year))
            {
                holidays[1].Date = new DateTime(year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.LeapYearAdjustment);
                return holidays[1]; ////holidays[1] = Sunday After New Year
            }
            else
            {
                holidays[2].Date = new DateTime(year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.LeapYearAdjustment);
                return holidays[2]; //holidays[3] = Epiphany
            }
        }

        private List<Holiday> GetRemainingEpiphanyHolidays(List<Holiday> holidays, int year)
        {
            var remainingHolidays = new List<Holiday>();

            for (int i = 0; i < AmountOfEpiphanyHolidays; i++)
            {
                //If final loop and is before 1992
                if (i == AmountOfEpiphanyHolidays - 1 && !Calc.IsYearBefore1992(year))
                {
                    holidays[9].Date = GetFirstEpiphanyHoliday(holidays, year).Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(holidays[9]); //holidays[9] = Final Sunday After Epiphany
                }
                else
                {
                    holidays[3 + i].Date = GetFirstEpiphanyHoliday(holidays, year).Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(holidays[3 + i]); //holidays[3 + i] = i Sunday After Epiphany
                }
            }
            return remainingHolidays;
        }

        private List<Holiday> GetAdjustedRemainingEpiphanyHolidays(List<Holiday> holidays, int year)
        {
            var remainingHolidays = new List<Holiday>();

            for (int i = 0; i < AmountOfEpiphanyHolidays; i++)
            {
                //If final loop and is before 1992
                if (i == AmountOfEpiphanyHolidays - 1 && Calc.IsYearBefore1992(year))
                {
                    holidays[9].Date = GetFirstEpiphanyHoliday(holidays, year).Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(holidays[9]); //holidays[9] = Final Sunday After Epiphany
                }
                else
                {
                    holidays[3 + i + 1].Date = GetFirstEpiphanyHoliday(holidays, year).Date.AddDays(7 + 7 * i);
                    remainingHolidays.Add(holidays[3 + i + 1]); //holidays[3 + i + 1] = i Sunday After Epiphany
                }
            }
            return remainingHolidays;
        }

        public List<Holiday> GetAllEpiphanyHolidays(List<Holiday> holidays, int year)
        {
            EpiphanyHolidays.Add(GetFirstEpiphanyHoliday(holidays, year));

            if (NeedAdjustedRemainingEpiphanyHolidays())
            {
                EpiphanyHolidays.AddRange(GetAdjustedRemainingEpiphanyHolidays(holidays, year));
            }
            else
            {
                EpiphanyHolidays.AddRange(GetRemainingEpiphanyHolidays(holidays, year));

            }

            return EpiphanyHolidays;
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
    }
}
