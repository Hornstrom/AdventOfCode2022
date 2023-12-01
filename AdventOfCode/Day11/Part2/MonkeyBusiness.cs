using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day11.Part2
{
    public class MonkeyBusiness
    {
        public List<Monkey> _monkeys = new List<Monkey>();
        
        public MonkeyBusiness(string[] data)
        {
            for (int i = 0; i <= data.Length / 7; i++)
            {
                var start = i * 7;
                var end = start + 6;
                var monkey = new Monkey(data[start..end]);
                _monkeys.Add(monkey);
            }

            var smallestDenominator = 1;
            foreach (var monkey in _monkeys)
            {
                smallestDenominator *= monkey.DivisibleBy;
            }
            
            foreach (var monkey in _monkeys)
            {
                monkey.SmallestDenominator = smallestDenominator;
            }
        }

        public long Play(int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                for (int j = 0; j < _monkeys.Count; j++)
                {
                    var targetsAndItems = _monkeys.ElementAt(j).Inspect();
                    foreach (var targetAndItem in targetsAndItems)
                    {
                        _monkeys.ElementAt(targetAndItem.Item1).Catch(targetAndItem.Item2);
                    }
                }
            }

            var twoMostActive = _monkeys.OrderBy(m => m.NumberOfInspects).TakeLast(2);
            return twoMostActive.ElementAt(0).NumberOfInspects * twoMostActive.ElementAt(1).NumberOfInspects;
        }
    }

    public class Monkey
    {
        private string _name;
        private List<long> _items = new List<long>();
        private char _operator;
        private int? _operationNumber = null;
        public int DivisibleBy { get; }
        private int _trueTarget;
        private int _falseTarget;
        public long NumberOfInspects { get; set; }
        public int SmallestDenominator { get; set; }
        
        public Monkey(string[] monkeyData)
        {
            _name = monkeyData[0][0..8];
            var numbersRegex = new Regex("\\d+");

            var itemMatches = numbersRegex.Matches(monkeyData[1]);
            foreach (Match itemMatch in itemMatches)
            {
                _items.Add(int.Parse(itemMatch.Value));
            }

            if (monkeyData[2].Contains('*'))
            {
                _operator = '*';
            }

            if (monkeyData[2].Contains('+'))
            {
                _operator = '+';
            }
            
            var operationNumberMatches = numbersRegex.Matches(monkeyData[2]);
            if (operationNumberMatches.Count > 0)
            {
                _operationNumber = int.Parse(operationNumberMatches[0].Value);
            }
            
            var divisibleMatch = numbersRegex.Match(monkeyData[3]);
            DivisibleBy = int.Parse(divisibleMatch.Value);
            
            var trueMatch = numbersRegex.Match(monkeyData[4]);
            _trueTarget = int.Parse(trueMatch.Value);
            
            var falseMatch = numbersRegex.Match(monkeyData[5]);
            _falseTarget = int.Parse(falseMatch.Value);
        }

        public List<Tuple<int, long>> Inspect()
        {
            var targetAndItem = new List<Tuple<int, long>>();
            foreach (var item in _items)
            {
                var newItem = CalculateNewWorryLevel(item);
                var targetMonkey = GetTargetMonkey(newItem);
                
                targetAndItem.Add(new Tuple<int, long>(targetMonkey, newItem));
                NumberOfInspects++;
            }
            _items.Clear();
            return targetAndItem;
        }

        private long CalculateNewWorryLevel(long item)
        {
            var operationNumber = _operationNumber ?? item;
            if (_operator == '*')
            {
                item *= operationNumber;
            }
            if (_operator == '+')
            {
                item += operationNumber;
            }

            return item % SmallestDenominator;
        }

        private int GetTargetMonkey(long item)
        {
            if (item % DivisibleBy == 0)
            {
                return _trueTarget;
            }
            else
            {
                return _falseTarget;
            }
        }

        public void Catch(long item)
        {
            _items.Add(item);
        }

    }
}
