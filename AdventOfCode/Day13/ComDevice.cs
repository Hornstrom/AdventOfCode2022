using System;
using System.Collections.Generic;

public class ComDevice
{
    private List<PacketPair> PacketPairs { get; set; }
    public ComDevice(string[] data)
    {
        for (int i = 0; i < data.Length - 1; i += 3)
        {
            PacketPairs.Add(new PacketPair(data[i], data[i + 1]));
        }
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
    }

    public class Packet
    {
        public List<Packet> Packets { get; set; }
        public int? Actual { get; set; }
        public Packet(string data)
        {
            Packets = new List<Packet>();
            
            if (string.IsNullOrEmpty(data))
            {
                throw new Exception("No data what gives.");
            }
            
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
                        var endOfList = data.LastIndexOf(']');
                        Packets.Add(new Packet(data[1..(endOfList - 1)]));
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
                        Packets.Add(new Packet(data[1..(indexOfNextComma - 1)]));
                        data = data[(indexOfNextComma + 1)..];
                        
                    }
                }    
            }
        }

        public string Print()
        {
            var result = "";

            if (Actual.HasValue)
            {
                result = Actual.ToString();
            }
            else
            {
                foreach (var packet in Packets)
                {
                    result += $"|{packet.Print()}|";
                }
            }
                
            return result;
        }
    }

    
}
