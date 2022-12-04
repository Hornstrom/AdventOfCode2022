namespace AdventOfCode.Day4;

public class RangeComparer
{
    public List<ElfPair> ParseSearchFieldInput(string[] elfPairings)
    {
        var elfPairs = new List<ElfPair>();

        foreach (var elfPairing in elfPairings)
        {
            var elfPairStringArray = elfPairing.Split(',');
            var elfOneArray = elfPairStringArray[0].Split('-');
            var elfTwoArray = elfPairStringArray[1].Split('-');
            var elfPair = new ElfPair
            {
                ElfOneStart = int.Parse(elfOneArray[0]),
                ElfOneEnd = int.Parse(elfOneArray[1]),
                ElfTwoStart = int.Parse(elfTwoArray[0]),
                ElfTwoEnd = int.Parse(elfTwoArray[1])
            };
            elfPairs.Add(elfPair);
        }

        return elfPairs;
    }

    public int NumberOfRangesThatFullyOverlap(List<ElfPair> elfPairs)
    {
        var overlappingRanges = 0;

        foreach (var elfPair in elfPairs)
        {
            if ((elfPair.ElfOneStart <= elfPair.ElfTwoStart && elfPair.ElfOneEnd >= elfPair.ElfTwoEnd)
                ||
                (elfPair.ElfOneStart >= elfPair.ElfTwoStart && elfPair.ElfOneEnd <= elfPair.ElfTwoEnd))
            {
                overlappingRanges++;
            }
        }

        return overlappingRanges;
    }
    
    public int NumberOfRangesThatOverlap(List<ElfPair> elfPairs)
    {
        var overlappingRanges = 0;

        foreach (var elfPair in elfPairs)
        {
            if ((elfPair.ElfOneStart <= elfPair.ElfTwoStart && elfPair.ElfOneEnd >= elfPair.ElfTwoStart)
                ||
                (elfPair.ElfOneStart <= elfPair.ElfTwoEnd && elfPair.ElfOneEnd >= elfPair.ElfTwoEnd)
                ||
                (elfPair.ElfTwoStart <= elfPair.ElfOneStart && elfPair.ElfTwoEnd >= elfPair.ElfOneStart)
                ||
                (elfPair.ElfTwoStart <= elfPair.ElfOneEnd && elfPair.ElfTwoEnd >= elfPair.ElfOneEnd))
            {
                overlappingRanges++;
            }
        }

        return overlappingRanges;
    }
}

public class ElfPair
{
    public int ElfOneStart { get; set; }
    public int ElfOneEnd { get; set; }
    public int ElfTwoStart { get; set; }
    public int ElfTwoEnd { get; set; }
}
