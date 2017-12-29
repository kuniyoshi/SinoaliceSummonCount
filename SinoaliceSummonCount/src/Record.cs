namespace SinoaliceSummonCount
{

    public struct Record
    {

        public readonly string Actor;

        public readonly int Id;

        public readonly bool IsStrong;

        public readonly bool? DidSinmaPrefer;

        public readonly bool? DidBlessed;

        public Record(int id,
                      string actor,
                      bool? didSinmaPrefer,
                      bool? didBlessed,
                      bool isStrong)
        {
            Id = id;
            Actor = actor;
            DidSinmaPrefer = didSinmaPrefer;
            DidBlessed = didBlessed;
            IsStrong = isStrong;
        }

    }

}
