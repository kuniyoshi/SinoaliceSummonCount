using System.Collections.Generic;
using System.Linq;

namespace SinoaliceSummonCount
{

    public class Guild
    {

        public readonly Member[] Members;

        int? _remainTurnToCompletePrepare;

        readonly List<Record> _records;

        public IReadOnlyList<Record> Records => _records;

        public Guild(Member[] members)
        {
            Members = members;
            _records = new List<Record>();
            _remainTurnToCompletePrepare = Constant.TurnToCompletePrepare;
        }

        public List<Record> Act(int id, Sinma.Base sinma)
        {
            if (sinma != null && sinma.SinmaState == SinmaState.NoSign)
            {
                _remainTurnToCompletePrepare = Constant.TurnToCompletePrepare;
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

                if (buki == null)
                {
                    continue;
                }
                
                var record = new Record(
                    id: id,
                    actor: member.Name,
                    sinmaState: sinma?.SinmaState ?? SinmaState.NoSign,
                    isStrong: Environment.DoesJobStrongWith(member.Job, buki),
                    didSinmaPrefer: sinma?.DoesPrefer(buki) ?? false
                );
                
                sinma?.Watch(buki);
                
                records.Add(record);
            }

            _records.AddRange(records);

            return records;
        }

        public override string ToString()
        {
            var members = Members
                .Select(m => m.ToString());
            return $"{Util.TrimNameSpace<Guild>()}{{Members: {string.Join(", ", members)}}}";
        }

    }

}
