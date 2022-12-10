using System;
using AdventOfCode.Day10;

public class Day10 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day10\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day10\test_data.txt");
    
    public void Part1()
    {
        var videoDeviceTest = new VideoDevice();
        videoDeviceTest.RunCommands(DataTest);
        Console.WriteLine($"Result test: {videoDeviceTest.SumOfSignalStrength()}");
        
        var videoDevice = new VideoDevice();
        videoDevice.RunCommands(Data);
        Console.WriteLine($"Result test: {videoDevice.SumOfSignalStrength()}");

    }

    public void Part2()
    {
        var videoDeviceTest = new VideoDevice();
        videoDeviceTest.RunCommands(DataTest);
        videoDeviceTest.PrintCrtDisplay();
        
        Console.WriteLine();
        
        var videoDevice = new VideoDevice();
        videoDevice.RunCommands(Data);
        videoDevice.PrintCrtDisplay();
    }
}
