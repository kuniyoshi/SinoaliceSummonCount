using System.Collections.Generic;

namespace SinoaliceSummonCount
{

    public struct Result
    {

        public readonly int PassedTurn;

        public readonly Effect[] Effects;

        public Result(int passedTurn, Effect[] effects)
        {
            PassedTurn = passedTurn;
            Effects = effects;
        }

        public Result Add(Result other)
        {
            var effects = new List<Effect>();
            effects.AddRange(Effects);
            effects.AddRange(other.Effects);
            var newOne = new Result(
                passedTurn: PassedTurn + other.PassedTurn,
                effects: effects.ToArray()
            );
            return newOne;
        }

        public override string ToString()
        {
            return $"{Util.TrimNameSpace(typeof(Result).ToString())}{{RequiredTurn: {PassedTurn}, Effects: {string.Join(", ", Effects)}}}";
        }

    }

}
