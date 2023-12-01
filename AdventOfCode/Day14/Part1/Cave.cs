using System.Text.RegularExpressions;

namespace AdventOfCode.Day14;

public class Cave
{
    private Node[,] _map;
    private int _minX;
    public Cave(string[] data)
    {
        var megaString = "";
        foreach (var line in data)
        {
            megaString += line + " ";
        }

        var xRegEx = new Regex("\\d+,");
        var yRegEx = new Regex(",\\d+");
        var xMatches = xRegEx.Matches(megaString);
        var yMatches = yRegEx.Matches(megaString);
        var xValues = xMatches.Select(m => int.Parse(m.Value.Replace(",", null)));
        var yValues = yMatches.Select(m => int.Parse(m.Value.Replace(",", null)));
        var minX = xValues.Min();
        _minX = minX;
        var minY = yValues.Min();
        var maxX = xValues.Max();
        var maxY = yValues.Max();
        
        Console.WriteLine($"Min value x: {minX}");
        Console.WriteLine($"Min value y: {minY}");
        Console.WriteLine($"Max value x: {maxX}");
        Console.WriteLine($"Max value y: {maxY}");

        var xSize = maxX - minX;
        _map = new Node[xSize + 1, maxY + 1];

        for (int i = 0; i < xSize + 1; i++)
        {
            for (int j = 0; j < maxY + 1; j++)
            {
                _map[i, j] = new Node();
            }
        }

        foreach (var line in data)
        {
            var pairRegex = new Regex("\\d+,\\d+");
            var matches = pairRegex.Matches(line).Select(m => m.Value);

            var previousX = 0;
            var previousY = 0;
            
            for (int i = 0; i < matches.Count(); i++)
            {
                var xyArray = matches.ElementAt(i).Split(',');
                if (i == 0)
                {
                    previousX = int.Parse(xyArray[0]) - minX;
                    previousY = int.Parse(xyArray[1]);

                    _map[previousX, previousY].IsRock = true;
                }
                else
                {
                    var currentX = int.Parse(xyArray[0]) -minX;
                    var currentY = int.Parse(xyArray[1]);

                    if (previousX < currentX)
                    {
                        for (int x = previousX; x <= currentX; x++)
                        {
                            _map[x, currentY].IsRock = true;    
                        }    
                    }
                    
                    if (previousX > currentX)
                    {
                        for (int x = currentX; x <= previousX; x++)
                        {
                            _map[x, currentY].IsRock = true; 
                        }    
                    }
                    
                    if (previousY < currentY)
                    {
                        for (int y = previousY; y <= currentY; y++)
                        {
                            _map[currentX, y].IsRock = true;
                        }    
                    }
                    
                    if (previousY > currentY)
                    {
                        for (int y = currentY; y <= previousY; y++)
                        {
                            _map[currentX, y].IsRock = true;
                        }    
                    }

                    previousX = currentX;
                    previousY = currentY;
                }
            }
        }
    }

    public void Print()
    {
        for (int i = 0; i < _map.GetLength(1); i++)
        {
            for (int j = 0; j < _map.GetLength(0); j++)
            {
                var currentChar = '.';
                if (_map[j, i].IsRock)
                {
                    currentChar = '#';
                } 
                if (_map[j, i].IsSand)
                {
                    currentChar = 'o';
                } 
                Console.Write(currentChar);
            }
            
            Console.WriteLine();
            
        }
    }

    public bool SpawnUnitOfSand()
    {
        var comesToRest = false;

        var x = 500 - _minX;
        var y = 0;

        while (true)
        {
            // check for falling of south edge
            if (y + 1 >= _map.GetLength(1))
            {
                return false;
            }
            
            // Check position bellow
            if (_map[x, y + 1].IsRock || _map[x, y + 1].IsSand)
            {
                // Blocked!
                // try left
                if (x - 1 < 0)
                {
                    // Falls of the left edge
                    return false;
                }
                if (_map[x - 1, y + 1].IsRock || _map[x - 1, y + 1].IsSand)
                {
                    //left blocked try right
                    if (x + 1 >= _map.GetLength(0))
                    {
                        return false;
                    }
                    
                    if (_map[x + 1, y + 1].IsRock || _map[x + 1, y + 1].IsSand)
                    {
                        // all three directions blocked. Comes to rest
                        _map[x, y].IsSand = true;
                        return true;
                    }
                    else
                    {
                        // right is not blocked
                        y += 1;
                        x += 1;
                    }
                }
                else
                {
                    // left is not blocked
                    y += 1;
                    x -= 1;
                }
            }
            else
            {
                // down is not blocked
                y += 1;
            }
        }
    }

    public int UnitsOfSandThatCanSpawn()
    {
        var go = true;
        var unitsOfSand = 0;

        while (go)
        {
            go = SpawnUnitOfSand();
            unitsOfSand++;
        }

        return unitsOfSand - 1;
    }

    private class Node
    {
        public bool IsSand {get; set;}
        public bool IsRock {get; set;}
    }
    
}