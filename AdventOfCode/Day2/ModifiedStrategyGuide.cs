namespace AdventOfCode.Day2;

public class ModifiedStrategyGuide
{
    public string[] StrategyData = System.IO.File.ReadAllLines(@"Day2\data.txt");
    public string[] StrategyDataTest = System.IO.File.ReadAllLines(@"Day2\test_data.txt");

    public List<Play> ParseDataFiles(string[] inputLines)
    {
        var plays = new List<Play>();
        foreach (var line in inputLines)
        {
            var chars = line.ToCharArray();
            var play = new Play(chars.First(), chars.Last());
            plays.Add(play);
        }

        return plays;
    }

    public void PrintScore()
    {
        var testPlays = ParseDataFiles(StrategyDataTest);
        Console.WriteLine($"Total score for part 2 test: {testPlays.Sum(p => p.TotalScore)}");
        
        var plays = ParseDataFiles(StrategyData);
        Console.WriteLine($"Total score for part 2: {plays.Sum(p => p.TotalScore)}");
    }

    public class Play
    {
        public Play(char elfPlay, char outCome)
        {
            ElfPlay = elfPlay;
            MyPlay = CalculateMyPlay(outCome);
            WinScore = CalculateWinScore();
            SignScore = CalculateSignScore();
            TotalScore = WinScore + SignScore;
        }

        private char CalculateMyPlay(char outCome)
        {
            // X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win. Good luck!"
            switch (outCome)
            {
                case 'X':
                {
                    return ElfPlay switch
                    {
                        'A' => 'Z',
                        'B' => 'X',
                        'C' => 'Y',
                        _ => throw new Exception("Unexpected play")
                    };
                } 
                case 'Y':
                {
                    return ElfPlay switch
                    {
                        'A' => 'X',
                        'B' => 'Y',
                        'C' => 'Z',
                        _ => throw new Exception("Unexpected play")
                    };
                } 
                case 'Z':
                {
                    return ElfPlay switch
                    {
                        'A' => 'Y',
                        'B' => 'Z',
                        'C' => 'X',
                        _ => throw new Exception("Unexpected play")
                    };
                }
                default:
                    throw new Exception("Unknown play");
            }
        }

        private int CalculateSignScore()
        {
            return MyPlay switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => throw new Exception("Unknown play")
            };
        }

        private int CalculateWinScore()
        {
            switch (MyPlay)
            {
                case 'X':
                {
                    return ElfPlay switch
                    {
                        'A' => 3,
                        'B' => 0,
                        'C' => 6,
                        _ => throw new Exception("Unexpected play")
                    };
                } 
                case 'Y':
                {
                    return ElfPlay switch
                    {
                        'A' => 6,
                        'B' => 3,
                        'C' => 0,
                        _ => throw new Exception("Unexpected play")
                    };
                } 
                case 'Z':
                {
                    return ElfPlay switch
                    {
                        'A' => 0,
                        'B' => 6,
                        'C' => 3,
                        _ => throw new Exception("Unexpected play")
                    };
                }
                default:
                    throw new Exception("Unknown play");
            }
        }

        public char ElfPlay { get; set; }
        public char MyPlay { get; set; }
        public int WinScore { get; set; }
        public int SignScore { get; set; }
        public int TotalScore { get; set; }
    }
}


