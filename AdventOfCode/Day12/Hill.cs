namespace AdventOfCode.Day12
{
    public class Hill
    {
        private Node[,] _map;  
        
        public Hill(string[] data)
        {
            var width = data[0].Length; 
            var height = data.Length; 
            _map = new Node[width, height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _map[j, i] = new Node
                    {
                        X = j,
                        Y = i,
                        DistanceToEnd = 0,
                        StepsToHere = null,
                        PreviousNode = null,
                        Elevation = data[i][j]
                    };
                }
            }
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double DistanceToEnd { get; set; }
        public int? StepsToHere { get; set; }
        public Node? PreviousNode { get; set; }
        public int Elevation { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }
}
