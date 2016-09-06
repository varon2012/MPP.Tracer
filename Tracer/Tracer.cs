using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Tracer
{
    public sealed class Tracer : ITracer
    {
        private static volatile Tracer _instance;
        private static readonly object SyncRoot = new object();

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

        private Tracer()
        {
            TraceResult = new TraceResult();
        }

        public void StartTrace()
        {
            var stackTrace = new StackTrace(1);
            var stackFrame = stackTrace.GetFrame(0);
            var method = stackFrame.GetMethod();

            lock (SyncRoot)
            {
                TraceResult.StartNode(Thread.CurrentThread.ManagedThreadId,
                    method.DeclaringType.ToString(), method.Name,
                    FormParameterList(method.GetParameters()));
            }
        }

        private static List<ParameterInfo> FormParameterList(System.Reflection.ParameterInfo[] parameters)
        {
            return parameters.Select(parameter => new ParameterInfo(parameter.Name, parameter.ParameterType)).ToList();
        }

        public void StopTrace()
        {
            lock (SyncRoot)
            {
                TraceResult.FinishNode(Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
