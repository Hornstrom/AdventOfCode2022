using System;
using System.Text.RegularExpressions;

public class RopeBridge
{
    private bool _shouldPrint;
    private int _startPosX;
    private int _startPosY;
    
    private Location[,] _locations;
    private int _headPosX;
    private int _headPosY;
    private int _tailPosX;
    private int _tailPosY;
    private int _gridSize;

    public RopeBridge(int gridSize, int startPosX, int startPosY, bool shouldPrint)
    {
        _shouldPrint = shouldPrint;
        _gridSize = gridSize;
        _startPosX = startPosX;
        _startPosY = startPosY;
        
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
        _locations[_startPosX, _startPosY].IsStartingLocation = true;
        _locations[_startPosX, _startPosY].ContainsHead = true;
        _locations[_startPosX, _startPosY].ContainsTail = true;
        _locations[_startPosX, _startPosY].HasBeenVisitedByTail = true;

        _headPosX = _startPosX;
        _headPosY = _startPosY;
        _tailPosX = _startPosX;
        _tailPosY = _startPosY;

        Print();
        
        foreach (var line in data)
        {
            Console.WriteLine($"{line}");
            var direction = line[0];

            var regex = new Regex("\\d+");
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
        if (!_shouldPrint)
        {
            return;
        }
        Console.WriteLine();
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
            
            if (Math.Abs(_headPosX - _tailPosX) > 1 || Math.Abs(_headPosY - _tailPosY) > 1)
            {
                _tailPosX = _headPosX - xDirection;
                _tailPosY = _headPosY - yDirection;
            }
            
            _locations[_tailPosX, _tailPosY].ContainsTail = true;
            _locations[_tailPosX, _tailPosY].HasBeenVisitedByTail = true;
            Print();
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
