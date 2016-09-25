using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Concurrent;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Lazy<Tracer> _lazy = new Lazy<Tracer>(() => new Tracer());
        public static Tracer Instance => _lazy.Value;

        private TracerThreadsInfo _threadStack;

        private Tracer()
        {
            _threadStack = new TracerThreadsInfo();
        }

        public void StartTrace()
        {
            MethodBase currentMethod = GetMethodInfo();
            int threadId = Thread.CurrentThread.ManagedThreadId;

            _threadStack.StartTrace(currentMethod, threadId);
        }

        public void StopTrace()
        {
            MethodBase currentMethod = GetMethodInfo();
            int threadId = Thread.CurrentThread.ManagedThreadId;

            _threadStack.StopTrace(currentMethod, threadId);
        }

        private MethodBase GetMethodInfo()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame frame;

            for(int i = 0; i < stackTrace.FrameCount; i++)
            {
                frame = stackTrace.GetFrame(i);
                MethodBase method = frame.GetMethod();
                Type type = method.DeclaringType;

                if(type != typeof(Tracer))
                {
                    return method;
                }
            }

            return null;
        }

        public Dictionary<int, List<MethodsTree>> GetTraceResult() {
            return _threadStack.GetThreadsStack();
        }

        public void WaitStop() {
            _threadStack.WaitStopAllThreads();
        }
    }
}
