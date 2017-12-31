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
                var guid = GuildBuilder.Build(text);
                Console.Out.WriteLine(guid);

                Simulate(guid);
            }

            Environment.DeleteRandom();
        }

        static void Simulate(Guild guid)
        {
            var sinma = new Sinma(
                BackendBuki.Wand,
                FrontendBuki.Hammer,
                FrontendBuki.Bow,
                Constant.CountToSummon
            );
            var summoningCount = 0;
            var turn = 0;

            Func<SinmaState> stateGenerator = () =>
            {
                if (turn < 20)
                {
                    return SinmaState.NoSign;
                }
                if (turn < 23)
                {
                    return SinmaState.Signed;
                }
                return SinmaState.Summoning;
            };

            while (summoningCount < Constant.CountToSummon)
            {
                var state = stateGenerator();
                var logs = guid.Act(turn++, state, sinma);
                Console.Out.WriteLine(string.Join(", ", logs));
                summoningCount = summoningCount + CountValidSummoning(logs);
            }

            Console.Out.WriteLine($"turn: {turn-24}");
        }

        static int CountValidSummoning(List<Record> logs)
        {
            var count = logs.Count(r =>
            {
                var isSummoning = r.SinmaState == SinmaState.Summoning;
                return isSummoning && r.DidSinmaPrefer;
            });
            return count;
        }

        static string Usage()
        {
            return $"usage: script filename <filename>";
        }

    }

}
