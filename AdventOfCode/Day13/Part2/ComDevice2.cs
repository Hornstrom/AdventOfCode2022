using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Day13.Part2;

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
        Packets.Add(new Packet("[[2]]"));
        Packets.Add(new Packet("[[6]]"));
        
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
}
