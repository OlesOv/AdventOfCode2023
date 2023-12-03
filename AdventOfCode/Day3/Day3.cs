using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Day3
{
    internal class Day3 : DayModel
    {
        public Day3(string inputFilePath) : base(inputFilePath)
        {
        }

        public long Task1()
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
        public long Task2()
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
        private int getAdjacentCount(Match match, int lineIndex, Regex symbols)
        {
            int result = 0;
            int prevline = Math.Max(0, lineIndex - 1);
            int nextline = Math.Min(_input.Length-1, lineIndex + 1);

            int prevIndex = Math.Max(0, match.Index - 1);
            int lengthToCheck = match.Length;
            if (match.Index != 0) lengthToCheck++;
            if (prevIndex + lengthToCheck + 1 < _input[lineIndex].Length) lengthToCheck++;

            for (int i = prevline; i <= nextline; i++)
            {
                result += symbols.Matches(_input[i].Substring(prevIndex, lengthToCheck)).Count;
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
