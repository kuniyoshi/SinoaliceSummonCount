namespace SinoaliceSummonCount
{

    public struct Result
    {

        public readonly int RequiredTurn;

        public readonly Effect[] Effects;

        public Result(int requiredTurn, Effect[] effects)
        {
            RequiredTurn = requiredTurn;
            Effects = effects;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace(typeof(Result).ToString())}{{RequiredTurn: {RequiredTurn}, Effects: {string.Join(", ", Effects)}}}";
        }

    }

}
