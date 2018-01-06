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

                var result = Simulate(
                    guild: guild,
                    turnAtStartFirstPreparing: 54,
                    turnAtStartSecondPreparing: 123,
                    totalTurnDuringBattring: 160
                );
                var score = Environment.CalculateScore(result);
                Console.Out.WriteLine($"result: {result}");
                Console.Out.WriteLine($"score: {score}");
            }

            Environment.DeleteRandom();
        }

        static Result Simulate(Guild guild,
                               int turnAtStartFirstPreparing,
                               int turnAtStartSecondPreparing,
                               int totalTurnDuringBattring)
        {
            var remainedTurns = totalTurnDuringBattring;
            Console.Out.WriteLine($"remainedTurns: {remainedTurns}");
            var firstResult = SimulateOne(
                guild: guild,
                turnAtStartPreparing: turnAtStartFirstPreparing,
                summonedCount: 0,
                maxTurn: remainedTurns
            );
            Console.Out.WriteLine($"first result: {firstResult}");
            remainedTurns = remainedTurns - firstResult.PassedTurn;
            Console.Out.WriteLine($"remainedTurns: {remainedTurns}");
            var secondResult = SimulateOne(
                guild: guild,
                turnAtStartPreparing: turnAtStartSecondPreparing,
                summonedCount: 1,
                maxTurn: remainedTurns
            );
            Console.Out.WriteLine($"second result: {secondResult}");
            remainedTurns = remainedTurns - secondResult.PassedTurn;
            Console.Out.WriteLine($"remainedTurns: {remainedTurns}");
            var lastResult = SimulateOne(
                guild: guild,
                turnAtStartPreparing: totalTurnDuringBattring,
                summonedCount: 2,
                maxTurn: remainedTurns
            );
            Console.Out.WriteLine($"last result: {lastResult}");
            Console.Out.WriteLine($"remainedTurns: {remainedTurns}");
            var result = firstResult
                .Add(secondResult)
                .Add(lastResult);
            return result;
        }

        static Result SimulateOne(Guild guild,
                                  int turnAtStartPreparing,
                                  int summonedCount,
                                  int maxTurn)
        {
            var sinma = new Sinma(
                BackendBuki.Wand,
                FrontendBuki.Hammer,
                FrontendBuki.Bow,
                Constant.CountToSummon
            );
            var summoningCount = 0;
            var passedTurn = 0;

            Func<int, SinmaState> stateGenerator;

            if (!Environment.CanSummonInTheButtle(summonedCount))
            {
                stateGenerator = _ => SinmaState.NoSign;
            }
            else
            {
                stateGenerator = current =>
                {
                    if (current < turnAtStartPreparing)
                    {
                        return SinmaState.NoSign;
                    }
                    if (current < turnAtStartPreparing + Constant.TurnDuringSigning)
                    {
                        return SinmaState.Signed;
                    }
                    
                    // TODO: blessed state required
                    
                    return SinmaState.Summoning;
                };
            }

            var effects = new List<Effect>();

            while (summoningCount < Constant.CountToSummon && passedTurn < maxTurn)
            {
                var state = stateGenerator(passedTurn);
                var logs = guild.Act(passedTurn, state, sinma);
                summoningCount = summoningCount + CountValidSummoning(logs);
                effects.AddRange(ParseEffects(logs));
                passedTurn++;
            }

            var result = new Result(
                passedTurn: passedTurn,
                effects: effects.ToArray()
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
