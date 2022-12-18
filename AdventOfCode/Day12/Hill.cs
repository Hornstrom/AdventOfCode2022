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
                node.DistanceToEnd = Math.Abs(node.X - endX) + Math.Abs(node.Y - endY);
            }
            
            _moves.Add(_map[_currentX, _currentY]);
            
        }

        public void RunClimb()
        {
            while (_foundEnd == false)
            {
                if (_moves.Count == 0)
                {
                    Console.WriteLine("Failed to find end");
                    return;
                }
                else
                {
                    var nextMove = _moves.MinBy(m => m.DistanceToEnd);
                    _moves.Remove(nextMove);
                    Climb(nextMove);    
                }
                
            }
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

        public void Climb(Node move)
        {
            _currentX = move.X;
            _currentY = move.Y;

            if (_loopCount % 100 == 0)
            {
                Print();
                Console.WriteLine();    
            }
            _loopCount++;
            
            var currentNode = _map[_currentX, _currentY];
            
            // Check for end condition
            if (currentNode.IsEnd)
            {
                _foundEnd = true;
                Console.WriteLine($"Steps to end: {currentNode.StepsToHere}");
                return;
            }
            
            // Evaluate all directions
            if (_currentX - 1 < 0 || Math.Abs(currentNode.Elevation - _map[_currentX - 1, _currentY].Elevation) > 1)
            {
                // north = Int32.MaxValue;
            }
            else
            {
                var northNode = _map[_currentX - 1, _currentY];
                if (northNode.StepsToHere == null)
                {
                    northNode.PreviousNodeX = currentNode.X;
                    northNode.PreviousNodeY = currentNode.Y;
                    northNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(northNode);
                }
            }
            
            if (_currentX + 1 > _width - 1 || Math.Abs(currentNode.Elevation - _map[_currentX + 1, _currentY].Elevation) > 1)
            {
                // south = Int32.MaxValue;
            }
            else
            {
                var southNode = _map[_currentX + 1, _currentY];
                if (southNode.StepsToHere == null)
                {
                    southNode.PreviousNodeX = currentNode.X;
                    southNode.PreviousNodeY = currentNode.Y;
                    southNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(southNode);
                }
                
            }
            
            if (_currentY + 1 > _height - 1 || Math.Abs(currentNode.Elevation - _map[_currentX, _currentY + 1].Elevation) > 1)
            {
                // east = Int32.MaxValue;
            }
            else
            {
                var eastNode = _map[_currentX, _currentY + 1];

                if (eastNode.StepsToHere == null)
                {
                    eastNode.PreviousNodeX = currentNode.X;
                    eastNode.PreviousNodeY = currentNode.Y;
                    eastNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(eastNode);
                }
                
            }
            
            if (_currentY - 1 < 0 || Math.Abs(currentNode.Elevation - _map[_currentX, _currentY - 1].Elevation) > 1)
            {
                //west = Int32.MaxValue;
            }
            else
            {
                var westNode = _map[_currentX, _currentY - 1];
                if (westNode.StepsToHere == null)
                {
                    westNode.PreviousNodeX = currentNode.X;
                    westNode.PreviousNodeY = currentNode.Y;
                    westNode.StepsToHere = currentNode.StepsToHere + 1;
                    _moves.Add(westNode); 
                }
                
            }
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int DistanceToEnd { get; set; }
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
