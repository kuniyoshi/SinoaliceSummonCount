using System.Collections.Generic;

namespace SinoaliceSummonCount
{

    public struct Result
    {

        public readonly int? FirstRequiredTurnCount;
        
        public readonly int? SecondRequiredTurnCount;

        public readonly IReadOnlyList<Record> Records;

        public Result(int? firstRequiredTurnCount, int? secondRequiredTurnCount, IReadOnlyList<Record> records)
        {
            FirstRequiredTurnCount = firstRequiredTurnCount;
            SecondRequiredTurnCount = secondRequiredTurnCount;
            Records = records;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace<Result>()}{{FirstRequiredTurn: {FirstRequiredTurnCount}, SecondRequiredTurn: {SecondRequiredTurnCount}, Records: {string.Join(", ", Records)}}}";
        }

    }

}
