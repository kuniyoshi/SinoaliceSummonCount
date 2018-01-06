namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfSinpa : Base
    {

        public YakusaiOfSinpa(int hidingTurnCount)
            : base(BackendBuki.Wand,
                FrontendBuki.Hammer,
                FrontendBuki.Bow,
                GetNameOf<YakusaiOfSinpa>(),
                hidingTurnCount)
        {
        }

    }

}
