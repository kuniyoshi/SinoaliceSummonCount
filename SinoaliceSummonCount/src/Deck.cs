using System;
using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public class Deck
    {

        enum Status
        {

            UnUsed,
            Consumed,

        }
        
        readonly List<Pair<Status, Buki>> _bukis;

        readonly Equipment _equipment;

        public Deck(Equipment equipment)
        {
            _equipment = equipment;
            _bukis = new List<Pair<Status, Buki>>();
            InitializeBukis();
        }

        public void ConsumeBuki(Buki buki)
        {
            var index = _bukis.FindIndex(p => p.Second.Id == buki.Id);
            var pair = _bukis[index];
            pair.First = Status.Consumed;

            _bukis[index] = pair;
        }

        public Slot Open()
        {
            var candidate = _bukis
                .Where(p => p.First == Status.UnUsed)
                .Select(p => p.Second)
                .Take(Constant.SlotSize);

            var slot = new Slot(candidate);
            
            return slot;
        }

        public void Reset()
        {
            InitializeBukis();
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace<Deck>()}{{Bukis: {string.Join(", ", _bukis.Select(p => p.Second))}}}";
        }

        void InitializeBukis()
        {
            _bukis.Clear();

            Action<int, FrontendBuki> addRangeFrontend = (c, b) =>
            {
                _bukis.AddRange(
                    Enumerable.Range(0, c)
                        .Select(_ => new Pair<Status, Buki>(Status.UnUsed, new Buki(b)))
                );
            };

            Action<int, BackendBuki> addRangeBackend = (c, b) =>
            {
                _bukis.AddRange(
                    Enumerable.Range(0, c)
                        .Select(_ => new Pair<Status, Buki>(Status.UnUsed, new Buki(b)))
                );
            };

            addRangeFrontend(_equipment.Sword, FrontendBuki.Sword);
            addRangeFrontend(_equipment.Lance, FrontendBuki.Lance);
            addRangeFrontend(_equipment.Bow, FrontendBuki.Bow);
            addRangeFrontend(_equipment.Hammer, FrontendBuki.Hammer);

            addRangeBackend(_equipment.Wand, BackendBuki.Wand);
            addRangeBackend(_equipment.MusicInstrument, BackendBuki.MusicalInstrument);
            addRangeBackend(_equipment.Book, BackendBuki.Book);
            addRangeBackend(_equipment.MagicItem, BackendBuki.MagicItem);

            Util.Shuffle(_bukis);
        }

    }

}
