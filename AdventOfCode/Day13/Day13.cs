

using System;

public class Day13 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day13\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day13\test_data.txt");
    
    public void Part1()
    {
        // var comDeviceTest = new ComDevice(DataTest);
        // comDeviceTest.Print();
        // Console.WriteLine($"Sum of indicies test: {comDeviceTest.SumOfCorrectPairs()}");
        //
        var comDevice = new ComDevice(Data);
        comDevice.Print();
        Console.WriteLine($"Sum of indicies: {comDevice.SumOfCorrectPairs()}");
    }

    public void Part2()
    {
        
        
    }
}
