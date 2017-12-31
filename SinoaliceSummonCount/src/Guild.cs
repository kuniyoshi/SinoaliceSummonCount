using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public class Guild
    {

        const int TurnToCompletePrepare = 3 + 1; // first move takes one turn

        public readonly Member[] Members;

        int? _remainTurnToCompletePrepare;

        readonly List<Record> _records;

        public IReadOnlyList<Record> Records => _records;

        public Guild(Member[] members)
        {
            Members = members;
            _records = new List<Record>();
            _remainTurnToCompletePrepare = TurnToCompletePrepare;
        }

        public List<Record> Act(int id, SinmaState sinmaState, Sinma sinma)
        {
            if (sinmaState == SinmaState.NoSign)
            {
                _remainTurnToCompletePrepare = TurnToCompletePrepare;
            }

            if (sinmaState == SinmaState.Signed)
            {
                _remainTurnToCompletePrepare--;
            }

            var sinmaCountDown = sinmaState == SinmaState.Signed
                ? _remainTurnToCompletePrepare
                : null;

            var records = (
                from member in Members
                let buki = member.Act(sinmaState, sinmaCountDown, sinma)
                select new Record(
                    id: id,
                    actor: member.Name,
                    didSinmaPrefer: sinma.DoesPrefer(buki),
                    didBlessed: sinmaState == SinmaState.Blessed,
                    isStrong: Environment.DoesJobStrongWith(member.Job, buki))
            ).ToList();

            _records.AddRange(records);

            return records;
        }

        public override string ToString()
        {
            var members = Members
                .Select(m => m.ToString());
            return $"{typeof(Guild)}{{Members: {string.Join(", ", members)}}}";
        }

    }

}
