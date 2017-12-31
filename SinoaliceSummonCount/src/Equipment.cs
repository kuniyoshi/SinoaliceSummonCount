namespace SinoaliceSummonCount
{

    public struct Equipment
    {

        public readonly int Sword;

        public readonly int Lance;

        public readonly int Bow;

        public readonly int Hammer;

        public readonly int Wand;

        public readonly int MusicInstrument;

        public readonly int Book;

        public readonly int MagicItem;

        public Equipment(int sword,
                         int lance,
                         int bow,
                         int hammer,
                         int wand,
                         int musicInstrument,
                         int book,
                         int magicItem)
        {
            Sword = sword;
            Lance = lance;
            Bow = bow;
            Hammer = hammer;
            Wand = wand;
            MusicInstrument = musicInstrument;
            Book = book;
            MagicItem = magicItem;
        }


    }

}
