namespace AdventOfCode.Day3;

public class RuckSackSorter
{
    public void GoThroughTheSacks(string[] rucksacks)
    {
        var priorities = new List<int>();
        foreach (var rucksack in rucksacks)
        {
            var compartment1 = rucksack.Take(rucksack.Length / 2);
            var compartment2 = rucksack.Skip(rucksack.Length / 2);

            foreach (var item in compartment1)
            {
                if (compartment2.Contains(item))
                {
                    priorities.Add(GetPriority(item));
                    break;
                }
            }
        }
        
        Console.WriteLine($"The priorities of the items in the rucksacks are {string.Join(',', priorities)}");
        Console.WriteLine($"The sum of these priorities are {priorities.Sum()}");
    }
    
    public void FindTheBadges(string[] rucksacks)
    {
        var priorities = new List<int>();
        var currentGroup = 0;

        while (currentGroup * 3 < rucksacks.Length)
        {
            var group = rucksacks.Skip(currentGroup * 3).Take(3);

            var firstRuckSack = group.First().ToCharArray();

            foreach (var item in firstRuckSack)
            {
                var isInSecondRucksack = group.ElementAt(1).Contains(item);
                var isInThirdRucksack = group.ElementAt(2).Contains(item);

                if (isInSecondRucksack && isInThirdRucksack)
                {
                    priorities.Add(GetPriority(item));
                    break;
                }
            }

            currentGroup++;
        }
        
        Console.WriteLine($"The priorities of the badges in the rucksacks are {string.Join(',', priorities)}");
        Console.WriteLine($"The sum of these priorities are {priorities.Sum()}");
    }

    public static int GetPriority(char item)
    {
        var asciiCode = (int) item;
        var priority = 0;
        if (asciiCode < 91)
        {
            priority = asciiCode - 38;
        }

        if (asciiCode > 96)
        {
            priority = asciiCode - 96;
        }

        return priority;
    }
}
