
namespace AdventOfCode
{
    internal class Day4 : DayModel
    {
        public Day4(string inputFilePath) : base(inputFilePath)
        {
        }

        public override long Task1()
        {
            long result = 0;

            foreach(string line in _input)
            {
                int winningNumbersStart = line.IndexOf(':') + 1;
                int numbersWeHaveStart = line.IndexOf('|');
                HashSet<string> winningNumbers = line.Substring(winningNumbersStart, numbersWeHaveStart - winningNumbersStart - 1).Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                var numbersWeHave = line.Substring(numbersWeHaveStart + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var matches = numbersWeHave.Where(number => winningNumbers.Contains(number));

                result += Convert.ToInt64(Math.Pow(2, matches.Count() - 1));
            }
            return result;
        }
        public override long Task2()
        {

            int[] cards = Enumerable.Repeat(1, _input.Length).ToArray();
            for(int i = 0; i < _input.Length; i++)
            {
                string line = _input[i];
                int winningNumbersStart = line.IndexOf(':') + 1;
                int numbersWeHaveStart = line.IndexOf('|');
                HashSet<string> winningNumbers = line.Substring(winningNumbersStart, numbersWeHaveStart - winningNumbersStart - 1).Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                var numbersWeHave = line.Substring(numbersWeHaveStart + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var matchesCount = numbersWeHave.Where(number => winningNumbers.Contains(number)).Count();

                for(int j = i+1; j <= i + matchesCount; j++)
                {
                    cards[j] += cards[i];
                }
            }

            return cards.Sum();
        }
    }
}
