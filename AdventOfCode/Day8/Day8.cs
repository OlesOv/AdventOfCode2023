using System.Xml.Linq;

namespace AdventOfCode
{
    internal class Day8 : DayModel
    {
        private readonly string _directions;
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        public Day8(string inputFilePath) : base(inputFilePath)
        {
            _directions = _input[0];

            _nodes = _input.Skip(2).Select(s => new Node(s.Split(" = ", StringSplitOptions.RemoveEmptyEntries)[0])).ToDictionary(x => x.Id);
            foreach (var line in _input.Skip(2))
            {
                string key = line.Split(" = ", StringSplitOptions.RemoveEmptyEntries)[0];
                string left = line.Substring(line.IndexOf('(') + 1, 3);
                string right = line.Substring(line.IndexOf(", ") + 2, 3);
                _nodes[key].Left = _nodes[left];
                _nodes[key].Right = _nodes[right];
            }
        }

        public override long Task1()
        {
            return getStepsToZ(_nodes["AAA"]);
        }
        public override long Task2()
        {
            IEnumerable<Node> currentNodes = _nodes.Where(kv => kv.Key[2] == 'A').Select(kv => kv.Value);
            return LeastCommonMultiply(currentNodes.Select(getStepsToZ));
        }
        private class Node
        {
            public string Id { get; }
            public Node? Right { get; set; }
            public Node? Left { get; set; }
            public Node(string id)
            {
                Id = id;
            }
            public override string ToString() {return Id;}
        }
        private long getStepsToZ(Node node)
        {

            int result = 0;
            for (result = 0; node.Id[2] != 'Z'; result++)
            {
                if (_directions[result % _directions.Length] == 'R')
                {
                    node = node.Right;
                }
                else node = node.Left;
            }
            return result;
        }
        static long GreatestCommonDivider(long x, long y)
        {
            if (y == 0)
            {
                return x;
            }
            else
            {
                return GreatestCommonDivider(y, x % y);
            }
        }

        public static long LeastCommonMultiply(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((x, y) => x * y / GreatestCommonDivider(x, y));
        }
    }
}