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
                FrontendBuki.Bow
            );
            List<Record> logs;
            logs = guid.Act(1, SinmaState.NoSign, sinma);
            Console.Out.WriteLine(string.Join(", ", logs));
            logs = guid.Act(2, SinmaState.Signed, sinma);
            Console.Out.WriteLine(string.Join(", ", logs));
            logs = guid.Act(3, SinmaState.Signed, sinma);
            Console.Out.WriteLine(string.Join(", ", logs));
            logs = guid.Act(4, SinmaState.Signed, sinma);
            Console.Out.WriteLine(string.Join(", ", logs));
            logs = guid.Act(5, SinmaState.Summoning, sinma);
            Console.Out.WriteLine(string.Join(", ", logs));
        }

        static string Usage()
        {
            return $"usage: script filename <filename>";
        }

    }

}
