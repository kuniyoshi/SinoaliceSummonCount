namespace SinoaliceSummonCount
{

    public struct Sinma
    {

        public readonly FrontendBuki FirstFrontendBuki;

        public readonly FrontendBuki SecondFrontendBuki;

        public readonly BackendBuki BackendBuki;

        public readonly int CounttoSummon;

        public Sinma(BackendBuki backendBuki,
                     FrontendBuki firstFrontendBuki,
                     FrontendBuki secondFrontendBuki, 
                     int counttoSummon)
        {
            BackendBuki = backendBuki; 
            FirstFrontendBuki = firstFrontendBuki;
            SecondFrontendBuki = secondFrontendBuki;
            CounttoSummon = counttoSummon;
        }

        public bool DoesPrefer(Buki buki)
        {
            if (buki.IsFrontend)
            {
                return buki.IsSameAs(FirstFrontendBuki)
                       || buki.IsSameAs(SecondFrontendBuki);
            }

            if (buki.IsBackend)
            {
                return buki.IsSameAs(BackendBuki);
            }

            return false;
        }

    }

}
