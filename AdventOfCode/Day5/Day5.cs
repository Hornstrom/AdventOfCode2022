namespace AdventOfCode.Day5;

public class Day5 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day5\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day5\test_data.txt");
    
    public void Part1()
    {
        var testCargoOrganizer = new CargoOrganizer(3, 3, DataTest);
        var cargoOrganizer = new CargoOrganizer(9, 8, Data);
        
        Console.WriteLine(testCargoOrganizer.RunCommands());
        Console.WriteLine(cargoOrganizer.RunCommands());
    }

    public void Part2()
    {
        var testCargoOrganizer = new CargoOrganizer(3, 3, DataTest);
        var cargoOrganizer = new CargoOrganizer(9, 8, Data);
        
        Console.WriteLine(testCargoOrganizer.RunCommandsPart2());
        Console.WriteLine(cargoOrganizer.RunCommandsPart2());
    }
}
