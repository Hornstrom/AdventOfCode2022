

using System;

public class Day13 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day13\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day13\test_data.txt");
    
    public void Part1()
    {
        var comDeviceTest = new ComDevice(DataTest);
        Console.WriteLine();
    }

    public void Part2()
    {
        
    }
}
