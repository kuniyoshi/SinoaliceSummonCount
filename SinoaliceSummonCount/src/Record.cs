namespace SinoaliceSummonCount
{

    public struct Record
    {

        public readonly int Id;

        public readonly string Actor;

        public readonly SinmaState SinmaState;

        public readonly bool IsStrong;

        public readonly bool DidSinmaPrefer;

        public Record(int id,
                      string actor,
                      SinmaState sinmaState,
                      bool isStrong,
                      bool didSinmaPrefer)
        {
            Id = id;
            Actor = actor;
            SinmaState = sinmaState;
            IsStrong = isStrong;
            DidSinmaPrefer = didSinmaPrefer;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace(typeof(Record).ToString())}{{Id: {Id}, Actor: {Actor}, SinmaState: {SinmaState}, IsStrong: {IsStrong}, DidSinmaPrefer: {DidSinmaPrefer}}}";
        }

    }

}
