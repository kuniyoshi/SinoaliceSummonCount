namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfNichirin : Base
    {

        public YakusaiOfNichirin(int hidingTurnCount)
            : base(BackendBuki.Book,
                FrontendBuki.Sword,
                FrontendBuki.Lance,
                GetNameOf<YakusaiOfNichirin>(),
                hidingTurnCount)
        {
        }

    }

}
