using System.Diagnostics;

namespace Tracer
{
    public sealed class Tracer : ITracer
    {
        public TraceResult TraceResult { get; }

        private static Tracer _instance;

        private static readonly object LockObject = new object();

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

        private Tracer()
        {
            TraceResult = new TraceResult();
        }

        public void StartTrace()
        {
            lock (LockObject) 
            {
                TraceResult.AddMethodToTree(new StackTrace().GetFrame(1).GetMethod());
            }
        }

        public void StopTrace()
        {
            lock (LockObject)
            {
                TraceResult.OutOfMethodOnTree();
            }
        }
    }
}