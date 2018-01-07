using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public struct Slot
    {

        public readonly Buki[] Bukis;

        public Slot(IEnumerable<Buki> bukis)
        {
            Bukis = bukis.ToArray();
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace<Slot>()}{{Bukis: {string.Join(", ", Bukis.ToList())}}}";
        }

    }

}
