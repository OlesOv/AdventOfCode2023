namespace AdventOfCode
{
    internal class Day7 : DayModel
    {
        private List<Hand>? _hands;
        private List<Hand2>? _hands2;
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
                //Console.WriteLine($"{_hands[i].OriginalInput} {_hands[i].Bid} | {string.Join(',', _hands[i].Cards)} {_hands[i].Bid * (i + 1)}");
                result += _hands[i].Bid * (i + 1);
            }
            return result;
        }
        public override long Task2()
        {
            _hands2 = _input.Select(s => new Hand2(s.Split(' ')[0], Int32.Parse(s.Split(' ')[1]))).ToList();
            long result = 0;
            _hands2.Sort();
            var maxRank = _hands2.Count();
            for (int i = 0; i < maxRank; i++)
            {
                Console.WriteLine($"{_hands2[i].OriginalInput} {_hands2[i].Bid} | {string.Join(',', _hands2[i].Cards)} {_hands2[i].Strength} {i+1} {_hands2[i].Bid * (i + 1)}");
                result += _hands2[i].Bid * (i + 1);
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
                if (cardsCount.Any(c => c == 5)) return 7;
                if (cardsCount.Any(c => c == 4)) return 6;
                if (cardsCount.Any(c => c == 3) && cardsCount.Any(c => c == 2)) return 5;
                if (cardsCount.Any(c => c == 3)) return 4;
                if (cardsCount.Count(c => c == 2) == 2) return 3;
                if (cardsCount.Any(c => c == 2)) return 2;
                return 1;
            }
            protected virtual int cardStrength(char card)
            {
                switch (card)
                {
                    case '2': return 0;
                    case '3': return 1;
                    case '4': return 2;
                    case '5': return 3;
                    case '6': return 4;
                    case '7': return 5;
                    case '8': return 6;
                    case '9': return 7;
                    case 'T': return 8;
                    case 'J': return 9;
                    case 'Q': return 10;
                    case 'K': return 11;
                    case 'A': return 12;
                    default:  return -1;
                }
            }

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
                int jokerReplace = 0;
                if(applyJoker(cardsCount, out jokerReplace) >= CalcHandStrength(cardsCount))
                {
                    _cards = Cards.Select(c => c == 0 ? jokerReplace : c).ToArray();
                    return applyJoker(cardsCount, out jokerReplace);
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
            private byte applyJoker(int[] cardsCount, out int increaseIndex)
            {
                var max = cardsCount.Max();
                for(int i = 1; i < cardsCount.Length; i++)
                {
                    if (cardsCount[i] == max)
                    {
                        increaseIndex = i;
                        cardsCount[i] += cardsCount[0];
                        return CalcHandStrength(cardsCount);
                    }
                }
                increaseIndex = 0;
                return 0;
            }

            protected override int cardStrength(char card)
            {
                switch (card)
                {
                    case 'J': return 0;
                    case '2': return 1;
                    case '3': return 2;
                    case '4': return 3;
                    case '5': return 4;
                    case '6': return 5;
                    case '7': return 6;
                    case '8': return 7;
                    case '9': return 8;
                    case 'T': return 9;
                    case 'Q': return 10;
                    case 'K': return 11;
                    case 'A': return 12;
                    default: return -1;
                }
            }
        }
    }
}
//249946396 high


//249770559