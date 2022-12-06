using System.Collections;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day5;

public class CargoOrganizer
{
    private List<Stack<char>> _crateStacks;
    private string[] _commands;

    public CargoOrganizer(int numberOfStacks, int stackHeight, string[] data)
    {
        _crateStacks = new List<Stack<char>>();

        for (int i = 0; i < numberOfStacks; i++)
        {
            var stack = new Stack<char>();
            
            
            for (int j = stackHeight - 1; j >= 0; j--)
            {
                var line = data[j].ToCharArray();
                var crate = line[1 + i * 4];
                if (crate != ' ')
                {
                    stack.Push(line[1 + i * 4]);
                }
            }
            
            _crateStacks.Add(stack);
        }
        
        _commands = data.Skip(stackHeight + 2).ToArray();
    }

    public string RunCommands()
    {
        foreach (var command in _commands)
        {
            var regexPattern = "[0-9]+";
            var regex = new Regex(regexPattern);
            var matches = regex.Matches(command);
            var numberToMove = int.Parse(matches[0].ToString());
            var sourceStackIndex = int.Parse(matches[1].ToString()) - 1;
            var targetStackIndex = int.Parse(matches[2].ToString()) - 1;

            for (var i = 0; i < numberToMove; i++)
            {
                var crateToMove = _crateStacks.ElementAt(sourceStackIndex).Pop();
                _crateStacks.ElementAt(targetStackIndex).Push(crateToMove);
            }
        }

        var result = "";
        foreach (var stack in _crateStacks)
        {
            result += stack.Peek();
        }

        return result;
    }
    
    public string RunCommandsPart2()
    {
        foreach (var command in _commands)
        {
            var regexPattern = "[0-9]+";
            var regex = new Regex(regexPattern);
            var matches = regex.Matches(command);
            var numberToMove = int.Parse(matches[0].ToString());
            var sourceStackIndex = int.Parse(matches[1].ToString()) - 1;
            var targetStackIndex = int.Parse(matches[2].ToString()) - 1;

            var stackInBetween = new Stack<char>();
            for (var i = 0; i < numberToMove; i++)
            {
                var crateToMove = _crateStacks.ElementAt(sourceStackIndex).Pop();
                stackInBetween.Push(crateToMove);
            }
            
            for (var i = 0; i < numberToMove; i++)
            {
                _crateStacks.ElementAt(targetStackIndex).Push(stackInBetween.Pop());
            }
        }

        var result = "";
        foreach (var stack in _crateStacks)
        {
            result += stack.Peek();
        }

        return result;
    }
}
