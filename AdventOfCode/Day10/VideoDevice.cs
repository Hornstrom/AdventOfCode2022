using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day10
{
    public class VideoDevice
    {
        private int _registerX = 1;
        private int _cycleNumber = 0;
        private List<int> _signalStrengths = new List<int>();
        private char[,] _crtDisplay = new char[40,6];

        public VideoDevice()
        {
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    _crtDisplay[i,j] = '.';
                }
            }
        }

        public void RunCommands(string[] data)
        {
            foreach (var line in data)
            {
                var lineSplit = line.Split(' ');

                switch (lineSplit[0])
                {
                    case "noop":
                        Noop();
                        break;
                    case "addx":
                        AddX(int.Parse(lineSplit[1]));
                        break;
                    default:
                        throw new Exception("Unexpected command");
                }
            }
        }

        private void Noop()
        {
            AddCycle();
        }
        
        private void AddX(int x)
        {
            AddCycle();
            AddCycle();
            _registerX += x;
        }

        private void AddCycle()
        {
            var currentLine = _cycleNumber / 40;
            
            if (currentLine < 6 && (_registerX == _cycleNumber % 40 || _registerX + 1 == _cycleNumber % 40 || _registerX - 1 == _cycleNumber % 40))
            {
                _crtDisplay[_cycleNumber % 40, currentLine] = '#';
            }
            
            _cycleNumber++;
            var wantedCycles = new List<int>()
            {
                1, 40, 20, 60, 100, 140, 180, 220
            };
            if (wantedCycles.Contains(_cycleNumber))
            {
                var signalStrength = _cycleNumber * _registerX;
                _signalStrengths.Add(signalStrength);
                //Console.WriteLine($"Signal strength at clock cycle {_cycleNumber} is {signalStrength}");
            }

            
        }

        public void PrintCrtDisplay()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Console.Write(_crtDisplay[j,i]);
                }
                Console.WriteLine();
            }
        }

        public int SumOfSignalStrength()
        {
            return _signalStrengths.Sum();
        }
    }
}