namespace AdventOfCode
{
    internal class Day7 : DayModel
    {
        private List<Hand>? _hands;
        public Day7(string inputFilePath) : base(inputFilePath)
        {
        }

        public override long Task1()
        {
            _hands = _input.Select(s => new Hand(s.Split(' ')[0], Int32.Parse(s.Split(' ')[1]))).ToList();
            long result = 0;
            _hands.Sort();
            var maxRank = _hands.Count();
            for (int i = 0; i < maxRank; i++)
            {
                //Console.WriteLine($"{_hands[i].OriginalInput} {_hands[i].Bid} | {string.Join(',', _hands[i].Cards)} {_hands[i].Strength} {i + 1} {_hands[i].Bid * (i + 1)}");
                result += _hands[i].Bid * (i + 1);
            }
            return result;
        }
        public override long Task2()
        {
            _hands = _input.Select(s => (Hand)new Hand2(s.Split(' ')[0], Int32.Parse(s.Split(' ')[1]))).ToList();
            long result = 0;
            _hands.Sort();
            var maxRank = _hands.Count();
            for (int i = 0; i < maxRank; i++)
            {
                //Console.WriteLine($"{_hands[i].OriginalInput} {_hands[i].Bid} | {string.Join(',', _hands[i].Cards)} {_hands[i].Strength} {i + 1} {_hands[i].Bid * (i + 1)}");
                result += _hands[i].Bid * (i + 1);
            }
            return result;
        }
        private class Hand : IComparable<Hand>
        {
            protected int[] _cards;
            public int[] Cards => _cards;
            public int Bid { get; }
            public byte Strength { get; }
            public string OriginalInput { get; }
            public Hand(string cards, int bid) 
            {
                _cards = cards.Select(c => cardStrength(c)).ToArray();
                Bid = bid;
                Strength = getHandStrength();
                OriginalInput = cards;
            }
            protected virtual byte getHandStrength()
            {
                int[] cardsCount = new int[13];
                foreach (var card in Cards)
                {
                    cardsCount[card]++;
                }
                return CalcHandStrength(cardsCount);
            }
            protected byte CalcHandStrength(int[] cardsCount)
            {
                byte maxPossibleValue = 1;
                if (cardsCount.Any(c => c == 2)) maxPossibleValue = 2;
                if (cardsCount.Count(c => c == 2) == 2) maxPossibleValue = 3;
                if (cardsCount.Any(c => c == 3)) maxPossibleValue = 4;
                if (cardsCount.Any(c => c == 3) && cardsCount.Any(c => c == 2)) maxPossibleValue = 5;
                if (cardsCount.Any(c => c == 4)) maxPossibleValue = 6;
                if (cardsCount.Any(c => c == 5)) maxPossibleValue = 7;

                return maxPossibleValue;
            }
            protected virtual int cardStrength(char card) => "23456789TJQKA".IndexOf(card);

            public int CompareTo(Hand? other)
            {
                if (Strength > other.Strength) return 1;
                else if (Strength < other.Strength) return -1;
                for(int i = 0; i < 5; i++)
                {
                    if (Cards[i] > other.Cards[i]) return 1;
                    if (Cards[i] < other.Cards[i]) return -1;
                }
                return 0;
            }
        }
        private class Hand2 : Hand
        {
            public Hand2(string cards, int bid) : base(cards, bid)
            {
            }
            protected override byte getHandStrength()
            {
                int[] cardsCount = new int[13];
                foreach (var card in Cards)
                {
                    cardsCount[card]++;
                }
                return Math.Max(CalcHandStrength(cardsCount), applyJoker(cardsCount));
            }
            private byte applyJoker(int[] cardsCount)
            {
                var max = cardsCount.Skip(1).Max();
                for(int i = 1; i < cardsCount.Length; i++)
                {
                    if (cardsCount[i] == max)
                    {
                        cardsCount[i] += cardsCount[0];
                        cardsCount[0] = 0;
                        return CalcHandStrength(cardsCount);
                    }
                }
                return 0;
            }

            protected override int cardStrength(char card) => "J23456789TQKA".IndexOf(card);
        }
    }
}