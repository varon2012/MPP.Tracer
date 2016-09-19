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
        public TraceResult TraceResult { get; private set; }
        private TraceResultBuilder ResultBuilder { get; set; }

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
                            instance.ResultBuilder.TraceResult = instance.TraceResult;
                        }
                    }
                }
                return instance;
            }
        }

        
        public void StartTrace()
        {
            var threadItem = TraceResult.FirstOrCreate(Thread.CurrentThread.ManagedThreadId);
            instance.ResultBuilder.StartTrace(threadItem);
        }

        public void StopTrace()
        {
            var threadItem = TraceResult[Thread.CurrentThread.ManagedThreadId];
            instance.ResultBuilder.StopTrace(threadItem);
        }

    }
}
