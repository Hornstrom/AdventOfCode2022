using System;
using System.Collections.Generic;

public class ComDevice
{
    private List<PacketPair> PacketPairs { get; set; }
    public ComDevice(string[] data)
    {
        PacketPairs = new List<PacketPair>();
        for (int i = 0; i < data.Length - 1; i += 3)
        {
            PacketPairs.Add(new PacketPair(data[i], data[i + 1]));
        }
        
        Console.WriteLine("Done parsing input");
    }

    public void Print()
    {
        foreach (var packetPair in PacketPairs)
        {
            packetPair.Print();
        }
    }
    
    public int SumOfCorrectPairs()
    {
        var sumCorrectPairs = 0;
        var currentIndex = 1;
        foreach (var packetPair in PacketPairs)
        {
            if (packetPair.HasCorrectOrder())
            {
                sumCorrectPairs += currentIndex;
            }

            currentIndex++;
        }

        return sumCorrectPairs;
    }

    public class PacketPair
    {
        public Packet LeftPacket { get; set; }
        public Packet RightPacket { get; set; }

        public PacketPair(string leftPacket, string rightPacket)
        {
            LeftPacket = new Packet(leftPacket[1..^1], true);
            RightPacket = new Packet(rightPacket[1..^1], true);
        }

        public void Print()
        {
            Console.WriteLine(LeftPacket.Print());
            Console.WriteLine(RightPacket.Print());
            Console.WriteLine();
            
        }

        public bool HasCorrectOrder()
        {
            return HasCorrectOrder(LeftPacket, RightPacket).Value;
        }

        public bool? HasCorrectOrder(Packet left, Packet right)
        {
            // win by size
            if (left.Actual.HasValue && right.Actual.HasValue)
            {
                if (left.Actual.Value < right.Actual.Value)
                {
                    return true;
                }
                
                if (left.Actual.Value > right.Actual.Value)
                {
                    return false;
                }

                return null;
            }
            
            
            // left side has run out of items
            if (!left.Actual.HasValue && !left.Packets.Any() && (right.Actual.HasValue || right.Packets.Any()))
            {
                return true;
            }
            
            // right side has run out of items
            if (!right.Actual.HasValue && !right.Packets.Any() && (left.Actual.HasValue || left.Packets.Any()))
            {
                return false;
            }
            
            // if left is int and right is list
            if (left.Actual.HasValue && !right.Actual.HasValue)
            {
                var packetToCompare = right.Packets.First();
                right.Packets.Remove(packetToCompare);
                return HasCorrectOrder(left, packetToCompare);
            }
            
            // if right is int and left is list
            if (right.Actual.HasValue && !left.Actual.HasValue)
            {
                var packetToCompare = left.Packets.First();
                left.Packets.Remove(packetToCompare);
                return HasCorrectOrder(packetToCompare, right);
            }
            
            while (true)
            {
                // Well let's grab the first package of each then until they run out
                var firstLeft = left.Packets.FirstOrDefault();
                var firstRight = right.Packets.FirstOrDefault();
                if (firstLeft == null)
                {
                    return true;
                }

                if (firstRight == null)
                {
                    return false;
                }
                left.Packets.Remove(firstLeft);
                right.Packets.Remove(firstRight);
                var result = HasCorrectOrder(firstLeft, firstRight);
                if (result.HasValue)
                {
                    return result;
                }
            }
        }

        
    }

    public class Packet
    {
        public List<Packet> Packets { get; set; }
        public int? Actual { get; set; }
        public Packet(string data, bool isInList = false)
        {
            Packets = new List<Packet>();

            if (string.IsNullOrEmpty(data))
            {
                return;
                throw new Exception("WHy no data?");
            }

            if(int.TryParse(data, out var d))
            {
                Actual = d;
                return;
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
                            endOfList = i;
                            break;
                        }
                        
                        if (data[i] == ']' && nrOfClosingToSkip > 0)
                        {
                            nrOfClosingToSkip--;
                        }
                    }
                    
                    if (!string.IsNullOrEmpty(data[1..endOfList]))
                    {
                        Packets.Add(new Packet(data[1..endOfList], true));
                    }
                    if (endOfList == data.Length - 1)
                    {
                        data = "";
                        continue;
                    }
                    else
                    {
                        data = data[(endOfList + 1)..];
                        continue;
                    }
                }

                if (char.IsNumber(data[0]))
                {
                    var indexOfNextComma = data.IndexOf(',');
                    if (indexOfNextComma > 0)
                    {
                        if (!string.IsNullOrEmpty(data[..indexOfNextComma]))
                        {
                            Packets.Add(new Packet(data[..indexOfNextComma]));
                        }
                        data = data[(indexOfNextComma + 1)..];    
                        continue;
                    }
                    else
                    {
                        if (!int.TryParse(data, out var foo))
                        {
                            throw new Exception("Expected it to only be numbers left in data string");
                        }
                        if (!string.IsNullOrEmpty(data))
                        {
                            Packets.Add(new Packet(data));
                        }
                        data = "";
                        continue;
                    }
                }   
                if (data[0] == ',')
                {
                    data = data[1..];
                }
            }    
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
    }

    
}
