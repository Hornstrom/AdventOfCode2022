

using AdventOfCode.Day12;

public class Day12 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day12\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day12\test_data.txt");
    
    public void Part1()
    {
        // var hillTest = new Hill(DataTest);
        // hillTest.RunClimb();
        //
        // var hill = new Hill(Data);
        // hill.RunClimb();
    }

    public void Part2()
    {
        var hillTest = new Hill(DataTest);
        hillTest.FewestStepsFromLowestElevation();
        
        var hill = new Hill(Data);
        Console.WriteLine($"Shortest path form elevation a to end is: {hill.FewestStepsFromLowestElevation()}");
    }
}
