using System;

namespace SinoaliceSummonCount
{

    public static class Environment
    {

        static Random _random;

//        static readonly Effect PreferredEffect = new Effect(50);

//        static readonly Effect UnPreferredEffect = new Effect(30);

        public static void GenRandom()
        {
            _random = new Random();
        }

        public static void DeleteRandom()
        {
            _random = null;
        }

        public static int RandomRange(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static bool DoesJobStrongWith(Job job, Buki buki)
        {
            switch (job)
            {
                case Job.Breaker:
                    return buki.IsSameAs(FrontendBuki.Sword);
                case Job.Crasher:
                    return buki.IsSameAs(FrontendBuki.Hammer);
                case Job.Gunner:
                    return buki.IsSameAs(FrontendBuki.Bow);
                case Job.Paladin:
                    return buki.IsSameAs(FrontendBuki.Lance);
                case Job.Minstrel:
                    return buki.IsSameAs(BackendBuki.MusicalInstrument);
                case Job.Sorcerer:
                    return buki.IsSameAs(BackendBuki.Book);
                case Job.Cleric:
                    return buki.IsSameAs(BackendBuki.Wand);
                default:
                    throw new ArgumentOutOfRangeException(nameof(job), job, null);
            }
        }

    }

}
