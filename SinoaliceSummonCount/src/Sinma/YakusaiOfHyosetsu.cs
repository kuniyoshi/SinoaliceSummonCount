namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfHyosetsu : Base
    {

        public YakusaiOfHyosetsu(int hidingTurnCount)
            : base(BackendBuki.Wand,
                FrontendBuki.Sword,
                FrontendBuki.Lance,
                GetNameOf<YakusaiOfHyosetsu>(),
                hidingTurnCount)
        {
        }

    }

}
