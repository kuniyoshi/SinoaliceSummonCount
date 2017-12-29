using System.Linq;

namespace SinoaliceSummonCount
{

    public class Member
    {

        static Buki SelectPreferredBuki(Slot slot, Sinma sinma)
        {
            var candidates = slot.Bukis
                .Where(sinma.DoesPrefer)
                .ToArray();

            if (!candidates.Any())
            {
                candidates = slot.Bukis;
            }

            var index = Environment.RandomRange(0, candidates.Length);
            return candidates[index];
        }

        static Buki SelectUnPreferredBuki(Slot slot, Sinma sinma)
        {
            var candidates = slot.Bukis
                .Where(buki => !sinma.DoesPrefer(buki))
                .ToArray();

            if (!candidates.Any())
            {
                candidates = slot.Bukis;
            }

            var index = Environment.RandomRange(0, candidates.Length);
            return candidates[index];
        }

        public readonly Deck Deck;

        public readonly Job Job;

        public readonly string Name;

        public Member(Job job, Deck deck, string name)
        {
            Job = job;
            Deck = deck;
            Name = name;
        }

        public Buki Act(SinmaState sinmaState, int? sinmaCountDown, Sinma sinma)
        {
            var slot = Deck.Open();

            if (!slot.Bukis.Any())
            {
                Deck.Reset();
                return null;
            }

            Buki buki;

            switch (sinmaState)
            {
                case SinmaState.NoSign:
                    buki = SelectUnPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Signed:
                    buki = SelectUnPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Summoning:
                    buki = SelectPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Blessed:
                    buki = SelectPreferredBuki(slot, sinma);
                    break;
                default:
                    buki = SelectUnPreferredBuki(slot, sinma);
                    break;
            }

            return buki;
        }

    }

}
