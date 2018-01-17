using System;
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
                var firstSinma = new Sinma.YakusaiOfHyosetsu(hidingTurnCount: 54);
                var secondSinma = new Sinma.YakusaiOfRaiko(hidingTurnCount: 53);
                var totalTurn = 160;

                var result = Environment.Simulate(
                    guild: guild,
                    totalTurnDuringBattring: totalTurn,
                    firstSinma: firstSinma,
                    secondSinma: secondSinma
                );

//                Console.Out.WriteLine($"{firstSinma.Name} required {result.FirstRequiredTurnCount}");
//                Console.Out.WriteLine($"{secondSinma.Name} required {result.SecondRequiredTurnCount}");

                var score = Environment.CalculateScore(result, totalTurn);
//                Console.Out.WriteLine($"score: {score}");

                var summary = Environment.Summarize(result.Records);
//                Console.Out.WriteLine($"summary: {string.Join("\n", summary)}");

                var output = string.Join(
                    "\t",
                    result.FirstRequiredTurnCount.HasValue ? result.FirstRequiredTurnCount.Value.ToString() : "NULL",
                    result.SecondRequiredTurnCount.HasValue ? result.SecondRequiredTurnCount.Value.ToString() : "NULL",
                    score
                );
                Console.Out.WriteLine(output);
            }

            Environment.DeleteRandom();
        }


        static string Usage()
        {
            return $"usage: script filename <filename>";
        }

    }

}
