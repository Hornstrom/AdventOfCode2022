using System;
using System.Collections;
using System.Collections.Generic;

public class ComDevice2
{
    private List<Packet> Packets { get; set; }
    public ComDevice2(string[] data)
    {
        Packets = new List<Packet>();
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] == "")
            {
                continue;
            }
            Packets.Add(new Packet(data[i]));
        }
        
        Console.WriteLine("Done parsing input");
    }

    public int GetDecoderKey()
    {
        Packets.Sort();
        var i = 0;
        var stopPack2 = 0;
        var stopPack6 = 0;
        foreach (var packet in Packets)
        {
            i++;
            Console.WriteLine($"{i}: {packet.Print()}");
            if (packet.Print() == "[[2]]")
            {
                stopPack2 = i;
            }
            if (packet.Print() == "[[6]]")
            {
                stopPack6 = i;
            }
        }

        return stopPack2 * stopPack6;
    }

    
    public class Packet : IComparable<Packet>
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
        
        

        public int CompareTo(Packet obj)
        {
            var correctOrder = ComDevice.HasCorrectOrder(this, obj);
            if ()
            {
                
            }
        }
    }

    
}
