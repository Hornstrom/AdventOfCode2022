namespace AdventOfCode.Day9;

public class Day9 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day9\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day9\test_data.txt");
    
    public void Part1()
    {
        // var bridgeTest = new RopeBridge();
        // bridgeTest.RunCommands(DataTest);
        // // bridgeTest.Print();
        // Console.WriteLine($"Number of spaces visited by tail test {bridgeTest.NumberOfSpacesVisitedByTail()}");
        
        var bridge = new RopeBridge();
        bridge.RunCommands(Data);
        Console.WriteLine($"Number of spaces visited by tail {bridge.NumberOfSpacesVisitedByTail()}");
        
    }

    public void Part2()
    {
        Console.WriteLine($"");
        Console.WriteLine($"");
        
    }
}
