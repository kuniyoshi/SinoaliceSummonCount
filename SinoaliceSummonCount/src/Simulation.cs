using System;
using System.Text;

namespace SinoaliceSummonCount
{

    public static class Simulation
    {

        public static void Main()
        {
            Environment.GenRandom();

            var sinmaString = ReadLine();
            var text = ReadToEnd();

            var sinma = SinmaBuilder.Build(sinmaString);
            var guild = GuildBuilder.Build(text);
            var totalTurn = 160;

            var result = Environment.Simulate(
                guild: guild,
                totalTurnDuringBattring: totalTurn,
                firstSinma: sinma.First,
                secondSinma: sinma.Second
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

            Environment.DeleteRandom();
        }

        static string ReadLine()
        {
            return Console.ReadLine();
        }

        static string ReadToEnd()
        {
            var b = new StringBuilder();
            string s;

            do
            {
                s = ReadLine();
                b.AppendLine(s);
            } while (s != null);

            return b.ToString();
        }

    }

}
