namespace SinoaliceSummonCount.Sinma
{

    public class YakusaiOfRyusei : Base
    {

        public YakusaiOfRyusei(int hidingTurnCount)
            : base(BackendBuki.MusicalInstrument,
                FrontendBuki.Hammer,
                FrontendBuki.Bow,
                GetNameOf<YakusaiOfRyusei>(),
                hidingTurnCount)
        {
        }

    }

}
