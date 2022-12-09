using System.Text.RegularExpressions;

namespace AdventOfCode.Day9;

public class RopeBridge
{
    private Location[,] _locations;
    private int _headPosX;
    private int _headPosY;
    private int _tailPosX;
    private int _tailPosY;
    private int _gridSize = 2000;

    public RopeBridge()
    {
        _locations = new Location[_gridSize, _gridSize];

        for (int i = 0; i < _gridSize; i++)
        {
            for (int j = 0; j < _gridSize; j++)
            {
                _locations[i, j] = new Location();
            }
        }
    }

    public void RunCommands(string[] data)
    {
        var x = 1000;
        var y = 1000;

        _locations[x, y].IsStartingLocation = true;
        _locations[x, y].ContainsHead = true;
        _locations[x, y].ContainsTail = true;
        _locations[x, y].HasBeenVisitedByTail = true;

        _headPosX = x;
        _headPosY = y;
        _tailPosX = x;
        _tailPosY = y;

        // Print();
        
        foreach (var line in data)
        {
            Console.WriteLine($"== {line} ==");
            var direction = line[0];

            var regex = new Regex("\\d");
            var moves = int.Parse(regex.Match(line).Value);

            switch (direction)
            {
                case 'U':
                    Move(0, -1, moves);
                    break;
                case 'D':
                    Move(0, 1, moves);
                    break;
                case 'L':
                    Move(-1, 0, moves);
                    break;
                case 'R':
                    Move(1, 0, moves);
                    break;
                default:
                    throw new Exception("Weird direction");
                
            }
        }
    }

    public int NumberOfSpacesVisitedByTail()
    {
        var result = 0;
        foreach (var location in _locations)
        {
            if (location.HasBeenVisitedByTail)
            {
                result++;
            }
        }

        return result;
    }

    public void Print()
    {
        for (int i = 0; i < _gridSize; i++)
        {
            for (int j = 0; j < _gridSize; j++)
            {
                var output = ".";
                if (_locations[j, i].HasBeenVisitedByTail)
                {
                    output = "#";
                }
                if (_locations[j, i].IsStartingLocation)
                {
                    output = "s";
                }
                if (_locations[j, i].ContainsTail)
                {
                    output = "T";
                }
                if (_locations[j, i].ContainsHead)
                {
                    output = "H";
                }
                Console.Write($"{output}");
            }
            Console.WriteLine();
        }
    }

    public void Move(int xDirection, int yDirection, int numberOfMoves)
    {
        for (int i = 0; i < numberOfMoves; i++)
        {
            _locations[_headPosX, _headPosY].ContainsHead = false;
            _headPosX += xDirection;
            _headPosY += yDirection;
            _locations[_headPosX, _headPosY].ContainsHead = true;

            _locations[_tailPosX, _tailPosY].ContainsTail = false;
            // trigger diagonal move to heads previous space
            if (Math.Abs(_headPosX - _tailPosX) + Math.Abs(_headPosY - _tailPosY) > 2)
            {
                _tailPosX = _headPosX - xDirection;
                _tailPosY = _headPosY - yDirection;
            }
            else // tail simply follows
            {
                if (Math.Abs(_headPosX - _tailPosX) > 1)
                {
                    _tailPosX = _headPosX - xDirection;
                    _tailPosY = _headPosY - yDirection;
                }
            
                if (Math.Abs(_headPosY - _tailPosY) > 1)
                {
                    _tailPosX = _headPosX - xDirection;
                    _tailPosY = _headPosY - yDirection;
                }    
            }
            _locations[_tailPosX, _tailPosY].ContainsTail = true;
            _locations[_tailPosX, _tailPosY].HasBeenVisitedByTail = true;
            // Print();
            // Console.WriteLine();
        }
    }
}

public class Location
{
    public bool IsStartingLocation { get; set; }
    public bool HasBeenVisitedByTail { get; set; }
    public bool ContainsHead { get; set; }
    public bool ContainsTail { get; set; }
}
