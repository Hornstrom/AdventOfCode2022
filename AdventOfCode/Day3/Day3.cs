namespace AdventOfCode.Day3;

public class Day3 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day3\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day3\test_data.txt");

    public void Part1()
    {
        var rucksackSorter = new RuckSackSorter();
        rucksackSorter.GoThroughTheSacks(DataTest);
        rucksackSorter.GoThroughTheSacks(Data);
    }

    public void Part2()
    {
        var rucksackSorter = new RuckSackSorter();
        rucksackSorter.FindTheBadges(DataTest);
        rucksackSorter.FindTheBadges(Data);
    }
}
