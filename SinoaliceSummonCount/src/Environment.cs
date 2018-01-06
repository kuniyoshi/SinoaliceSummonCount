using System;
using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public static class Environment
    {

        static Random _random;

        #region LifeCycle

        public static void GenRandom()
        {
            _random = new Random();
        }

        public static void DeleteRandom()
        {
            _random = null;
        }

        #endregion

        public static bool CanSummonInTheButtle(int summonedCount)
        {
            return summonedCount < 2;
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

        public static Effect ParseLog(Record log)
        {
            if (log.SinmaState == SinmaState.Blessed)
            {
                return log.IsStrong
                    ? Effect.BlessedStrong
                    : Effect.Blessed;
            }

            return log.IsStrong
                ? Effect.Strong
                : Effect.Normal;
        }

        public static int CalculateScore(Result result)
        {
            var map = new Dictionary<Effect, int>
            {
                {Effect.Normal, 3},
                {Effect.Strong, 4},
                {Effect.Blessed, 5},
                {Effect.BlessedStrong, 6}
            };
            
            var score = result.Records
                .Sum(r =>
                {
                    var e = r.IsStrong ? Effect.Strong : Effect.Normal;

                    if (r.SinmaState == SinmaState.Blessed)
                    {
                        e = r.IsStrong ? Effect.BlessedStrong : Effect.Strong;
                    }
                    
                    return map[e];
                });

            return score;
        }

        public static int RandomRange(int min, int max)
        {
            return _random.Next(min, max);
        }

    }

}
