using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SinoaliceSummonCount
{

    public static class Simulation
    {

        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                throw new Exception(Usage());
            }

            var candidate = args[0];

            if (!File.Exists(candidate))
            {
                throw new FileNotFoundException(Usage(), candidate);
            }

            var filename = candidate;

            Environment.GenRandom();

            using (var reader = new StreamReader(filename, Encoding.UTF8))
            {
                var text = reader.ReadToEnd();
                var guild = GuildBuilder.Build(text);
                var firstSinma = new Sinma.YakusaiOfHyosetsu(54);
                var secondSinma = new Sinma.YakusaiOfRaiko(53);

                var result = Simulate(
                    guild: guild,
                    totalTurnDuringBattring: 160,
                    firstSinma: firstSinma,
                    secondSinma: secondSinma
                );

                Console.Out.WriteLine($"{firstSinma.Name} required {firstSinma.TurnCountToSummon}");
                Console.Out.WriteLine($"{secondSinma.Name} required {secondSinma.TurnCountToSummon}");

                var score = Environment.CalculateScore(result);
                Console.Out.WriteLine($"score: {score}");
//                Console.Out.WriteLine($"result: {result}");
            }

            Environment.DeleteRandom();
        }

        static Result Simulate(Guild guild,
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
                Console.Out.WriteLine($"records: {string.Join(", ", records)}");

                firstSinma.PassTurn();
                secondSinma.PassTurn();
            }

            var result = new Result(
                firstRequiredTurnCount: firstSinma.TurnCountToSummon,
                secondRequiredTurnCount: secondSinma.TurnCountToSummon,
                records: guild.Records
            );

            return result;
        }

        static int CountValidSummoning(IEnumerable<Record> logs)
        {
            var count = logs.Count(r =>
            {
                var isSummoning = r.SinmaState == SinmaState.Summoning;
                return isSummoning && r.DidSinmaPrefer;
            });
            return count;
        }

        static IEnumerable<Effect> ParseEffects(IEnumerable<Record> logs)
        {
            var list = logs.Select(Environment.ParseLog)
                .ToList();
            return list;
        }

        static string Usage()
        {
            return $"usage: script filename <filename>";
        }

    }

}
