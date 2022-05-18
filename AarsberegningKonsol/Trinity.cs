using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Trinity
    {
        public List<Holiday> TrinityHolidays { get; set; }
        public List<Holiday> ExcelHolidays { get; set; }
        public Calculations Calc { get; set; }
        public Easter Easter { get; set; }
        public int Year { get; set; }
        public int AmountOfTrinitySundaysAfterTrinity { get; set; }
        public int AllSaintsReplacesThisSundayAfterTrinity { get; set; }
        public DateTime FinalTrinityHoliday { get; set; }

        public Trinity(int year, Calculations calc, List<Holiday> holidays)
        {
            TrinityHolidays = new List<Holiday>();
            ExcelHolidays = holidays;
            Calc = calc;
            Year = year;
            Easter = new Easter(year, calc, holidays);
            CalculateAmountOfTrinitySundaysAfterFirst();
            CalculateAllSaintsReplacesThisSundayAfterTrinity();
            CalculateFinalTrinityHoliday();
        }
        public List<Holiday> GetAllTrinityHolidays()
        {
            TrinityHolidays.Add(GetTrinity());
            TrinityHolidays.AddRange(GetRemainingTrinityHolidays());

            return TrinityHolidays;
        }
        private Holiday GetTrinity()
        {
            ExcelHolidays[33].Date = Easter.GetWhitMonday().Date.AddDays(6);
            return ExcelHolidays[33];
        }
        private List<Holiday> GetRemainingTrinityHolidays()
        {
            var remainingTrinityHolidays = new List<Holiday>();

            for (int i = 0; i < AmountOfTrinitySundaysAfterTrinity; i++)
            {
                if (IsAllSaints(i))
                {
                    ExcelHolidays[68].Date = GetTrinity().Date.AddDays(7 + 7 * i);
                    remainingTrinityHolidays.Add(ExcelHolidays[68]);
                }
                else if (IsFinalSundayInChurchYear(i))
                {
                    ExcelHolidays[69].Date = GetTrinity().Date.AddDays(7 + 7 * i);
                    remainingTrinityHolidays.Add(ExcelHolidays[69]);
                }
                else
                {
                    ExcelHolidays[34 + i].Date = GetTrinity().Date.AddDays(7 + 7 * i);
                    remainingTrinityHolidays.Add(ExcelHolidays[34 + i]);
                }
            }
            return remainingTrinityHolidays;
        }

        private void CalculateAmountOfTrinitySundaysAfterFirst()
        {
            AmountOfTrinitySundaysAfterTrinity = 22 + (40 - Calc.CalendarNr) / 7;
        }
        private void CalculateAllSaintsReplacesThisSundayAfterTrinity()
        {
            AllSaintsReplacesThisSundayAfterTrinity = 24 - (Calc.CalendarNr - 1) / 7;
        }
        private bool IsAllSaints(int number)
        {
            if (number == AllSaintsReplacesThisSundayAfterTrinity - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsFinalSundayInChurchYear(int number)
        {
            if (number == AmountOfTrinitySundaysAfterTrinity - 1 && !Calc.IsYearBefore1992(Year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CalculateFinalTrinityHoliday()
        {
            if (Calc.IsYearBefore1992(Year))
            {
               FinalTrinityHoliday = GetTrinity().Date.AddDays(7 * AmountOfTrinitySundaysAfterTrinity);

            }
            else
            {
                FinalTrinityHoliday = GetTrinity().Date.AddDays(7 * AmountOfTrinitySundaysAfterTrinity);
            }
        }
    }
}
