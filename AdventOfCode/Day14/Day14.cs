namespace AdventOfCode.Day14;

public class Day14 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day14\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day14\test_data.txt");
    
    public void Part1()
    {
        var caveTest = new Cave(DataTest);
        var cave = new Cave(Data);
        
        caveTest.Print();
        Console.WriteLine($"Units of sand that can spawn: {caveTest.UnitsOfSandThatCanSpawn()}");
        Console.WriteLine("--- Real Data ---");
        cave.Print();
        Console.WriteLine($"Units of sand that can spawn: {cave.UnitsOfSandThatCanSpawn()}");
    }

    public void Part2()
    {
        var caveTest = new Part2.Cave(DataTest);
        var cave = new Part2.Cave(Data);
        
        Console.WriteLine($"Units of sand that can spawn: {caveTest.UnitsOfSandThatCanSpawn()}");
        caveTest.Print();
        Console.WriteLine("--- Real Data ---");
        Console.WriteLine($"Units of sand that can spawn: {cave.UnitsOfSandThatCanSpawn()}");
    }
}