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
        public int WeekCode { get; set; }
        public int DOSError { get; set; }
        public bool LeapYear { get; set; }
        public int LeapYearCounter { get; set; }
        public int KeyNumberOne { get; set; }
        public int KeyNumberTwo { get; set; }
        public int KeyNumberThree { get; set; }
        public int AllSaintsNumber { get; set; }
        public bool Before1992 { get; set; }

        private int[] Divisors = new int[] { 19, 100, 4, 25, 3, 30, 4, 7, 451, 31 };
        public int[] Integers { get; set; } = new int[10];
        public int[] Rest { get; set; } = new int[10];

        public Calculations(int year)
        {
            CalculateVariables(year);
        }

        public void CalculateVariables(int year)
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

            if (year < 1992)
            {
                Before1992 = true;
            }
            else
            {
                Before1992 = false;
            }


            if (Integers[8] == 0)
            {
                CalendarNr = Rest[5] + (Rest[7] + 1);
            }
            else
            {
                CalendarNr = Rest[5];
            }

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
                LeapYear = true;
            }
            else
            {
                LeapYear = false;
            }

            if (Rest[1] == 0 && LeapYear == false)
            {
                DOSError = 1;
            }
            else
            {
                DOSError = 0;
            }

            if (DOSError == 0)
            {
                WeekCode = ((new DateOnly(year, Integers[9], Rest[9] + 1).DayOfYear - new DateTime(year, 1, 1).DayOfYear)) % 7;
            }
            else
            {
                WeekCode = (((new DateOnly(year, Integers[9], Rest[9] + 1).DayOfYear - new DateTime(year, 1, 1).DayOfYear)) + 1) % 7;
            }

            KeyNumberOne = (40 - CalendarNr) / 7;
            KeyNumberTwo = (CalendarNr - 1) / 7;
            //KeyNumberThree is the amount of days Christmas Sunday is after 2nd Christmas Day. If 6 or 0, it falls on another christmas holiday. 
            KeyNumberThree = CalendarNr % 7;

            //AllSaintsNumber will be used to replace the correct Sunday of the Trinity.
            AllSaintsNumber = 24 - KeyNumberTwo;

            //Things to do
            //Add consideration for before 1992 and H3K
            //If the first Sunday after New Year is 2-6 it is 1.s.e.NewYear before 1992, or H3K from 1992. 7-8 = First.S.E.H3K
            //Add consideration for ChristmasSunday

        }
    }

}
