using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance;
        private static readonly object SyncRoot = new object();
        public TraceResult TraceResult { get;} = new TraceResult();
        private TraceResultBuilder resultBuilder = new TraceResultBuilder();

        public static Tracer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                            instance.resultBuilder.TraceResult = instance.TraceResult;
                        }
                    }
                }
                return instance;
            }
        }

        
        public void StartTrace()
        {
            var threadItem = TraceResult.GetOrAdd(Thread.CurrentThread.ManagedThreadId,
                new TraceResultItem(Thread.CurrentThread.ManagedThreadId));
            lock (SyncRoot)
            {
                instance.resultBuilder.StartTrace(threadItem);
            }
        }

        public void StopTrace()
        {
            var threadItem = TraceResult[Thread.CurrentThread.ManagedThreadId];
            lock (SyncRoot)
            {
                instance.resultBuilder.StopTrace(threadItem);
            }
        }

    }
}
