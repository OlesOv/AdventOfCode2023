using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day1 : DayModel
    {

        public Day1(string inputFilePath) : base(inputFilePath)
        {
        }

        public override long Task1()
        {
            long result = 0;

            Regex digitsFromLeft = new Regex(@"\d");
            Regex digitsFromRight = new Regex(@"\d", RegexOptions.RightToLeft);
            foreach (string line in _input)
            {
                string current = line;

                result += 10 * stringToInt(digitsFromLeft.Match(current).Value) + stringToInt(digitsFromRight.Match(current).Value);
            }
            return result;
        }
        public override long Task2()
        {
            long result = 0;

            Regex digitsFromLeft = new Regex(@"one|two|three|four|five|six|seven|eight|nine|\d");
            Regex digitsFromRight = new Regex(@"one|two|three|four|five|six|seven|eight|nine|\d", RegexOptions.RightToLeft);
            foreach (string line in _input)
            {
                string current = line;

                result += 10 * stringToInt(digitsFromLeft.Match(current).Value) + stringToInt(digitsFromRight.Match(current).Value);
            }
            return result;
        }
        private int stringToInt(string input)
        {
            switch (input)
            {
                case "one": return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
                default: return int.Parse(input);
            }
        }
    }
}
