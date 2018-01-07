using System;

namespace SinoaliceSummonCount.Sinma
{

    public class Base
    {

        class TurnCount
        {

            public int NoSign;

            public int Signed;

            public int Summoning;

            public int Blessing;

        }

        protected static string GetNameOf<T>()
        {
            return Util.TrimNameSpace(typeof(T).ToString());
        }

        public readonly BackendBuki PreferredBackendBuki;

        public readonly FrontendBuki FirstPreferredFrontendBuki;

        public readonly FrontendBuki SecondPreferredFrontendBuki;

        public readonly string Name;

        public readonly int HidingTurnCount;

        bool _didGone;

        int _preferredTimes;

        SinmaState _sinmaState = SinmaState.NoSign;

        readonly TurnCount _turnCount;

        public bool DidGone => _didGone;

        public SinmaState SinmaState => _sinmaState;

        public int TurnCountToSummon => _turnCount.Summoning;

        protected Base(BackendBuki preferredBackendBuki,
                       FrontendBuki firstPreferredFrontendBuki,
                       FrontendBuki secondPreferredFrontendBuki,
                       string name,
                       int hidingTurnCount)
        {
            PreferredBackendBuki = preferredBackendBuki;
            FirstPreferredFrontendBuki = firstPreferredFrontendBuki;
            SecondPreferredFrontendBuki = secondPreferredFrontendBuki;
            Name = name;
            HidingTurnCount = hidingTurnCount;
            _turnCount = new TurnCount();
        }

        public bool DoesPrefer(Buki buki)
        {
            if (buki == null)
            {
                return false;
            }
            
            return buki.IsSameAs(PreferredBackendBuki)
                   || buki.IsSameAs(FirstPreferredFrontendBuki)
                   || buki.IsSameAs(SecondPreferredFrontendBuki);
        }

        public void PassTurn()
        {
            if (_didGone)
            {
                return;
            }

            if (SinmaState == SinmaState.NoSign)
            {
                _turnCount.NoSign++;

                if (_turnCount.NoSign == HidingTurnCount)
                {
                    _sinmaState = SinmaState.Signed;
                }
            }

            if (SinmaState == SinmaState.Signed)
            {
                _turnCount.Signed++;

                if (_turnCount.Signed == Constant.TurnDuringSigning)
                {
                    _sinmaState = SinmaState.Summoning;
                }
            }

            if (SinmaState == SinmaState.Summoning)
            {
                _turnCount.Summoning++;
            }

            if (SinmaState == SinmaState.Blessed)
            {
                _turnCount.Blessing++;
                
                if (_turnCount.Blessing == Constant.TurnDuringBlessed)
                {
                    _sinmaState = SinmaState.NoSign;
                    _didGone = true;
                }
            }
        }

        public void Watch(Buki buki)
        {
            if (buki == null)
            {
                return;
            }
            
            switch (SinmaState)
            {
                case SinmaState.NoSign:
                    WatchNosign(buki);
                    break;
                case SinmaState.Signed:
                    WatchSigned(buki);
                    break;
                case SinmaState.Summoning:
                    WatchSummoning(buki);
                    break;
                case SinmaState.Blessed:
                    WatchBlessed(buki);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void WatchBlessed(Buki buki)
        {
            // do nothing
        }

        void WatchNosign(Buki buki)
        {
            // do nothing
        }

        void WatchSigned(Buki buki)
        {
            // do nothing
        }

        void WatchSummoning(Buki buki)
        {
            _preferredTimes = _preferredTimes
                              + Convert.ToInt32(DoesPrefer(buki));

            if (_preferredTimes == Constant.CountToSummon)
            {
                _sinmaState = SinmaState.Blessed;
            }
        }


    }

}
