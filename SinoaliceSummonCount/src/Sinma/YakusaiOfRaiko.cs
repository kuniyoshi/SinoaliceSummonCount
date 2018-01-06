namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfRaiko : Base
    {

        public YakusaiOfRaiko(int hidingTurnCount)
            : base(BackendBuki.MusicalInstrument,
                FrontendBuki.Sword,
                FrontendBuki.Lance,
                GetNameOf<YakusaiOfRaiko>(),
                hidingTurnCount)
        {
        }

    }

}
