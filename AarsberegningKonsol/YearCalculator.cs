using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AarsberegningKonsol
{
    public class YearCalculator
    {
        public List<Holiday> Holidays { get; set; }
        public List<Holiday> DisplayHolidays { get; set; }
        public Calculations Calc { get; set; }
        public Excel Excel { get; set; }
        public Epiphany Epiphany { get; set; }

        public YearCalculator()
        {
            Calc = new Calculations(DateTime.Now.Year);
            DisplayHolidays = new List<Holiday>();
            Excel = new Excel();

            Holidays = Excel.GetAllHolidays();

            //Holidays = new List<Holiday>()
            //{
            //    new Holiday("Nytårsdag"), //0
            //    new Holiday("SENytår"),
            //    new Holiday("H3K"),
            //    new Holiday("FørsteSEH3K"),
            //    new Holiday("AndenSEH3K"),
            //    new Holiday("TredjeSEH3K"), //5
            //    new Holiday("FjerdeSEH3K"),
            //    new Holiday("FemteSEH3K"),
            //    new Holiday("SjetteSEH3K"),
            //    new Holiday("SidsteSEH3K"),
            //    new Holiday("Septuagesima"), //10
            //    new Holiday("Sexagesima"),
            //    new Holiday("Fastelavn"),
            //    new Holiday("FørsteSIFasten"),
            //    new Holiday("AndenSIFasten"),
            //    new Holiday("TredjeSIFasten"), //15
            //    new Holiday("Midfaste"),
            //    new Holiday("MariaeBebudelse"),
            //    new Holiday("Palmesøndag"),
            //    new Holiday("Skærtorsdag"),
            //    new Holiday("Langfredag"), //20
            //    new Holiday("Påskedag"),
            //    new Holiday("AndenPåskedag"),
            //    new Holiday("FørsteSEPåske"),
            //    new Holiday("AndenSEPåske"),
            //    new Holiday("TredjeSEPåske"), //25
            //    new Holiday("Bededag"),
            //    new Holiday("FjerdeSEPåske"),
            //    new Holiday("FemteSEPåske"),
            //    new Holiday("KristiHimmelfart"),
            //    new Holiday("SjetteSEPåske"), //30
            //    new Holiday("Pinsedag"),
            //    new Holiday("AndenPinsedag"),
            //    new Holiday("Trinitatis"),
            //    new Holiday("FørsteSETrin"),
            //    new Holiday("AndenSETrin"), //35
            //    new Holiday("TredjeSETrin"),
            //    new Holiday("FjerdeSETrin"),
            //    new Holiday("FemteSETrin"),
            //    new Holiday("SjetteSETrin"),
            //    new Holiday("SyvendeSETrin"), //40
            //    new Holiday("OttendeSETrin"),
            //    new Holiday("NiendeSETrin"),
            //    new Holiday("TiendeSETrin"),
            //    new Holiday("ElveteSETrin"),
            //    new Holiday("TolvteSETrin"), //45
            //    new Holiday("TrettendeSETrin"),
            //    new Holiday("FjortendeSETrin"),
            //    new Holiday("FemtendeSETrin"),
            //    new Holiday("SekstendeSETrin"),
            //    new Holiday("SyttendeSETrin"), //50
            //    new Holiday("AttendeSETrin"),
            //    new Holiday("NittendeSETrin"),
            //    new Holiday("TyvendeSETrin"),
            //    new Holiday("EnogtyvendeSETrin"),
            //    new Holiday("ToogtyvendeSETrin"), //55
            //    new Holiday("TreogtyvendeSETrin"),
            //    new Holiday("FireogtyvendeSETrin"),
            //    new Holiday("FemogtyvendeSETrin"),
            //    new Holiday("SeksogtyvendeSETrin"),
            //    new Holiday("SyvogtyvendeSETrin"), //60
            //    new Holiday("FørsteSIAdvent"),
            //    new Holiday("AndenSIAdvent"),
            //    new Holiday("TredjeSIAdvent"),
            //    new Holiday("FjerdeSIAdvent"),
            //    new Holiday("Juledag"), //65
            //    new Holiday("Andenjuledag"),
            //    new Holiday("Julesøndag"),
            //    new Holiday("Allehelgen"),
            //    new Holiday("SidsteSIKirkeaaret") //69
            //};
        }

        public void CalculateChurchYear(int year)
        {
            Calc.CalculateVariables(year);
            Epiphany = new Epiphany(year, Calc);

            if (DisplayHolidays.Count != 0)
            {
                DisplayHolidays.Clear();
            }

            //Nytårsdag
            Holidays[0].Date = new DateTime(year, 1, 1);
            DisplayHolidays.Add(Holidays[0]); //Nytårsdag added

            //Påskedag - Not yet added to DisplayHolidays
            Holidays[21].Date = new DateTime(year, Calc.Integers[9], Calc.Rest[9] + 1);

            DisplayHolidays.AddRange(Epiphany.GetAllEpiphanyHolidays(Holidays, year));

            //Loop Number for remaining H3K Sundays
            int loopNumber;
            if (Calc.DaysAfter1stOfJanuaryUntilFirstSunday == 0 || Calc.DaysAfter1stOfJanuaryUntilFirstSunday == 6)
            {
                loopNumber = ((Holidays[21].Date.DayOfYear - 70) / 7) - 1;
            }
            else
            {
                loopNumber = (Holidays[21].Date.DayOfYear - 70) / 7;
            }

            ////SENytår/H3K/FørsteSEH3K//
            ////We check if the DaysAfter1stOfJanuaryUntilFirstSunday is 0 or 6, as it decides whether the first entry is H3K/SENytår or FørsteSEH3K

            ////FørsteSEH3K
            //if (Calc.DaysAfter1stOfJanuaryUntilFirstSunday == 0)
            //{
            //    Holidays[3].Date = new DateTime(year, 1, 8 - Calc.DOSError);
            //    DisplayHolidays.Add(Holidays[3]); //FørsteSEH3K added

            //    //Loop to add remaining H3K Themed days.
            //    for (int i = 0; i < loopNumber; i++)
            //    {
            //        //This if statement is to make the final day SidsteSEH3K if it is after 1991. If not then the loop just adds the next Sunday in the line.
            //        if (i == loopNumber - 1 && !Calc.Before1992)
            //        {
            //            Holidays[9].Date = Holidays[3 + i].Date.AddDays(7);
            //            DisplayHolidays.Add(Holidays[9]); //SidsteSEH3K added
            //        }
            //        else
            //        {
            //            Holidays[4 + i].Date = Holidays[3 + i].Date.AddDays(7);
            //            DisplayHolidays.Add(Holidays[4 + i]); //Remaining Sundays after FørsteSEH3K added
            //        }
            //    }
            //}
            ////FørsteSEH3K
            //else if (Calc.DaysAfter1stOfJanuaryUntilFirstSunday == 6)
            //{
            //    Holidays[3].Date = new DateTime(year, 1, 7 - Calc.DOSError);
            //    DisplayHolidays.Add(Holidays[3]);

            //    //Loop to add remaining H3K Themed days.
            //    for (int i = 0; i < loopNumber; i++)
            //    {
            //        if (i == loopNumber - 1 && !Calc.Before1992)
            //        {
            //            Holidays[9].Date = Holidays[3 + i].Date.AddDays(7);
            //            DisplayHolidays.Add(Holidays[9]); //SidsteSEH3K added
            //        }
            //        else
            //        {
            //            Holidays[4 + i].Date = Holidays[3 + i].Date.AddDays(7);
            //            DisplayHolidays.Add(Holidays[4 + i]); //Remaining Sundays after FørsteSEH3K added
            //        }
            //    }
            //}
            ////H3K and SENytår
            //else
            //{
            //    //SENytår, this day only existed before 1992.
            //    if (Calc.Before1992)
            //    {
            //        Holidays[1].Date = new DateTime(year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.DOSError);
            //        Holidays[2].Date = new DateTime(year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.DOSError); //We only set this date here, to be used in the loop.
            //        DisplayHolidays.Add(Holidays[1]); //SENytår

            //        //Loop to add remaining H3K Themed days.
            //        for (int i = 0; i < loopNumber; i++)
            //        {
            //            Holidays[3 + i].Date = Holidays[2 + i].Date.AddDays(7);
            //            DisplayHolidays.Add(Holidays[3 + i]); //Remaining Sundays after SENytår added
            //        }
            //    }
            //    //H3K
            //    else
            //    {
            //        Holidays[2].Date = new DateTime(year, 1, 1 + Calc.DaysAfter1stOfJanuaryUntilFirstSunday - Calc.DOSError);
            //        DisplayHolidays.Add(Holidays[2]); //H3K Added

            //        //Loop to add remaining H3K Themed days.
            //        for (int i = 0; i < loopNumber; i++)
            //        {
            //            if (i == loopNumber - 1)
            //            {
            //                //SidsteSEH3K
            //                Holidays[9].Date = Holidays[2 + i].Date.AddDays(7);
            //                DisplayHolidays.Add(Holidays[9]); //SidsteSEH3K added
            //            }
            //            else
            //            {
            //                //Sundays after H3K
            //                Holidays[3 + i].Date = Holidays[2 + i].Date.AddDays(7);
            //                DisplayHolidays.Add(Holidays[3 + i]); //Remaining Sundays after H3K added
            //            }
            //        }
            //    }

            //}

            //Adds Septuagesima, Sexagesima, Fastelavn, 1-3 SIFasten, Midfaste, Mariæ Bebudelse and Palmesøndag.
            for (int i = 10; i < 19; i++)
            {
                Holidays[i].Date = DisplayHolidays.Last().Date.AddDays(7);
                DisplayHolidays.Add(Holidays[i]);
            }

            //Skærtorsdag
            Holidays[19].Date = DisplayHolidays.Last().Date.AddDays(4);
            DisplayHolidays.Add(Holidays[19]);

            //Langfredag
            Holidays[20].Date = DisplayHolidays.Last().Date.AddDays(1);
            DisplayHolidays.Add(Holidays[20]);

            //Påskedag added
            DisplayHolidays.Add(Holidays[21]);

            //AndenPåskedag
            Holidays[22].Date = DisplayHolidays.Last().Date.AddDays(1);
            DisplayHolidays.Add(Holidays[22]);

            //1-3 SEPåske
            for (int i = 23; i < 26; i++)
            {
                if (i == 23)
                {
                    Holidays[i].Date = DisplayHolidays.Last().Date.AddDays(6);
                    DisplayHolidays.Add(Holidays[i]);
                }
                else
                {
                    Holidays[i].Date = DisplayHolidays.Last().Date.AddDays(7);
                    DisplayHolidays.Add(Holidays[i]);
                }
            }

            //Bededag
            Holidays[26].Date = DisplayHolidays.Last().Date.AddDays(5);
            DisplayHolidays.Add(Holidays[26]);

            //4SEPåske
            Holidays[27].Date = DisplayHolidays.Last().Date.AddDays(2);
            DisplayHolidays.Add(Holidays[27]);

            //5SEPåske
            Holidays[28].Date = DisplayHolidays.Last().Date.AddDays(7);
            DisplayHolidays.Add(Holidays[28]);

            //KristiHimmelfart
            Holidays[29].Date = DisplayHolidays.Last().Date.AddDays(4);
            DisplayHolidays.Add(Holidays[29]);

            //6SEPåske
            Holidays[30].Date = DisplayHolidays.Last().Date.AddDays(3);
            DisplayHolidays.Add(Holidays[30]);

            //Pinsedag
            Holidays[31].Date = DisplayHolidays.Last().Date.AddDays(7);
            DisplayHolidays.Add(Holidays[31]);

            //AndenPinsedag
            Holidays[32].Date = DisplayHolidays.Last().Date.AddDays(1);
            DisplayHolidays.Add(Holidays[32]);

            //Trinitatis
            Holidays[33].Date = DisplayHolidays.Last().Date.AddDays(6);
            DisplayHolidays.Add(Holidays[33]);

            //LoopNumber is set to add all the Sundays after Trinitatis
            //KeyNumberOne is between 0-5, which makes the loopNumber between 22-27.
            loopNumber = 22 + Calc.KeyNumberOne;


            for (int i = 34; i < 34 + loopNumber; i++)
            {
                //This makes sure that the AllSaints (Allehelgen) is replacing the correct Sunday after Trinitatis.
                if (i == 33 + Calc.AllSaintsNumber)
                {
                    Holidays[68].Date = DisplayHolidays.Last().Date.AddDays(7);
                    DisplayHolidays.Add(Holidays[68]);
                }
                //This makes sure that if it is after 1992, and it is the final loop number, it adds SidsteSIKirkekaaret.
                else if (i == 33 + loopNumber && !Calc.IsYearBefore1992(year))
                {
                    Holidays[69].Date = DisplayHolidays.Last().Date.AddDays(7);
                    DisplayHolidays.Add(Holidays[69]);
                }
                //Here we add all the normal Sundays after Trinitatis.
                else
                {
                    Holidays[i].Date = DisplayHolidays.Last().Date.AddDays(7);
                    DisplayHolidays.Add(Holidays[i]);
                }
            }

            //This adds the 4 advent Sundays.
            for (int i = 61; i < 65; i++)
            {
                Holidays[i].Date = DisplayHolidays.Last().Date.AddDays(7);
                DisplayHolidays.Add(Holidays[i]);
            }

            //Adding Christmas Day. (Juledag)
            Holidays[65].Date = new DateTime(year, 12, 25);
            DisplayHolidays.Add(Holidays[65]);

            //Adding Second Christmas Day. (Andenjuledag)
            Holidays[66].Date = new DateTime(year, 12, 26);
            DisplayHolidays.Add(Holidays[66]);

            //Adding Christ Sunday (Julesøndag) by adding 7 days to Fourth Advent Sunday, if it does not happen on one of the Christmas Days.
            if (Calc.KeyNumberThree != 0 && Calc.KeyNumberThree != 6)
            {
                Holidays[67].Date = Holidays[64].Date.AddDays(7);
                DisplayHolidays.Add(Holidays[67]);
            }
        }
    }
}
