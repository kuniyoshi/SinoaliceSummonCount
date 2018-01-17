using System;
using System.Linq;

namespace SinoaliceSummonCount
{

    public static class SinmaBuilder
    {

        public static Pair<Sinma.Base, Sinma.Base> Build(string text)
        {
            var ids = text.Split('\t')
                .Select(int.Parse)
                .ToArray();

            var first = GetSinma(ids[0], 54);
            var second = GetSinma(ids[1], 53);
            var pair = new Pair<Sinma.Base, Sinma.Base>(first, second);

            return pair;
        }

        static Sinma.Base GetSinma(int id, int turnCountToSummon)
        {
            switch (id)
            {
                case 0:
                    return new Sinma.YakusaiOfHyosetsu(turnCountToSummon);
                case 1:
                    return new Sinma.YakusaiOfNichirin(turnCountToSummon);
                case 2:
                    return new Sinma.YakusaiOfRaiko(turnCountToSummon);
                case 3:
                    return new Sinma.YakusaiOfRyusei(turnCountToSummon);
                case 4:
                    return new Sinma.YakusaiOfSenpu(turnCountToSummon);
                case 5:
                    return new Sinma.YakusaiOfSinpa(turnCountToSummon);
                default:
                    throw new Exception($"Unknown sinma Id: {id}");
            }
        }

    }

}
