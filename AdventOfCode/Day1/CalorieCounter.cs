namespace AdventOfCode.Day1;

public class CalorieCounter
{
    public string[] BackPackData = System.IO.File.ReadAllLines(@"Day1\data.txt");
    public string[] BackPackDataTest = System.IO.File.ReadAllLines(@"Day1\test_data.txt");

    public void WhoCarriesTheMost()
    {
        Console.WriteLine("Starting weight counting!");

        var currentHeavy = 0;
        var currentElf = 0;
        for (int i = 0; i < BackPackData.Length; i++)
        {
            if (BackPackData[i] == "")
            {
                if (currentElf > currentHeavy)
                {
                    currentHeavy = currentElf;
                }

                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(BackPackData[i]);
            }
        }
        
        Console.WriteLine($"Heaviest load na elf carries is {currentHeavy}");
    }
    
    public void TopThreeCarriesTheMost()
    {
        Console.WriteLine("Starting weight counting!");

        var allTheWeights = new List<int>();
        var currentElf = 0;
        for (int i = 0; i < BackPackData.Length; i++)
        {
            if (BackPackData[i] == "")
            {
                allTheWeights.Add(currentElf);

                currentElf = 0;
            }
            else
            {
                currentElf += int.Parse(BackPackData[i]);
            }
        }

        var topthree = allTheWeights.OrderByDescending(a => a).Take(3);
        foreach (var weight in topthree)
        {
            Console.WriteLine($"Top three loads {weight}");    
        }
        
        Console.WriteLine($"Result: {topthree.Sum(a => a)}");
    }
}
