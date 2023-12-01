using System;

public class Day9 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day9\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day9\test_data.txt");
    public string[] DataTest2 = System.IO.File.ReadAllLines(@"Day9\test_data2.txt");
    
    public void Part1()
    {
        var bridgeTest = new RopeBridge(6,0,5,true);
        bridgeTest.RunCommands(DataTest);
        Console.WriteLine($"Number of spaces visited by tail test {bridgeTest.NumberOfSpacesVisitedByTail()}");
        
        var bridgeTest2 = new RopeBridge2(32,16,16,true, 10);
        bridgeTest2.RunCommands(DataTest2);
        Console.WriteLine($"Number of spaces visited by tail test {bridgeTest2.NumberOfSpacesVisitedByTail()}");
        
        var bridge = new RopeBridge(1000, 500, 500, false);
        bridge.RunCommands(Data);
        Console.WriteLine($"Number of spaces visited by tail {bridge.NumberOfSpacesVisitedByTail()}");
        
    }

    public void Part2()
    {
        var bridgeTest = new RopeBridge2(6,0,5,true, 10);
        bridgeTest.RunCommands(DataTest);
        Console.WriteLine($"Number of spaces visited by tail test {bridgeTest.NumberOfSpacesVisitedByTail()}");
        
        var bridge = new RopeBridge2(1000, 500, 500, false, 10);
        bridge.RunCommands(Data);
        Console.WriteLine($"Number of spaces visited by tail {bridge.NumberOfSpacesVisitedByTail()}");
        
    }
}
