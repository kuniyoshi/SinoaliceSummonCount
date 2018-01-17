using System.Linq;

namespace SinoaliceSummonCount
{

    public class Member
    {

        static Buki SelectPreferredBuki(Slot slot, Sinma.Base sinma)
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

        static Buki SelectUnPreferredBuki(Slot slot, Sinma.Base sinma)
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

        static Buki SelectRandomBuki(Slot slot)
        {
            var index = Environment.RandomRange(0, slot.Bukis.Length);
            return slot.Bukis[index];
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

        public Buki Act(int? sinmaCountDown, Sinma.Base sinma)
        {
            var slot = Deck.Open();

            if (!slot.Bukis.Any())
            {
                Deck.Reset();
                return null;
            }

            Buki buki;

            switch (sinma?.SinmaState)
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
                case null:
                    buki = SelectRandomBuki(slot);
                    break;
                default:
                    buki = SelectRandomBuki(slot);
                    break;
            }

            Deck.ConsumeBuki(buki);

            return buki;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace<Member>()}{{Name: {Name}, Job: {Job}, Deck: {Deck}}}";
        }

    }

}
