namespace AdventOfCode.Day13;

public class ComDevice
{
    public ComDevice(string[] data)
    {
        for (int i = 0; i < data.Length -1; i++)
        {
            
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
        public Packet(string packet)
        {
            
        }
    }

    
}