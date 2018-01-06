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

        public List<Record> Act(int id, Sinma.Base sinma)
        {
            if (sinma != null && sinma.SinmaState == SinmaState.NoSign)
            {
                _remainTurnToCompletePrepare = TurnToCompletePrepare;
            }

            if (sinma != null && sinma.SinmaState == SinmaState.Signed)
            {
                _remainTurnToCompletePrepare--;
            }

            var sinmaCountDown = sinma != null && sinma.SinmaState == SinmaState.Signed
                ? _remainTurnToCompletePrepare
                : null;

            var records = new List<Record>();
            
            foreach (var member in Members)
            {
                var buki = member.Act(sinmaCountDown, sinma);
                var record = new Record(
                    id: id,
                    actor: member.Name,
                    sinmaState: sinma?.SinmaState ?? SinmaState.NoSign,
                    isStrong: Environment.DoesJobStrongWith(member.Job, buki),
                    didSinmaPrefer: sinma?.DoesPrefer(buki) ?? false
                );
                records.Add(record);
                
                sinma?.Watch(buki);
            }

            _records.AddRange(records);

            return records;
        }

        public override string ToString()
        {
            var members = Members
                .Select(m => m.ToString());
            return $"{Util.TrimNameSpace(typeof(Guild).ToString())}{{Members: {string.Join(", ", members)}}}";
        }

    }

}
