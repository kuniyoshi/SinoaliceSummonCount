namespace SinoaliceSummonCount
{

    public class Buki
    {

        static int _nextTId = 1;

        public readonly int Id;

        readonly BackendBuki? _backend;

        readonly FrontendBuki? _frontend;

        public bool IsFrontend => _frontend.HasValue;

        public bool IsBackend => _backend.HasValue;

        public Buki(FrontendBuki frontendBuki)
        {
            _frontend = frontendBuki;
            Id = _nextTId++;
        }

        public Buki(BackendBuki backendBuki)
        {
            _backend = backendBuki;
            Id = _nextTId++;
        }

        public bool IsSameAs(FrontendBuki buki)
        {
            return _frontend.HasValue && _frontend.Value == buki;
        }
        
        public bool IsSameAs(BackendBuki buki)
        {
            return _backend.HasValue && _backend.Value == buki;
        }

        public override string ToString()
        {
            return IsFrontend
                ? $"{_frontend}"
                : $"{_backend}";
        }

    }

}
