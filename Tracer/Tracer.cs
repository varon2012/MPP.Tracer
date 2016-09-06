using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Tracer
{
    public sealed class Tracer : ITracer
    {
        private Tracer()
        {
            TraceResult = new TraceResult();
        }

        private static List<ParameterInfo> FormParameterList(System.Reflection.ParameterInfo[] parameters)
        {
            return parameters.Select(parameter => new ParameterInfo(parameter.Name, parameter.ParameterType)).ToList();
        }

        public void StartTrace()
        {
            lock (SyncRoot)
            {
                var stackTrace = new StackTrace(1);
                var stackFrame = stackTrace.GetFrame(0);
                var method = stackFrame.GetMethod();

                TraceResult.StartNode(Thread.CurrentThread.ManagedThreadId,
                    method.DeclaringType.ToString(), method.Name,
                    FormParameterList(method.GetParameters()));
            }
        }

        public void StopTrace()
        {
            lock (SyncRoot)
            {
                TraceResult.FinishNode(Thread.CurrentThread.ManagedThreadId);
            }
        }

        public TraceResult TraceResult { get; }

        public static Tracer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
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

        private static volatile Tracer _instance;
        private static readonly object SyncRoot = new object();
    }
}
