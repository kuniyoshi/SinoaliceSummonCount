namespace SinoaliceSummonCount
{

    public class Pair<TFirst, TSecond>
    {

        public TFirst First;

        public TSecond Second;

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"{typeof(Pair<TFirst, TSecond>)}{{First: {First}, Second: {Second}}}";
        }

    }

}
