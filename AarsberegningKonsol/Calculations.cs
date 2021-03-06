using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Calculations
    {
        public int CalendarNr { get; set; }
        public int DaysAfter1stOfJanuaryUntilFirstSunday { get; set; }
        public int LeapYearAdjustment { get; set; }
        public bool IsLeapYear { get; set; }
        public int LeapYearCounter { get; set; }
        private int[] Divisors { get; set; } = new int[] { 19, 100, 4, 25, 3, 30, 4, 7, 451, 31 };
        private int[] Integers { get; set; } = new int[10];
        private int[] Rest { get; set; } = new int[10];

        public Calculations(int year)
        {
            CalculateVariables(year);
        }

        public void CalculateVariables(int year)
        {
            CalculateIntegersAndRest(year);

            CalculateCalendarNr(year);

            IsYearLeapYear(year);

            IsYearMislabeledAsLeapYear(year);

            SetDaysAfter1stOfJanuaryUntilFirstSunday(year);
        }

        public bool IsFirstEpiphanyHolidayOnSeventhOrEigthOfJanuary()
        {
            if (DaysAfter1stOfJanuaryUntilFirstSunday == 0 || DaysAfter1stOfJanuaryUntilFirstSunday == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFirstEpiphanyHolidayOnSeventhOfJanuary()
        {
            if (DaysAfter1stOfJanuaryUntilFirstSunday == 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsFirstEpiphanyHolidayOnEighthOfJanuary()
        {
            if (DaysAfter1stOfJanuaryUntilFirstSunday == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsYearBefore1992(int year)
        {
            if (year < 1992)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime GetEasterDate(int year)
        {
            return new DateTime(year, Integers[9], Rest[9] + 1);
        }

        private void IsYearLeapYear(int year)
        {
            LeapYearCounter = 0;
            if ((year % 4 == 0))
            {
                LeapYearCounter++;
            }
            if (year % 100 > 0)
            {
                LeapYearCounter++;
            }
            if (year % 400 == 0)
            {
                LeapYearCounter++;
            }
            if (year % 3600 > 0)
            {
                LeapYearCounter++;
            }

            if (LeapYearCounter == 3)
            {
                IsLeapYear = true;
            }
            else
            {
                IsLeapYear = false;
            }
        }
        private bool IsYearMislabeledAsLeapYear(int year)
        {
            if (Rest[1] == 0 && !IsLeapYear)
            {
                LeapYearAdjustment = 1;
                return true;
            }
            else
            {
                LeapYearAdjustment = 0;
                return false;
            }
        }

        private void SetDaysAfter1stOfJanuaryUntilFirstSunday(int year)
        {
            if (IsYearMislabeledAsLeapYear(year))
            {
                DaysAfter1stOfJanuaryUntilFirstSunday = (((new DateTime(year, Integers[9], Rest[9] + 1).DayOfYear - new DateTime(year, 1, 1).DayOfYear)) + 1) % 7;
            }
            else
            {
                DaysAfter1stOfJanuaryUntilFirstSunday = ((new DateTime(year, Integers[9], Rest[9] + 1).DayOfYear - new DateTime(year, 1, 1).DayOfYear)) % 7;
            }
        }

        private void CalculateIntegersAndRest(int year)
        {
            Integers[0] = year / Divisors[0];
            Rest[0] = year % Divisors[0];

            Integers[1] = year / Divisors[1];
            Rest[1] = year % Divisors[1];

            Integers[2] = Integers[1] / Divisors[2];
            Rest[2] = Integers[1] % Divisors[2];

            Integers[3] = (Integers[1] + 8) / Divisors[3];
            Rest[3] = (Integers[1] + 8) % Divisors[3];

            Integers[4] = (Integers[1] - Integers[3] + 1) / Divisors[4];
            Rest[4] = (Integers[1] + 1) % Divisors[4];

            Integers[5] = (19 * Rest[0] + Integers[1] - Integers[2] - Integers[4] + 15) / Divisors[5];
            Rest[5] = (19 * Rest[0] + Integers[1] - Integers[2] - Integers[4] + 15) % Divisors[5];

            Integers[6] = Rest[1] / Divisors[6];
            Rest[6] = Rest[1] % Divisors[6];

            Integers[7] = (32 + 2 * Rest[2] + 2 * Integers[6] - Rest[5] - Rest[6]) / Divisors[7];
            Rest[7] = (32 + 2 * Rest[2] + 2 * Integers[6] - Rest[5] - Rest[6]) % Divisors[7];

            Integers[8] = (Rest[0] + 11 * Rest[5] + 22 * Rest[7]) / Divisors[8];
            Rest[8] = (Rest[0] + 11 * Rest[5] + 22 * Rest[7]) % Divisors[8];

            Integers[9] = (Rest[5] + Rest[7] - 7 * Integers[8] + 114) / Divisors[9];
            Rest[9] = (Rest[5] + Rest[7] - 7 * Integers[8] + 114) % Divisors[9];
        }

        public void CalculateCalendarNr(int year)
        {
            if (Integers[8] == 0)
            {
                CalendarNr = Rest[5] + (Rest[7] + 1);
            }
            else
            {
                CalendarNr = Rest[5];
            }
        }
    }

}
