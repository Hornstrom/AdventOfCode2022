namespace AdventOfCode.Day1;

public class Day1 : Day
{
    private CalorieCounter _calorieCounter = new CalorieCounter();
    public void Part1()
    {
        _calorieCounter.WhoCarriesTheMost();
    }

    public void Part2()
    {
        _calorieCounter.TopThreeCarriesTheMost();
    }
}
