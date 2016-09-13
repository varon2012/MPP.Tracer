using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class Tracer: ITracer
    {

        private static Tracer _instance;
        private static readonly object Lock = new object();

        public static Tracer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
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
            _traceResult = new TraceResult();
        }

        private TraceResult _traceResult;

        public void ClearTraceResult()
        {
            lock (Lock)
            {
                _traceResult = new TraceResult();
            }
        }

        public void StartTrace()
        {
            MethodBase method = new StackTrace().GetFrame(1).GetMethod();
            lock (Lock)
            {
                _traceResult.StartTraceMethod(Thread.CurrentThread.ManagedThreadId, 
                method.DeclaringType.Name, 
                method.Name, 
                method.GetParameters().Length);
            }
        }

        public void StopTrace()
        {
            lock (Lock)
            { 
                _traceResult.StopTraceMethod(Thread.CurrentThread.ManagedThreadId);
            }
           
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }
    }

}
