using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        public TraceResult TraceResult { get; }

        private static Tracer _instance;

        private static readonly object LockObject = new object();

        private static readonly object StartTraceObject = new object();

        private static readonly object StopTraceObject = new object();

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
            TraceResult = new TraceResult();
        }

        public void StartTrace()
        {
            lock (StartTraceObject) 
            {
                TraceResult.AddMethodToTree(new StackTrace().GetFrame(1).GetMethod());
            }
        }

        public void StopTrace()
        {
            lock (StopTraceObject)
            {
                TraceResult.OutOfMethodOnTree();
            }
        }
    }
}