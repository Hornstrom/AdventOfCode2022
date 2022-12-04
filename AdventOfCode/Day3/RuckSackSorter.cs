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
    // public int GetPriority(char item)
    // {
    //     return item switch
    //     {
    //         'a' => 1,
    //         'a' => 2,
    //         'a' => 3,
    //         'a' => 4,
    //         'a' => 5,
    //         'a' => 6,
    //         'a' => 7,
    //         'a' => 8,
    //         'a' => 9,
    //         'a' => 10,
    //         'a' => 11,
    //         'a' => 12,
    //         'a' => 13,
    //         'a' => 14,
    //         'a' => 15,
    //         'a' => 16,
    //         'a' => 17,
    //         'a' => 18,
    //         'a' => 19,
    //         'a' => 20,
    //         'a' => 21,
    //         'a' => 22,
    //         'a' => 23,
    //         'a' => 24,
    //         'a' => 25,
    //         'a' => 26,
    //         'a' => 27,
    //         'a' => 28,
    //         'a' => 29,
    //         'a' => 30,
    //         'a' => 31,
    //         'a' => 32,
    //         'a' => 33,
    //         'a' => 34,
    //         'a' => 35,
    //         'a' => 36,
    //         'a' => 2,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         'a' => 1,
    //         _ => throw new Exception("Unexpected char")
    //     };
    // }
    
}
