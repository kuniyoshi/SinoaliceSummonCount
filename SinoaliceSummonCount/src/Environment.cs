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

        public static bool DoesJobStrongWith(Job job, Buki buki)
        {
            if (buki == null)
            {
                return false;
            }
            
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

        public static float CalculateScore(Result result, int totalTurn)
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
                    var e = ParseLog(r);
                    return map[e];
                });

            return (float) score / totalTurn;
        }

        public static int RandomRange(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static Result Simulate(Guild guild,
                               int totalTurnDuringBattring,
                               Sinma.Base firstSinma,
                               Sinma.Base secondSinma)
        {
            for (var turn = 0; turn < totalTurnDuringBattring; ++turn)
            {
                Sinma.Base sinma;

                if (!firstSinma.DidGone)
                {
                    sinma = firstSinma;
                }
                else if (!secondSinma.DidGone)
                {
                    sinma = secondSinma;
                }
                else
                {
                    sinma = null;
                }

                var records = guild.Act(turn, sinma);
//                Console.Out.WriteLine($"records: {string.Join(", ", records)}");
                Console.Out.WriteLine($"records count: {records.Count}");

                sinma?.PassTurn();
            }

            var result = new Result(
                firstRequiredTurnCount: firstSinma.TurnCountToSummon,
                secondRequiredTurnCount: secondSinma.TurnCountToSummon,
                records: guild.Records
            );

            return result;
        }

        public static string[] Summarize(IReadOnlyList<Record> records)
        {
            var summary = records.Where(r => r.SinmaState == SinmaState.Summoning)
                .GroupBy(r => r.Id)
                .Select(g =>
                {
                    var members = g.Select(l => $"{l.DidSinmaPrefer}")
                        .ToArray();
                    return $"{g.Key}: {string.Join(", ", members)}";
                }).ToArray();
            return summary;
        }

    }

}
