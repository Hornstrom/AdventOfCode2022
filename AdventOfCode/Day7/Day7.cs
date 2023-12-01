namespace AdventOfCode.Day7;

public class Day7 : Day
{
    public string[] Data = System.IO.File.ReadAllLines(@"Day7\data.txt");
    public string[] DataTest = System.IO.File.ReadAllLines(@"Day7\test_data.txt");
    
    public void Part1()
    {
        var testFileSystem = new FileSystem(DataTest);
        var fileSystem = new FileSystem(Data);
        Console.WriteLine($"Sum of test directories smaller then 100000 is: {testFileSystem.GetSizeDirectoriesSmallerThen(100000)}");
        Console.WriteLine($"Sum of directories smaller then 100000 is: {fileSystem.GetSizeDirectoriesSmallerThen(100000)}");

    }

    public void Part2()
    {
        var testFileSystem = new FileSystem(DataTest);
        var fileSystem = new FileSystem(Data);
        var testDirToDelete = testFileSystem.GetDirectoryToDelete(70000000, 30000000);
        var dirToDelete = fileSystem.GetDirectoryToDelete(70000000, 30000000);
        Console.WriteLine($"Smallest directory we can delete is {testDirToDelete.Item1} with size {testDirToDelete.Item2}");
        Console.WriteLine($"Smallest directory we can delete is {dirToDelete.Item1} with size {dirToDelete.Item2}");
    }
}
