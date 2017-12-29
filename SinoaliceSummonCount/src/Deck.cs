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
            Empty,

        }
        
        readonly List<Pair<Status, Buki>> _bukis;

        readonly Equipment _equipment;

        int _cursor;

        public Deck(Equipment equipment)
        {
            _equipment = equipment;
            _bukis = new List<Pair<Status, Buki>>();
            InitializeBukis();
        }

        public void ConsumeAtSlotIndex(int index)
        {
            var slot = Open();
            var buki = slot.Bukis[index];
            var deckIndex = _bukis.FindIndex(p => p.Second.Id == buki.Id);
            var pair = _bukis[deckIndex];
            pair.First = Status.Consumed;

            _bukis[deckIndex] = pair;
        }

        public Slot Open()
        {
            var candidate = _bukis
                .Where(p => p.First == Status.UnUsed)
                .Select(p => p.Second)
                .ToList();
            var slot = new Slot(candidate.GetRange(_cursor, Constant.SlotSize));
            return slot;
        }

        public void Reset()
        {
            InitializeBukis();
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
            _cursor = 0;
        }

    }

}
