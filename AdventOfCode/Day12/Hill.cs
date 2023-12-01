namespace AdventOfCode.Day12
{
    public class Hill
    {
        private Node[,] _map;
        private int _width;
        private int _height;
        private int _currentX;
        private int _currentY;
        private List<Node> _moves = new List<Node>();
        private bool _foundEnd = false;
        private int _loopCount;
        public Hill(string[] data)
        {
            _width = data[0].Length; 
            _height = data.Length; 
            _map = new Node[_width, _height];
            var endX = 0;
            var endY = 0;

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    var currentChar = data[i][j];
                    var isStart = currentChar == 'S';
                    var isEnd = currentChar == 'E';

                    var elevation = currentChar;
                    if (isStart)
                    {
                        elevation = 'a';
                        _currentX = j;
                        _currentY = i;
                    }

                    if (isEnd)
                    {
                        elevation = 'z';
                        endX = j;
                        endY = i;
                    }
                    
                    _map[j, i] = new Node
                    {
                        X = j,
                        Y = i,
                        DistanceToEnd = 0,
                        StepsToHere = isStart ? 0 : null,
                        Elevation = elevation,
                        IsStart = isStart,
                        IsEnd = isEnd
                    };
                }
            }

            foreach (var node in _map)
            {
                node.DistanceToEnd = Math.Sqrt(Math.Pow(Math.Abs(node.X - endX), 2) + Math.Pow(Math.Abs(node.Y - endY), 2));
            }
            
            _moves.Add(_map[_currentX, _currentY]);
            
        }

        public int RunClimb()
        {
            var stepsToEnd = int.MaxValue;
            while (_foundEnd == false)
            {
                if (_moves.Count == 0)
                {
                    Console.WriteLine("Failed to find end");
                    // Print();
                    return stepsToEnd;
                }
                else
                {
                    var nextMove = _moves.MinBy(m => m.DistanceToEnd + m.StepsToHere ?? 0);
                    _moves.Remove(nextMove);
                    stepsToEnd = Climb(nextMove);    
                }
                
            }

            return stepsToEnd;
        }

        public int FewestStepsFromLowestElevation()
        {
            var fewestSteps = int.MaxValue;

            foreach (var node in _map)
            {
                if (node.Elevation == 'a')
                {
                    ClearPath();
                    _moves.Add(node);
                    _currentX = node.X;
                    _currentY = node.Y;
                    node.StepsToHere = 0;
                    var steps = RunClimb();
                    if (steps < fewestSteps)
                    {
                        fewestSteps = steps;
                    }
                }
            }

            return fewestSteps;
        }

        private void ClearPath()
        {
            foreach (var node in _map)
            {
                node.StepsToHere = null;
                node.PreviousNodeX = null;
                node.PreviousNodeY = null;
            }
            _moves.Clear();
            _foundEnd = false;
        }
        public void Print()
        {
            for (int i = 0; i < _height; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < _width; j++)
                {
                    if (_currentX == j && _currentY == i)
                    {
                        Console.Write('#');    
                    }
                    else
                    {
                        if (_map[j, i].StepsToHere.HasValue)
                        {
                            Console.Write('X');
                        }
                        else
                        {
                            Console.Write(_map[j, i].Elevation);
                        }
                    }
                }
            }
        }

        public int Climb(Node currentNode)
        {
            _currentX = currentNode.X;
            _currentY = currentNode.Y;
            // currentNode.Y = move.Y;

            // if (_loopCount % 100 == 0)
            // {
            //     Print();
            //     Console.WriteLine();    
            // }
            // _loopCount++;
            
            // var currentNode = _map[currentNode.X, currentNode.Y];
            
            // Check for end condition
            if (currentNode.IsEnd)
            {
                _foundEnd = true;
                Console.WriteLine($"Steps to end: {currentNode.StepsToHere}");
                return currentNode.StepsToHere.Value;
            }
            
            // Evaluate all directions
            if (currentNode.Y - 1 >= 0 && Math.Abs(currentNode.Elevation - _map[currentNode.X, currentNode.Y - 1].Elevation) <= 1)
            {
                var northNode = _map[currentNode.X, currentNode.Y - 1];
                if (northNode.StepsToHere == null)
                {
                    northNode.PreviousNodeX = currentNode.X;
                    northNode.PreviousNodeY = currentNode.Y;
                    northNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(northNode);
                }
            }
            
            if (currentNode.Y + 1 <= _height - 1 && _map[currentNode.X, currentNode.Y + 1].Elevation - currentNode.Elevation <= 1)
            {
                var southNode = _map[currentNode.X, currentNode.Y + 1];
                if (southNode.StepsToHere == null)
                {
                    southNode.PreviousNodeX = currentNode.X;
                    southNode.PreviousNodeY = currentNode.Y;
                    southNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(southNode);
                }
                
            }
            
            if (currentNode.X + 1 <= _width - 1 && _map[currentNode.X + 1, currentNode.Y].Elevation - currentNode.Elevation <= 1)
            {
                var eastNode = _map[currentNode.X + 1, currentNode.Y];
                if (eastNode.StepsToHere == null)
                {
                    eastNode.PreviousNodeX = currentNode.X;
                    eastNode.PreviousNodeY = currentNode.Y;
                    eastNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(eastNode);
                }
                
            }
            
            if (currentNode.X - 1 >= 0 && _map[currentNode.X - 1, currentNode.Y].Elevation - currentNode.Elevation <= 1)
            {
                var westNode = _map[currentNode.X - 1, currentNode.Y];
                if (westNode.StepsToHere == null)
                {
                    westNode.PreviousNodeX = currentNode.X;
                    westNode.PreviousNodeY = currentNode.Y;
                    westNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(westNode); 
                }
                
            }

            return int.MaxValue;
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double DistanceToEnd { get; set; }
        public int? StepsToHere { get; set; }
        public int? PreviousNodeX { get; set; }
        public int? PreviousNodeY { get; set; }
        public char Elevation { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }

    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Distance { get; set; }
    }
}
