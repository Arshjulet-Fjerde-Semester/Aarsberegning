using AarsberegningKonsol;

YearCalculator a = new YearCalculator();

int inputNumber;

do
{
    string? input = Console.ReadLine();
    inputNumber = Convert.ToInt32(input);

    a.CalculateChurchYear(inputNumber);

    Console.Clear();

    for (int i = 0; i < a.DisplayEvents.Count; i++)
    {
        Console.WriteLine("Name: " + a.DisplayEvents[i].Name + ", Date: " + a.DisplayEvents[i].Date.ToString("dd-MM-yyyy"));
    }

    Console.WriteLine();
    Console.WriteLine("UgeKode: " + a.Calc.WeekCode);
    Console.WriteLine("Number of holidays: " + a.DisplayEvents.Count);

} while (inputNumber != 100);
