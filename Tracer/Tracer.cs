namespace Tracer
{
    public class Tracer : ITracer
    {
        public TraceResult TraceResult { get; }

        private static Tracer _instance;

        private static readonly object LockObject = new object();
        
        private TraceResult _traceResult;

        public static Tracer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new Tracer();
                        }
                    }
                }
                return _instance;
            }
        }

        protected Tracer()
        {
            _traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            throw new System.NotImplementedException();
        }

        public void StopTrace()
        {
            throw new System.NotImplementedException();
        }
    }
}