using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
    internal class Day2 : DayModel
    {
        private int _greenCount = 13, _blueCount = 14, _redCount = 12;
        private Regex _red = new Regex(@"\d+ red");
        private Regex _green = new Regex(@"\d+ green");
        private Regex _blue = new Regex(@"\d+ blue");

        public Day2(string inputFilePath) : base(inputFilePath)
        {
        }

        public int Task1()
        {
            int result = 0;

            foreach (string line in _input)
            {
                if(_red.Matches(line).Any(match => getCubeCount(match.Value) > _redCount)
                    || _green.Matches(line).Any(match => getCubeCount(match.Value) > _greenCount)
                    || _blue.Matches(line).Any(match => getCubeCount(match.Value) > _blueCount))
                {
                    continue;
                }

                result += getGameID(line);
            }
            return result;
        }
        public long Task2()
        {
            long result = 0;

            foreach (string line in _input)
            {
                int power = _red.Matches(line).Max(match => getCubeCount(match.Value)) * _green.Matches(line).Max(match => getCubeCount(match.Value)) * _blue.Matches(line).Max(match => getCubeCount(match.Value));

                result += power;
            }
            return result;
        }
        private int getGameID(string input)
        {
            return Int32.Parse(input.Substring(5, input.IndexOf(':')-5));
        }
        private int getCubeCount(string input)
        {
            return Int32.Parse(input.Substring(0, input.IndexOf(' ')));
        }
    }
}
