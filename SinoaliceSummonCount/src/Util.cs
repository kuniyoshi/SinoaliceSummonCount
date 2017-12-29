using System;
using System.Collections.Generic;

namespace SinoaliceSummonCount
{

    public static class Util
    {

        public static void Shuffle<T>(List<T> list)
        {
            var gen = new Random();
            
            for (var i = list.Count - 1; i > 0; --i)
            {
                var index = gen.Next(i);
                var swapper = list[i];
                list[i] = list[index];
                list[index] = swapper;
            }
        }

    }

}
