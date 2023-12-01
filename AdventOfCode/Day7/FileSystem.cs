using System.Text.RegularExpressions;

namespace AdventOfCode.Day7;

public class FileSystem
{
    private Directory _iAmRoot;
    public FileSystem(string[] data)
    {
        _iAmRoot = new Directory("root", null);
        var currentDir = _iAmRoot;

        for (int i = 0; i < data.Length; i++)
        {
            var line = data[i];
            if (line.Contains("$ cd /"))
            {
                currentDir = _iAmRoot;
                continue;
            }
            if (line.Contains("$ cd .."))
            {
                currentDir = currentDir.ParentDir;
                continue;
            }

            if (line.Contains("$ ls"))
            {
                continue;
            }


            var regex = new Regex("\\$ cd [a-zA-Z]+");
            var matches = regex.Matches(line);
            if (matches.Count > 0)
            {
                var directoryName = matches[0].Value[4..];
                currentDir = currentDir.Directories.First(d => d.Name == directoryName);
                continue;
            }

            if (line.Contains("dir"))
            {
                var newFolder = new Directory(line[3..], currentDir);
                currentDir.Directories.Add(newFolder);
                continue;
            }
            
            var regexFile = new Regex("[0-9]+ [a-zA-Z]+");
            var fileMatches = regexFile.Matches(line);
            if (fileMatches.Count > 0)
            {
                var fileLine = line.Split(' ');
                var newFile = new File(fileLine[1], int.Parse(fileLine[0]));
                currentDir.Files.Add(newFile);
                continue;
            }

            throw new Exception("unexpected command");
        }
    }
    
    public List<Tuple<string,int>> GetDirectoryNameAndSizes()
    {
        var directoryList = new List<Tuple<string, int>>();
        _iAmRoot.NameAndSizePlease(directoryList);
        return directoryList;
    }
    
    public int GetSizeDirectoriesSmallerThen(int maxSize)
    {
        var directories = GetDirectoryNameAndSizes().Where(t => t.Item2 <= maxSize);
        return directories.Sum(d => d.Item2);
    }

    public Tuple<string, int> GetDirectoryToDelete(int totalSystemSize, int requiredFreeSize)
    {
        var allDir = GetDirectoryNameAndSizes().OrderBy(d => d.Item2);
        var totalSize = _iAmRoot.GetSize();
        var currentFreeSpace = totalSystemSize - totalSize;
        var sizeNeeded = requiredFreeSize - currentFreeSpace;
        return allDir.First(d => d.Item2 >= sizeNeeded);
    }


}

public class Directory
{
    public Directory ParentDir { get; set; }
    public List<Directory> Directories { get; set; }
    public List<File> Files { get; set; }
    public string Name { get; set; }

    public Directory(string name, Directory parentDir)
    {
        Name = name;
        ParentDir = parentDir;
        Directories = new List<Directory>();
        Files = new List<File>();
    }

    public int GetSize()
    {
        var filesInDir = 0;
        if (Files.Any())
        {
            filesInDir = Files.Sum(f => f.Size);    
        }
        
        var dirInDir = 0;
        if (Directories.Any())
        {
            dirInDir += Directories.Sum(directory => directory.GetSize());
        }

        return filesInDir + dirInDir;
    }

    public List<Tuple<string, int>> NameAndSizePlease(List<Tuple<string, int>> directoryList)
    {
        directoryList.Add(new Tuple<string, int>(Name, GetSize()));
        Directories.ForEach(d => d.NameAndSizePlease(directoryList));
        return directoryList;
    }
}

public class File
{
    public string Name { get; set; }
    public int Size { get; set; }

    public File(string name, int size)
    {
        Name = name;
        Size = size;
    }
}
