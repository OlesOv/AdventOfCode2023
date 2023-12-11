using System.Xml.Linq;

namespace AdventOfCode
{
    internal class Day9 : DayModel
    {
        public Day9(string inputFilePath) : base(inputFilePath)
        {
        }

        public override long Task1()
        {
            long result = 0;
            foreach (var line in _input)
            {
                var currentNums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                result += findRowNext(currentNums);
            }
            return result;
        }
        public override long Task2()
        {
            long result = 0;
            foreach (var line in _input)
            {
                var currentNums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
                result += findRowPrev(currentNums);
            }
            return result;
        }
        private List<long> GetDiffs(List<long> nums)
        {
            List<long> result = new List<long>();
            for(int i = 1; i < nums.Count() ; i++)
            {
                result.Add(nums[i] - nums[i - 1]);
            }
            return result;
        }
        private long findRowNext(List<long> nums)
        {
            var diffs = GetDiffs(nums);
            if (diffs.All(d => d == 0))
            {
                return nums.Last();
            }
            else return findRowNext(diffs) + nums.Last();
        }
        private long findRowPrev(List<long> nums)
        {
            var diffs = GetDiffs(nums);
            if (diffs.All(d => d == 0))
            {
                return nums.First();
            }
            else return nums.First() - findRowPrev(diffs);
        }
    }
}