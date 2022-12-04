namespace AdventOfCode.Day4;

public class Day4 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day4\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day4\test_data.txt");

    public void Part1()
    {
        var rangeComparer = new RangeComparer();
        var elfPairs = rangeComparer.ParseSearchFieldInput(Data);
        var elfPairsTest = rangeComparer.ParseSearchFieldInput(DataTest);
        Console.WriteLine($"Overlapping ranges in test data {rangeComparer.NumberOfRangesThatFullyOverlap(elfPairsTest)}");
        Console.WriteLine($"Overlapping ranges in data {rangeComparer.NumberOfRangesThatFullyOverlap(elfPairs)}");
    }

    public void Part2()
    {
        var rangeComparer = new RangeComparer();
        var elfPairs = rangeComparer.ParseSearchFieldInput(Data);
        var elfPairsTest = rangeComparer.ParseSearchFieldInput(DataTest);
        Console.WriteLine($"Overlapping ranges in test data {rangeComparer.NumberOfRangesThatOverlap(elfPairsTest)}");
        Console.WriteLine($"Overlapping ranges in data {rangeComparer.NumberOfRangesThatOverlap(elfPairs)}");
    }
}
