using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class Event
    {
        public string Name { get; set; }
        public DateOnly Date { get; set; }

        public Event(string name)
        {
            Name = name;
        }
        public Event(string name, DateOnly date)
        {
            Name = name;
            Date = date;
        }
    }
}
