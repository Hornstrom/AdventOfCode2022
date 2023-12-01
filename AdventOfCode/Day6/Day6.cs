namespace AdventOfCode.Day6;

public class Day6 : Day
{
    public string Data = System.IO.File.ReadAllText(@"Day6\data.txt");
    public string DataTest = System.IO.File.ReadAllText(@"Day6\test_data.txt");
    
    public void Part1()
    {
        var device = new CommunicationDevice();
        Console.WriteLine($"Found start at: {device.FindStartOfPacket(DataTest, 4)}");
        Console.WriteLine($"Found start at: {device.FindStartOfPacket(Data, 4)}");
    }

    public void Part2()
    {
        var device = new CommunicationDevice();
        Console.WriteLine($"Found message at: {device.FindStartOfPacket(DataTest, 14)}");
        Console.WriteLine($"Found message at: {device.FindStartOfPacket(Data, 14)}");
    }
}
