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

                var result = Simulate(
                    guid: guid,
                    turnAtStartPreparing: 54
                );
                var score = Environment.ParseResult(result);
                Console.Out.WriteLine(result);
                Console.Out.WriteLine(score);
            }

            Environment.DeleteRandom();
        }

        static Result Simulate(Guild guid,
                               int turnAtStartPreparing)
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
                if (turn < turnAtStartPreparing)
                {
                    return SinmaState.NoSign;
                }
                if (turn < turnAtStartPreparing + Constant.TurnDuringSigning)
                {
                    return SinmaState.Signed;
                }
                return SinmaState.Summoning;
            };

            var effects = new List<Effect>();

            while (summoningCount < Constant.CountToSummon)
            {
                var state = stateGenerator();
                var logs = guid.Act(turn++, state, sinma);
                summoningCount = summoningCount + CountValidSummoning(logs);
                effects.AddRange(ParseEffects(logs));
            }

            var result = new Result(
                requiredTurn: turn - Constant.TurnDuringSigning - turnAtStartPreparing,
                effects: effects.ToArray()
            );

            return result;
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
