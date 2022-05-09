using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class HolidaysCalculator
    {
        public Calculations calc { get; set; }

        public HolidaysCalculator(int year)
        {
            calc = new Calculations(year);
        }

        public List<Holiday> CalculateEpiphany()
        {
            return null;
        }
    }
}
