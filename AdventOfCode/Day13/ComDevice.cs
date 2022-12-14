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

    public class PacketPair
    {
        public Packet LeftPacket { get; set; }
        public Packet RightPacket { get; set; }

        public PacketPair(string leftPacket, string rightPacket)
        {
            LeftPacket = new Packet(leftPacket);
            RightPacket = new Packet(rightPacket);
        }
        
        public int SumOfCorrectPairs
    }

    public class Packet
    {
        public List<Packet> Packets { get; set; }
        public int? Actual { get; set; }
        public Packet(string data)
        {
            Packets = new List<Packet>();

            if(int.TryParse(data, out var d))
            {
                Actual = d;
            }
            else
            {
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
                            }
                            
                            if (data[i] == ']' && nrOfClosingToSkip > 0)
                            {
                                nrOfClosingToSkip--;
                            }
                        }
                        
                        Packets.Add(new Packet(data[1..endOfList]));
                        if (endOfList == data.Length - 1)
                        {
                            data = "";
                            continue;
                        }
                        else
                        {
                            data = data[(endOfList + 1)..];
                        }
                    }

                    if (char.IsNumber(data[0]))
                    {
                        var indexOfNextComma = data.IndexOf(',');
                        if (indexOfNextComma > 0)
                        {
                            Packets.Add(new Packet(data[0..indexOfNextComma]));
                            data = data[(indexOfNextComma + 1)..];    
                        }
                        else
                        {
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
        
        
    }

    
}
