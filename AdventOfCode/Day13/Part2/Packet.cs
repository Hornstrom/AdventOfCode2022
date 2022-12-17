namespace AdventOfCode.Day13.Part2;

public class Packet : IComparable<Packet>
{
    public List<Packet> Packets { get; set; }
    public int? Actual { get; set; }
    public Packet(string data)
    {
        Packets = new List<Packet>();

        if (string.IsNullOrEmpty(data))
        {
            return;
        }

        if (int.TryParse(data, out var d))
        {
            Actual = d;
            return;
        }
        else
        {
            // unpack list
            data = data[1..^1];
        }

        var packetStrings = new List<string>();

        while (!string.IsNullOrEmpty(data))
        {
            if (data[0] == ']')
            {
                throw new Exception("Unexpected char");
            }

            if (data[0] == '[')
            {
                var nrOfClosingToSkip = 0;
                var endOfList = 0;
                for (int i = 1; i < data.Length; i++)
                {
                    if (data[i] == '[')
                    {
                        nrOfClosingToSkip++;
                    }

                    if (data[i] == ']' && nrOfClosingToSkip == 0)
                    {
                        endOfList = i + 1;
                        break;
                    }

                    if (data[i] == ']' && nrOfClosingToSkip > 0)
                    {
                        nrOfClosingToSkip--;
                    }
                }

                packetStrings.Add(data[0..endOfList]);

                if (endOfList == data.Length - 1)
                {
                    data = "";
                    continue;
                }
                else
                {
                    data = data[endOfList..];
                    continue;
                }
            }

            if (char.IsNumber(data[0]))
            {
                var indexOfNextComma = data.IndexOf(',');
                if (indexOfNextComma > 0)
                {
                    packetStrings.Add(data[..indexOfNextComma]);
                    data = data[(indexOfNextComma + 1)..];
                    continue;
                }
                else
                {
                    if (!int.TryParse(data, out var foo))
                    {
                        throw new Exception("Expected it to only be numbers left in data string");
                    }

                    packetStrings.Add(data);

                    data = "";
                    continue;
                }
            }

            if (data[0] == ',')
            {
                data = data[1..];
            }
        }

        foreach (var packetString in packetStrings)
        {
            Packets.Add(new Packet(packetString));
        }
    }
    public string Print()
    {
        if (Actual.HasValue)
        {
            return Actual.ToString();
        }
        else
        {
            var listOfPackets = "";
            foreach (var packet in Packets)
            {
                if (!string.IsNullOrEmpty(listOfPackets))
                {
                    listOfPackets += ",";
                }
                listOfPackets += packet.Print();

            }

            return $"[{listOfPackets}]";
        }
    }
    
    public static bool? HasCorrectOrder(Packet left, Packet right)
    {
        // Console.WriteLine($"Compare {left.Print()} vs {right.Print()}");
        // win by size
        if (left.Actual.HasValue && right.Actual.HasValue)
        {
            if (left.Actual.Value < right.Actual.Value)
            {
                // Console.WriteLine("Left side is smaller, so inputs are in the right order");
                return true;
            }
            
            if (left.Actual.Value > right.Actual.Value)
            {
                // Console.WriteLine("Right side is smaller, so inputs are not in the right order");
                return false;
            }

            return null;
        }
        
        
        // left side has run out of items
        if (!left.Actual.HasValue && !left.Packets.Any() && (right.Actual.HasValue || right.Packets.Any()))
        {
            // Console.WriteLine("Left side ran out of items, so inputs are in the right order");
            return true;
        }
        
        // right side has run out of items
        if (!right.Actual.HasValue && !right.Packets.Any() && (left.Actual.HasValue || left.Packets.Any()))
        {
            // Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
            return false;
        }
        
        // if left is int and right is list
        if (left.Actual.HasValue && !right.Actual.HasValue)
        {
            return HasCorrectOrder(new Packet($"[{left.Actual.Value.ToString()}]"), right);
        }
        
        // if right is int and left is list
        if (right.Actual.HasValue && !left.Actual.HasValue)
        {
            return HasCorrectOrder(left, new Packet($"[{right.Actual.Value.ToString()}]"));
        }
        
        var i = 0;
        while (true)
        {
            var currentLeft = left.Packets.ElementAtOrDefault(i);
            var currentRight = right.Packets.ElementAtOrDefault(i);
            if (currentLeft == null && currentRight != null)
            {
                // Console.WriteLine("Left side ran out of items, so inputs are in the right order (loop)");
                return true;
            }

            if (currentRight == null && currentLeft != null) 
            {
                // Console.WriteLine("Right side ran out of items, so inputs are not in the right order (loop)");
                return false;
            }

            if (currentLeft == null && currentRight == null)
            {
                // inconclusive
                return null;
            }
            
            var result = HasCorrectOrder(currentLeft, currentRight);
            if (result.HasValue)
            {
                return result;
            }

            i++;
            if (i > left.Packets.Count + right.Packets.Count)
            {
                break;
            }
        }

        throw new Exception("Failed to find correct order");
    }
        

    public int CompareTo(Packet? other)
    {
        if (other == null)
        {
            return 1;
        }
        
        var inCorrectOrder = HasCorrectOrder(this, other);
        if (inCorrectOrder.HasValue && inCorrectOrder.Value)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
