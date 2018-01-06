namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfSenpu : Base
    {

        public YakusaiOfSenpu(int hidingTurnCount)
            : base(BackendBuki.Book,
                FrontendBuki.Hammer,
                FrontendBuki.Bow,
                GetNameOf<YakusaiOfSenpu>(),
                hidingTurnCount)
        {
        }

    }

}
