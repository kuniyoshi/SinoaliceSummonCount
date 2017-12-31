using System;
using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public static class GuildBuilder
    {

        public static Guild Build(string text)
        {
            var lines = text.Split('\n');

            var list = new List<Member>();
            
            foreach (var line in lines)
            {
                if (line == "")
                {
                    continue;
                }
                
                var columuns = line.Split('\t');
                
                if (!columuns.Any())
                {
                    continue;
                }
                
                var name = columuns[0];
                var job = ParseJob(columuns[1]);
                var deck = GenerateDeck(columuns.Skip(2).Take(8).ToArray());
                list.Add(new Member(job, deck, name));
            }
            
            var guild = new Guild(
                list.ToArray()
            );

            return guild;
        }

        static Deck GenerateDeck(string[] bukis)
        {
            var counts = bukis
                .Select(int.Parse)
                .ToArray();
            
            var equipment = new Equipment(
                sword: counts[0],
                lance: counts[1],
                bow: counts[2],
                hammer: counts[3],
                wand: counts[4],
                musicInstrument: counts[5],
                book: counts[6],
                magicItem: counts[7]
            );

            var deck = new Deck(equipment);

            return deck;
        }

        static Job ParseJob(string job)
        {
            var parsed = (Job) Enum.Parse(typeof(Job), job);
            return parsed;
        }

    }

}
