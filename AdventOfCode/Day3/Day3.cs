using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Day3 : DayModel
    {
        public Day3(string inputFilePath) : base(inputFilePath)
        {
        }

        public override long Task1()
        {
            long result = 0;

            for(int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];
                var NumbersInThisLine = Regex.Matches(line, @"\d+");

                foreach (Match number in NumbersInThisLine)
                {
                    if(getAdjacentSequences(number, i, new Regex(@"[^\.\d]")).Count() != 0)
                    {
                        result += Int32.Parse(number.Value);
                    }
                }
            }
            return result;
        }
        public override long Task2()
        {
            long result = 0;

            for (int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];
                var starsInThisLine = Regex.Matches(line, @"\*");

                foreach (Match star in starsInThisLine)
                {
                    IEnumerable<int> numbers = getAdjacentSequences(star, i, new Regex(@"\d+")).Select(str => Int32.Parse(str));
                    if (numbers.Count() == 2)
                    {
                        result += numbers.First() * numbers.Last();
                    }
                }
            }
            return result;
        }

        private IEnumerable<string> getAdjacentSequences(Match centerEntry, int lineIndex, Regex sequenceSymbols)
        {
            List<string> result = new List<string>();
            int prevline = Math.Max(0, lineIndex - 1);
            int nextline = Math.Min(_input.Length - 1, lineIndex + 1);

            int leftIndex = Math.Max(0, centerEntry.Index - 1);
            int rightIndex = leftIndex + centerEntry.Length-1;
            if (centerEntry.Index != 0) rightIndex++;
            if (rightIndex + 1 < _input[lineIndex].Length) rightIndex++;

            for (int i = prevline; i <= nextline; i++)
            {
                result.AddRange(
                sequenceSymbols.Matches(_input[i]).Where(
                    match => (match.Index >= leftIndex && match.Index <= rightIndex) 
                || (match.Index + match.Length > leftIndex && match.Index + match.Length <= rightIndex)).Select(match => match.Value));
            }
            return result;
        }
    }
}
