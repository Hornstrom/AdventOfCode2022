using System;
using AdventOfCode.Day11.Part1;

public class Day11 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day11\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day11\test_data.txt");
    
    public void Part1()
    {
        var monkeyBusinessTest = new MonkeyBusiness(DataTest);
        var monkeyBusinessTest20 = monkeyBusinessTest.Play(20);
        Console.WriteLine($"Monkey business test: {monkeyBusinessTest20}");
        
        var monkeyBusiness = new MonkeyBusiness(Data);
        var monkeyBusiness20 = monkeyBusiness.Play(20);
        Console.WriteLine($"Monkey business: {monkeyBusiness20}");
    }

    public void Part2()
    {
        var monkeyBusinessTest = new AdventOfCode.Day11.Part2.MonkeyBusiness(DataTest);
        var monkeyBusinessTest20 = monkeyBusinessTest.Play(10000);
        Console.WriteLine($"Monkey business test: {monkeyBusinessTest20}");
        
        var monkeyBusiness = new AdventOfCode.Day11.Part2.MonkeyBusiness(Data);
        var monkeyBusiness20 = monkeyBusiness.Play(10000);
        Console.WriteLine($"Monkey business: {monkeyBusiness20}");
    }
}
