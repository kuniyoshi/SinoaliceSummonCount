namespace SinoaliceSummonCount
{

    public struct Sinma
    {

        public FrontendBuki FirstFrontendBuki;
        
        public FrontendBuki SecondFrontendBuki;

        public BackendBuki BackendBuki;

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
