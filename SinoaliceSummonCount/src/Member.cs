using System.Linq;

namespace SinoaliceSummonCount
{

    public class Member
    {

        static int SelectPreferredBuki(Slot slot, Sinma.Base sinma)
        {
            var candidates = slot.Bukis
                .Select((b, i) => new {Buki = b, Index = i})
                .Where(b => sinma.DoesPrefer(b.Buki))
                .ToArray();

            if (!candidates.Any())
            {
                candidates = slot.Bukis
                    .Select((b, i) => new {Buki = b, Index = i})
                    .ToArray();
            }

            var index = Environment.RandomRange(0, candidates.Length);

            return candidates[index].Index;
        }

        static int SelectUnPreferredBuki(Slot slot, Sinma.Base sinma)
        {
            var candidates = slot.Bukis
                .Select((b, i) => new {Buki = b, Index = i})
                .Where(b => !sinma.DoesPrefer(b.Buki))
                .ToArray();

            if (!candidates.Any())
            {
                candidates = slot.Bukis
                    .Select((b, i) => new {Buki = b, Index = i})
                    .ToArray();
            }

            var index = Environment.RandomRange(0, candidates.Length);
            
            return candidates[index].Index;
        }

        static int SelectRandomBuki(Slot slot)
        {
            var index = Environment.RandomRange(0, slot.Bukis.Length);
            return index;
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

            int? index;

            switch (sinma?.SinmaState ?? SinmaState.NoSign)
            {
                case SinmaState.NoSign:
                    index = SelectUnPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Signed:
                    index = SelectUnPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Summoning:
                    index = SelectPreferredBuki(slot, sinma);
                    break;
                case SinmaState.Blessed:
                    index = SelectPreferredBuki(slot, sinma);
                    break;
                default:
                    index = SelectRandomBuki(slot);
                    break;
            }

            var buki = slot.Bukis[index.Value];
            Deck.ConsumeAtSlotIndex(index.Value);

            return buki;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace(typeof(Member).ToString())}{{Name: {Name}, Job: {Job}, Deck: {Deck}}}";
        }

    }

}
