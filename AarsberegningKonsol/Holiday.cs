using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Holiday
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Holiday()
        {

        }
        public Holiday(string name)
        {
            Name = name;
        }
        public Holiday(DateTime date)
        {
            Date = date;
        }
    }
}
