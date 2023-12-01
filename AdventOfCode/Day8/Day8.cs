namespace AdventOfCode.Day8;

public class Day8 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day8\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day8\test_data.txt");
    
    public void Part1()
    {
        var testGrove = new TreeGrove(DataTest);
        var grove = new TreeGrove(Data);
        Console.WriteLine($"Visible trees test {testGrove.GetNumberOfVisibleTrees()}");
        Console.WriteLine($"Visible trees {grove.GetNumberOfVisibleTrees()}");
    }

    public void Part2()
    {
        var testGrove = new TreeGrove(DataTest);
        var grove = new TreeGrove(Data);
        Console.WriteLine($"Score of best tree test {testGrove.ScoreOfBestTree()}");
        Console.WriteLine($"Score of best tree {grove.ScoreOfBestTree()}");
        
    }
}
