using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class RopeBridge2
{
    private bool _shouldPrint;
    private int _startPosX;
    private int _startPosY;
    
    private Location[,] _locations;
    private int _gridSize;
    private Tuple<int, int>[] _ropePosition;

    public RopeBridge2(int gridSize, int startPosX, int startPosY, bool shouldPrint, int ropeLength)
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

        _ropePosition = new Tuple<int, int>[ropeLength];
        for (int i = 0; i < ropeLength; i++)
        {
            _ropePosition[i] = new Tuple<int, int>(startPosX,startPosY);
        }
    }

    public void RunCommands(string[] data)
    {
        _locations[_startPosX, _startPosY].IsStartingLocation = true;
        _locations[_startPosX, _startPosY].ContainsHead = true;
        _locations[_startPosX, _startPosY].ContainsTail = true;
        _locations[_startPosX, _startPosY].HasBeenVisitedByTail = true;

        Print();
        
        foreach (var line in data)
        {
            if(_shouldPrint)
            {
                Console.WriteLine($"{line}");    
            }
            
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

                for (int k = 0; k < _ropePosition.Length; k++)
                {
                    if (_ropePosition[k].Item1 == j && _ropePosition[k].Item2 == i)
                    {
                        output = k.ToString();
                    }
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
            for (int j = 0; j < _ropePosition.Length; j++)
            {
                var x = _ropePosition[j].Item1;
                var y = _ropePosition[j].Item2;
                if (j == 0)
                {
                    _locations[x, y].ContainsHead = false;
                    _locations[x + xDirection, y + yDirection].ContainsHead = true;
                    _ropePosition[j] = new Tuple<int, int>(x + xDirection, y + yDirection);
                }
                else
                {
                    var xPrevious = _ropePosition[j - 1].Item1;
                    var yPrevious = _ropePosition[j - 1].Item2;
                    if (Math.Abs(xPrevious - x) + Math.Abs(yPrevious - y) > 2)
                    {
                        // Must move diagonally
                        if (xPrevious > x && yPrevious > y)
                        {
                            x++;
                            y++;
                        }
                        if (xPrevious < x && yPrevious > y)
                        {
                            x--;
                            y++;
                        }
                        if (xPrevious < x && yPrevious < y)
                        {
                            x--;
                            y--;
                        }
                        if (xPrevious > x && yPrevious < y)
                        {
                            x++;
                            y--;
                        }
                    }
                    else if(Math.Abs(x - xPrevious) > 1)
                    {
                        if (x - xPrevious > 1)
                        {
                            x--;
                        }
                        if (xPrevious - x > 1)
                        {
                            x++;
                        }
                    }
                    else if (Math.Abs(y - yPrevious) > 1)
                    {
                        if (y - yPrevious > 1)
                        {
                            y--;
                        }
                        if (yPrevious - y > 1)
                        {
                            y++;
                        }
                    }
                    _ropePosition[j] = new Tuple<int, int>(x, y);
                    if (j == _ropePosition.Length - 1)
                    {
                        _locations[x, y].ContainsTail = false;
                        _locations[x, y].ContainsTail = true;
                        _locations[x, y].HasBeenVisitedByTail = true;
                    }
                }
            }
            
            Print();
        }
    }
}
