namespace AdventOfCode.Day6;

public class CommunicationDevice
{
    public int FindStartOfPacket(string data, int startMarkerLength)
    {
        for (int i = 0; i < data.Length - startMarkerLength; i++)
        {
            var potentialStart = true;
            var potentialMarker = data.Substring(i, startMarkerLength);
            for (int j = 0; j < startMarkerLength; j++)
            {
                var stringBefore = potentialMarker.Substring(0, j);
                var stringAfter = "";
                if (j + 1 < startMarkerLength)
                {
                    stringAfter = potentialMarker.Substring(j + 1);    
                }
                

                if (stringAfter.Contains(potentialMarker[j]) || stringBefore.Contains(potentialMarker[j]))
                {
                    potentialStart = false;
                }
            }

            if (potentialStart)
            {
                return i+startMarkerLength;
            }
        }

        throw new Exception("No start packet found!");
    }
}
