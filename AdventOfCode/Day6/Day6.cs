namespace AdventOfCode
{
    internal class Day6 : DayModel
    {
        private long[] _times, _distances;
        public Day6(string inputFilePath) : base(inputFilePath)
        {
            _times = new long[1];

            _distances = new long[1];
        }

        public override long Task1()
        {
            _times = _input[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(number => Int64.Parse(number)).ToArray();
            _distances = _input[1].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(number => Int64.Parse(number)).ToArray();

            return getWinWayMultiply();
        }
        public override long Task2()
        {
            _times = new long[1];
            _distances = new long[1];
            _times[0] = long.Parse(_input[0].Split(':')[1].Replace(" ", string.Empty));
            _distances[0] = long.Parse(_input[1].Split(':')[1].Replace(" ", string.Empty));

            return getWinWayMultiply();
        }
        private long getWinWayMultiply()
        {
            long result = 1;
            for (int i = 0; i < _times.Length; i++)
            {
                var allowedTime = _times[i];
                var recordDistance = _distances[i];
                var possibleDistances = new long[(allowedTime + 1) / 2];
                for (int j = 1; j < possibleDistances.Length; j++)
                {
                    possibleDistances[j] = j * (allowedTime - j);
                }
                var winningWays = possibleDistances.Count(distance => distance > recordDistance) * 2 + (allowedTime + 1) % 2;
                result *= winningWays;
            }
            return result;
        }
    }
}