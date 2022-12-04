namespace AdventOfCode.Day2;

public class Day2 : Day
{
    public void Part1()
    {
        var strategyGuide = new StrategyGuide();
        strategyGuide.PrintScore();
    }

    public void Part2()
    {
        var strategyGuide = new ModifiedStrategyGuide();
        strategyGuide.PrintScore();
    }
}
