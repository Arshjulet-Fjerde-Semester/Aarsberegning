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
        public DateOnly Date { get; set; }
        public Holiday()
        {

        }
        public Holiday(string name)
        {
            Name = name;
        }
        public Holiday(string name, DateOnly date)
        {
            Name = name;
            Date = date;
        }
    }
}
