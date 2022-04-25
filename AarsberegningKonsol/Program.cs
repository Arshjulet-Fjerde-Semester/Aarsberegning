using AarsberegningKonsol;

YearCalculator a = new YearCalculator();

int inputNumber;

do
{
    string? input = Console.ReadLine();
    inputNumber = Convert.ToInt32(input);

    a.CalculateChurchYear(inputNumber);

    Console.Clear();

    for (int i = 0; i < a.DisplayHolidays.Count; i++)
    {
        Console.WriteLine("Name: " + a.DisplayHolidays[i].Name + ", Date: " + a.DisplayHolidays[i].Date.ToString("dd-MM-yyyy"));
    }

    Console.WriteLine();
    Console.WriteLine("UgeKode: " + a.Calc.WeekCode);
    Console.WriteLine("Number of holidays: " + a.DisplayHolidays.Count);

} while (inputNumber != 100);
