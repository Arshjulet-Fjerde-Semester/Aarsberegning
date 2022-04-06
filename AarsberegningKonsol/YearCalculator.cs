using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class YearCalculator
    {
        public List<Event> Events { get; set; }
        public List<Event> DisplayEvents { get; set; }
        public Calculations Calc { get; set; }

        public YearCalculator()
        {
            Calc = new Calculations(DateTime.Now.Year);
            DisplayEvents = new List<Event>();

            Events = new List<Event>()
            {
                new Event("Nytårsdag"), //0
                new Event("SENytår"),
                new Event("H3K"),
                new Event("FørsteSEH3K"),
                new Event("AndenSEH3K"),
                new Event("TredjeSEH3K"), //5
                new Event("FjerdeSEH3K"),
                new Event("FemteSEH3K"),
                new Event("SjetteSEH3K"),
                new Event("SidsteSEH3K"),
                new Event("Septuagesima"), //10
                new Event("Sexagesima"),
                new Event("Fastelavn"),
                new Event("FørsteSIFasten"),
                new Event("AndenSIFasten"),
                new Event("TredjeSIFasten"), //15
                new Event("Midfaste"),
                new Event("MariaeBebudelse"),
                new Event("Palmesøndag"),
                new Event("Skærtorsdag"),
                new Event("Langfredag"), //20
                new Event("Påskedag"),
                new Event("AndenPåskedag"),
                new Event("FørsteSEPåske"),
                new Event("AndenSEPåske"),
                new Event("TredjeSEPåske"), //25
                new Event("Bededag"),
                new Event("FjerdeSEPåske"),
                new Event("FemteSEPåske"),
                new Event("KristiHimmelfart"),
                new Event("SjetteSEPåske"), //30
                new Event("Pinsedag"),
                new Event("AndenPinsedag"),
                new Event("Trinitatis"),
                new Event("FørsteSETrin"),
                new Event("AndenSETrin"), //35
                new Event("TredjeSETrin"),
                new Event("FjerdeSETrin"),
                new Event("FemteSETrin"),
                new Event("SjetteSETrin"),
                new Event("SyvendeSETrin"), //40
                new Event("OttendeSETrin"),
                new Event("NiendeSETrin"),
                new Event("TiendeSETrin"),
                new Event("ElveteSETrin"),
                new Event("TolvteSETrin"), //45
                new Event("TrettendeSETrin"),
                new Event("FjortendeSETrin"),
                new Event("FemtendeSETrin"),
                new Event("SekstendeSETrin"),
                new Event("SyttendeSETrin"), //50
                new Event("AttendeSETrin"),
                new Event("NittendeSETrin"),
                new Event("TyvendeSETrin"),
                new Event("EnogtyvendeSETrin"),
                new Event("ToogtyvendeSETrin"), //55
                new Event("TreogtyvendeSETrin"),
                new Event("FireogtyvendeSETrin"),
                new Event("FemogtyvendeSETrin"),
                new Event("SeksogtyvendeSETrin"),
                new Event("SyvogtyvendeSETrin"), //60
                new Event("FørsteSIAdvent"),
                new Event("AndenSIAdvent"),
                new Event("TredjeSIAdvent"),
                new Event("FjerdeSIAdvent"),
                new Event("Juledag"), //65
                new Event("Andenjuledag"),
                new Event("Julesøndag"),
                new Event("Allehelgen"),
                new Event("SidsteSIKirkeaaret") //69
            };
        }

        public void CalculateChurchYear(int year)
        {
            Calc.CalculateVariables(year);

            if (DisplayEvents.Count != 0)
            {
                DisplayEvents.Clear();
            }

            //Nytårsdag
            Events[0].Date = new DateOnly(year, 1, 1);
            DisplayEvents.Add(Events[0]); //Nytårsdag added

            //Påskedag - Not yet added to DisplayEvents
            Events[21].Date = new DateOnly(year, Calc.Integers[9], Calc.Rest[9] + 1);

            //Loop Number for remaining H3K Sundays
            int loopNumber;
            if (Calc.WeekCode == 0 || Calc.WeekCode == 6)
            {
                loopNumber = ((Events[21].Date.DayOfYear - 70) / 7) - 1;
            }
            else
            {
                loopNumber = (Events[21].Date.DayOfYear - 70) / 7;
            }

            //SENytår/H3K/FørsteSEH3K//
            //We check if the WeekCode is 0 or 6, as it decides whether the first entry is H3K/SENytår or FørsteSEH3K

            //FørsteSEH3K
            if (Calc.WeekCode == 0)
            {
                Events[3].Date = new DateOnly(year, 1, 8 - Calc.DOSError);
                DisplayEvents.Add(Events[3]); //FørsteSEH3K added

                //Loop to add remaining H3K Themed days.
                for (int i = 0; i < loopNumber; i++)
                {
                    //This if statement is to make the final day SidsteSEH3K if it is after 1991. If not then the loop just adds the next Sunday in the line.
                    if (i == loopNumber - 1 && !Calc.Before1992)
                    {
                        Events[9].Date = Events[3 + i].Date.AddDays(7);
                        DisplayEvents.Add(Events[9]); //SidsteSEH3K added
                    }
                    else
                    {
                        Events[4 + i].Date = Events[3 + i].Date.AddDays(7);
                        DisplayEvents.Add(Events[4 + i]); //Remaining Sundays after FørsteSEH3K added
                    }
                }
            }
            //FørsteSEH3K
            else if (Calc.WeekCode == 6)
            {
                Events[3].Date = new DateOnly(year, 1, 7 - Calc.DOSError);
                DisplayEvents.Add(Events[3]);

                //Loop to add remaining H3K Themed days.
                for (int i = 0; i < loopNumber; i++)
                {
                    if (i == loopNumber - 1 && !Calc.Before1992)
                    {
                        Events[9].Date = Events[3 + i].Date.AddDays(7);
                        DisplayEvents.Add(Events[9]); //SidsteSEH3K added
                    }
                    else
                    {
                        Events[4 + i].Date = Events[3 + i].Date.AddDays(7);
                        DisplayEvents.Add(Events[4 + i]); //Remaining Sundays after FørsteSEH3K added
                    }
                }
            }
            //H3K and SENytår
            else
            {
                //SENytår, this day only existed before 1992.
                if (Calc.Before1992)
                {
                    Events[1].Date = new DateOnly(year, 1, 1 + Calc.WeekCode - Calc.DOSError);
                    Events[2].Date = new DateOnly(year, 1, 1 + Calc.WeekCode - Calc.DOSError); //We only set this date here, to be used in the loop.
                    DisplayEvents.Add(Events[1]); //SENytår

                    //Loop to add remaining H3K Themed days.
                    for (int i = 0; i < loopNumber; i++)
                    {
                        Events[3 + i].Date = Events[2 + i].Date.AddDays(7);
                        DisplayEvents.Add(Events[3 + i]); //Remaining Sundays after SENytår added
                    }
                }
                //H3K
                else
                {
                    Events[2].Date = new DateOnly(year, 1, 1 + Calc.WeekCode - Calc.DOSError);
                    DisplayEvents.Add(Events[2]); //H3K Added

                    //Loop to add remaining H3K Themed days.
                    for (int i = 0; i < loopNumber; i++)
                    {
                        if (i == loopNumber - 1)
                        {
                            //SidsteSEH3K
                            Events[9].Date = Events[2 + i].Date.AddDays(7);
                            DisplayEvents.Add(Events[9]); //SidsteSEH3K added
                        }
                        else
                        {
                            //Sundays after H3K
                            Events[3 + i].Date = Events[2 + i].Date.AddDays(7);
                            DisplayEvents.Add(Events[3 + i]); //Remaining Sundays after H3K added
                        }
                    }
                }

            }

            //Adds Septuagesima, Sexagesima, Fastelavn, 1-3 SIFasten, Midfaste, Mariæ Bebudelse and Palmesøndag.
            for (int i = 10; i < 19; i++)
            {
                Events[i].Date = DisplayEvents.Last().Date.AddDays(7);
                DisplayEvents.Add(Events[i]);
            }

            //Skærtorsdag
            Events[19].Date = DisplayEvents.Last().Date.AddDays(4);
            DisplayEvents.Add(Events[19]);

            //Langfredag
            Events[20].Date = DisplayEvents.Last().Date.AddDays(1);
            DisplayEvents.Add(Events[20]);

            //Påskedag added
            DisplayEvents.Add(Events[21]);

            //AndenPåskedag
            Events[22].Date = DisplayEvents.Last().Date.AddDays(1);
            DisplayEvents.Add(Events[22]);

            //1-3 SEPåske
            for (int i = 23; i < 26; i++)
            {
                if (i == 23)
                {
                    Events[i].Date = DisplayEvents.Last().Date.AddDays(6);
                    DisplayEvents.Add(Events[i]);
                }
                else
                {
                    Events[i].Date = DisplayEvents.Last().Date.AddDays(7);
                    DisplayEvents.Add(Events[i]);
                }
            }

            //Bededag
            Events[26].Date = DisplayEvents.Last().Date.AddDays(5);
            DisplayEvents.Add(Events[26]);

            //4SEPåske
            Events[27].Date = DisplayEvents.Last().Date.AddDays(2);
            DisplayEvents.Add(Events[27]);

            //5SEPåske
            Events[28].Date = DisplayEvents.Last().Date.AddDays(7);
            DisplayEvents.Add(Events[28]);

            //KristiHimmelfart
            Events[29].Date = DisplayEvents.Last().Date.AddDays(4);
            DisplayEvents.Add(Events[29]);

            //6SEPåske
            Events[30].Date = DisplayEvents.Last().Date.AddDays(3);
            DisplayEvents.Add(Events[30]);

            //Pinsedag
            Events[31].Date = DisplayEvents.Last().Date.AddDays(7);
            DisplayEvents.Add(Events[31]);

            //AndenPinsedag
            Events[32].Date = DisplayEvents.Last().Date.AddDays(1);
            DisplayEvents.Add(Events[32]);

            //Trinitatis
            Events[33].Date = DisplayEvents.Last().Date.AddDays(6);
            DisplayEvents.Add(Events[33]);

            //LoopNumber is set to add all the Sundays after Trinitatis
            //KeyNumberOne is between 0-5, which makes the loopNumber between 22-27.
            loopNumber = 22 + Calc.KeyNumberOne;


            for (int i = 34; i < 34 + loopNumber; i++)
            {
                //This makes sure that the AllSaints (Allehelgen) is replacing the correct Sunday after Trinitatis.
                if (i == 33 + Calc.AllSaintsNumber)
                {
                    Events[68].Date = DisplayEvents.Last().Date.AddDays(7);
                    DisplayEvents.Add(Events[68]);
                }
                //This makes sure that if it is after 1992, and it is the final loop number, it adds SidsteSIKirkekaaret.
                else if (i == 33 + loopNumber && !Calc.Before1992)
                {
                    Events[69].Date = DisplayEvents.Last().Date.AddDays(7);
                    DisplayEvents.Add(Events[69]);
                }
                //Here we add all the normal Sundays after Trinitatis.
                else
                {
                    Events[i].Date = DisplayEvents.Last().Date.AddDays(7);
                    DisplayEvents.Add(Events[i]);
                }
            }

            //This adds the 4 advent Sundays.
            for (int i = 61; i < 65; i++)
            {
                Events[i].Date = DisplayEvents.Last().Date.AddDays(7);
                DisplayEvents.Add(Events[i]);
            }

            //Adding Christmas Day. (Juledag)
            Events[65].Date = new DateOnly(year, 12, 25);
            DisplayEvents.Add(Events[65]);

            //Adding Second Christmas Day. (Andenjuledag)
            Events[66].Date = new DateOnly(year, 12, 26);
            DisplayEvents.Add(Events[66]);

            //Adding Christ Sunday (Julesøndag) by adding 7 days to Fourth Advent Sunday, if it does not happen on one of the Christmas Days.
            if (Calc.KeyNumberThree != 0 && Calc.KeyNumberThree != 6)
            {
                Events[67].Date = Events[64].Date.AddDays(7);
                DisplayEvents.Add(Events[67]);
            }
        }
    }
}
