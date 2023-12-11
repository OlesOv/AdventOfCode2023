using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdventOfCode
{
    internal class Day11 : DayModel
    {
        private List<Coord> coord = new();
        public Day11(string inputFilePath) : base(inputFilePath)
        {
            for (int row = 0; row < _input.Length; row++)
            {
                var matches = Regex.Matches(_input[row], "#");
                coord.AddRange(matches.Select(m => new Coord(m.Index, row)));
            }
        }

        public override long Task1()
        {
            expandUniverse(2);
            return getSumOfDistances();
        }
        public override long Task2()
        {
            expandUniverse(1000000);
            return getSumOfDistances();
        }
        private void expandUniverse(long emptyBecomes)
         {
            var addCoord = emptyBecomes - 1;
            for (int j = _input[0].Length-1; j > 0; j--)
            {
                if(_input.All(row => row[j] != '#'))
                {
                    coord.Where(c => c.X > j).ToList().ForEach(c => c.X += addCoord);
                }
            }
            for(int i = _input.Length - 1; i > 0; i--)
            {
                if (!_input[i].Contains('#'))
                {
                    coord.Where(c => c.Y > i).ToList().ForEach(c => c.Y += addCoord);
                }
            }
        }
        private long getSumOfDistances()
        {
            long result = 0;
            for(int i = 0; i < coord.Count-1; i++)
            {
                for(int j = i+1; j < coord.Count; j++)
                {
                    result += Math.Abs(coord[i].X - coord[j].X) + Math.Abs(coord[i].Y - coord[j].Y);
                }
            }
            return result;
        }
        private class Coord
        {
            public long X { get; set; }
            public long Y { get; set; }
            public Coord(long x, long y)
            {
                X = x; Y = y;
            }
            public override string ToString()
            {
                return $"{{X: {X}; Y: {Y}}}";
            }
        }
    }
}