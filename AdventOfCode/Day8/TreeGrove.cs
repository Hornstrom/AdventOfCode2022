namespace AdventOfCode.Day8;

public class TreeGrove
{
    private Tree[,] _plantation;
    // private int[] _hiddenWest;
    // private int[] _hiddenNorth;
    // private int[] _hiddenSouth;
    // private int[] _hiddenEast;

    public TreeGrove(string[] data)
    {
        _plantation = new Tree[data.Length, data[0].Length];
        // _hiddenEast = new int[data.Length];
        // _hiddenWest = new int[data.Length];
        // _hiddenNorth = new int[data[0].Length];
        // _hiddenSouth = new int[data[0].Length];
        
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data[i].Length; j++)
            {
                _plantation[j, i] = new Tree
                {
                    Height = (int) char.GetNumericValue(data[i][j]),
                    HiddenFromNorth = false,
                    HiddenFromSouth = false,
                    HiddenFromEast = false,
                    HiddenFromWest = false
                };
            }
        }
        
        SetVisibilities(data[0].Length,data.Length);
        SetScores(data[0].Length,data.Length);
    }

    public void SetVisibilities(int x, int y)
    {
        var previousTallestNorth = -1;
        var previousTallestSouth = -1;
        var previousTallestEast = -1;
        var previousTallestWest = -1;
        
        for (int i = 0; i < x; i++)
        {
            previousTallestNorth = -1;
            previousTallestSouth = -1;
            for (int j = 0; j < y; j++)
            {
                // Check North
                if (_plantation[i, j].Height <= previousTallestNorth)
                {
                    _plantation[i, j].HiddenFromNorth = true;
                }
                else
                {
                    previousTallestNorth = _plantation[i, j].Height;
                }
                
                // Check South
                if (_plantation[i, y - j - 1].Height <= previousTallestSouth)
                {
                    _plantation[i, y - j - 1].HiddenFromSouth = true;
                }
                else
                {
                    previousTallestSouth = _plantation[i, y - j - 1].Height;
                }
            }
        }
        
        for (int i = 0; i < y; i++)
        {
            previousTallestEast = -1;
            previousTallestWest = -1;
            for (int j = 0; j < x; j++)
            {
                // Check West
                if (_plantation[j, i].Height <= previousTallestWest)
                {
                    _plantation[j, i].HiddenFromWest = true;
                }
                else
                {
                    previousTallestWest = _plantation[j, i].Height;
                }
                
                // Check East
                if (_plantation[x - j - 1,  i].Height <= previousTallestEast)
                {
                    _plantation[x - j - 1,  i].HiddenFromEast = true;
                }
                else
                {
                    previousTallestEast = _plantation[x - j - 1,  i].Height;
                }
            }
        }
    }

    public void SetScores(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                _plantation[i, j].Score = GetScore(i, j, x, y);
            }
        }
    }

    public int ScoreOfBestTree()
    {
        var maxScore = 0;
        foreach (var tree in _plantation)
        {
            if (tree.Score > maxScore)
            {
                maxScore = tree.Score;
            }
        }

        return maxScore;
    }

    private int GetScore(int x, int y, int maxX, int maxY)
    {
        var height = _plantation[x, y].Height;
        var scoreSouth = 0;
        var scoreNorth = 0;
        var scoreEast = 0;
        var scoreWest = 0;
        
        for (int i = x + 1; i < maxX; i++)
        {
            scoreEast++;
            if (_plantation[i, y].Height < height)
            {
                
            }
            else
            {
                break;
            }
        }

        
        for (int i = x - 1; i >= 0; i--)
        {
            scoreWest++;
            if (_plantation[i, y].Height < height)
            {
                
            }
            else
            {
                break;
            }
        }
        
        for (int i = y + 1; i < maxY; i++)
        {
            scoreSouth++;
            if (_plantation[x, i].Height < height)
            {
                
            }
            else
            {
                break;
            }
        }
        
        for (int i = y - 1; i >= 0; i--)
        {
            scoreNorth++;
            if (_plantation[x, i].Height < height)
            {
                
            }
            else
            {
                break;
            }
        }

        return scoreEast * scoreNorth * scoreSouth * scoreWest;
    }

    public int GetNumberHiddenTrees()
    {
        var result = 0;
        foreach (var tree in _plantation)
        {
            if (tree.Hidden)
            {
                result++;
            }
        }

        return result;
    }
    
    public int GetNumberOfVisibleTrees()
    {
        var result = 0;
        foreach (var tree in _plantation)
        {
            if (!tree.Hidden)
            {
                result++;
            }
        }

        return result;
    }

    public class Tree
    {
        public int Height { get; set; }
        public bool HiddenFromNorth { get; set; }
        public bool HiddenFromSouth { get; set; }
        public bool HiddenFromEast { get; set; }
        public bool HiddenFromWest { get; set; }
        public int Score { get; set; }

        public bool Hidden
        {
            get { return HiddenFromEast && HiddenFromNorth && HiddenFromWest && HiddenFromSouth; }
        }
    }
}
