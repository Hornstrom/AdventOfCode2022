namespace AdventOfCode.Day12
{
    public class Hill
    {
        
        
        public Hill(string[] data)
        {
            
        }
    }

    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double DistanceToEnd { get; set; }
        public int StepsToHere { get; set; }
        public Node? PreviousNode { get; set; }
    }
}
