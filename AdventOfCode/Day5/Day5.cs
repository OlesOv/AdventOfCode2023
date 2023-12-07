namespace AdventOfCode
{
    internal class Day5 : DayModel
    {
        private readonly long[]_seedsInput;
        private readonly List<Mapping>[] _mappingGroups = new List<Mapping>[7];
        public Day5(string inputFilePath) : base(inputFilePath)
        {
            _seedsInput = _input[0].Substring(_input[0].IndexOf(':') + 1).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(number => Int64.Parse(number)).ToArray();

            populateMappingGroups();
        }

        public override long Task1()
        {
            return _seedsInput.Min(seed => passItemThroughAllMappings(seed));
        }
        public override long Task2()
        {
            List<Range> seedRanges = new List<Range>();
            for(int i = 0; i < _seedsInput.Length; i+=2)
            {
                seedRanges.Add(new Range(_seedsInput[i], _seedsInput[i] + _seedsInput[i + 1]));
            }

            return seedRanges.Min(range => passRangeThroughAllMappings(range).Min(subrange => subrange.Start));
        }
        private long passItemThroughAllMappings(long startItem)
        {
            long currentItem = startItem;
            foreach(var mappingGroup in _mappingGroups)
            {
                Mapping correspondingMapping = mappingGroup.FirstOrDefault(mapping => mapping.SourceStart <= currentItem && mapping.SourceStart + mapping.MappingLength >= currentItem);
                currentItem = correspondingMapping.DestinationStart + currentItem - correspondingMapping.SourceStart;
            }
            return currentItem;
        }
        private List<Range> passRangeThroughAllMappings(Range range)
        {
            var r = new List<Range> { range };
            foreach (var mappingGroup in _mappingGroups)
            {
                r = passThroughOneMapping(r, mappingGroup);
            }
            return r;
        }

        private List<Range> passThroughOneMapping(List<Range> sourceRanges, List<Mapping> mappingGroup)
        {
            var mappedRanges = new List<Range>();
            foreach (var mapping in mappingGroup)
            {
                var SourceEnd = mapping.SourceStart + mapping.MappingLength;
                var rangesOutsideLastMapping = new List<Range>();
                foreach (var inputRange in sourceRanges)
                {
                    var sourceRangeBeforeMapping = new Range(inputRange.Start, Math.Min(inputRange.End, mapping.SourceStart));
                    var sourceRangeInsideMapping = new Range(Math.Max(inputRange.Start, mapping.SourceStart), Math.Min(SourceEnd, inputRange.End));
                    var sourceRangeAfterMapping = new Range(Math.Max(SourceEnd, inputRange.Start), inputRange.End);

                    if (sourceRangeInsideMapping.IsValid())
                    {
                        mappedRanges.Add(new Range(sourceRangeInsideMapping.Start - mapping.SourceStart + mapping.DestinationStart, sourceRangeInsideMapping.End - mapping.SourceStart + mapping.DestinationStart));
                    }
                    if (sourceRangeBeforeMapping.IsValid())
                    {
                        rangesOutsideLastMapping.Add(sourceRangeBeforeMapping);
                    }
                    if (sourceRangeAfterMapping.IsValid())
                    {
                        rangesOutsideLastMapping.Add(sourceRangeAfterMapping);
                    }
                }
                sourceRanges = rangesOutsideLastMapping;
            }
            mappedRanges.AddRange(sourceRanges);
            return mappedRanges;
        }
        private void populateMappingGroups()
        {
            int mappingGroupIndex = -1;
            int currentGroupStart = -1;
            for (int i = 1; i < _input.Length; i++)
            {
                string line = _input[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    mappingGroupIndex++;
                    _mappingGroups[mappingGroupIndex] = new List<Mapping>();
                    i++;
                    currentGroupStart = i;
                    continue;
                }
                var currentMapping = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                _mappingGroups[mappingGroupIndex].Add(new Mapping(Int64.Parse(currentMapping[0]), Int64.Parse(currentMapping[1]), Int64.Parse(currentMapping[2])));
            }
        }
        private readonly struct Mapping
        {
            public readonly long SourceStart;
            public readonly long DestinationStart;
            public readonly long MappingLength;

            public Mapping(long destinationStart, long sourceStart, long mappingLenght)
            {
                SourceStart = sourceStart;
                DestinationStart = destinationStart;
                MappingLength = mappingLenght;
            }
        }
        private readonly struct Range
        {
            public long Start { get; }
            public long End { get; }
            public Range(long start, long end)
            {
                Start = start;
                End = end;
            }
            public bool IsValid() => Start < End;
        }
    }
}
