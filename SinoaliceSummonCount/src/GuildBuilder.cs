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

//            Console.Out.WriteLine($"text: {text}");
//            Console.Out.WriteLine($"lines length: {lines.Length}");
//            Console.Out.WriteLine($"lines: {string.Join("\n", lines)}");
            if (lines.Length < 4)
            {
                return BuildFixed(lines.First());
            }

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

        static Guild BuildFixed(string line)
        {
            var counts = line.Split('\t');
            var list = new List<Member>();

            var jobs = new[]
            {
                Job.Breaker,
                Job.Paladin,
                Job.Breaker,
                Job.Crasher,
                Job.Paladin,
                Job.Cleric,
                Job.Minstrel,
                Job.Minstrel,
                Job.Cleric,
                Job.Cleric,
                Job.Cleric,
                Job.Sorcerer,
                Job.Sorcerer,
                Job.Sorcerer,
                Job.Cleric
            };

            var names = new[]
            {
                "miyako",
                "uru",
                "suno",
                "taka",
                "j",
                "shirotsuki",
                "oborio",
                "ai",
                "yukinko",
                "amika",
                "maki",
                "kuro",
                "note",
                "takeshi",
                "pura"
            };

            for (var i = 0; i < 15; ++i)
            {
                var bukis = counts.Take(4).ToArray();
                counts = counts.Skip(4).ToArray();
                var job = jobs[i];
                var name = names[i];
                var isFrontEnd = i < 5;
                var deck = isFrontEnd ? GenerateFrontendDeck(bukis) : GenerateBackgroundDeck(bukis);

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

        static Deck GenerateBackgroundDeck(string[] bukis)
        {
            var counts = bukis
                .Select(int.Parse)
                .ToArray();

            var equipment = new Equipment(
                0,
                0,
                0,
                0,
                counts[0],
                counts[0],
                counts[0],
                counts[0]
            );

            var deck = new Deck(equipment);

            return deck;
        }

        static Deck GenerateFrontendDeck(string[] bukis)
        {
            var counts = bukis
                .Select(int.Parse)
                .ToArray();

            var equipment = new Equipment(
                counts[0],
                counts[0],
                counts[0],
                counts[0],
                0,
                0,
                0,
                0
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
