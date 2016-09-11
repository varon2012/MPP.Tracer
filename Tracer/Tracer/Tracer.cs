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
        public TraceResult TraceResult { get; }

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
                            TraceResultBuilder.TraceResult = instance.TraceResult;
                        }
                    }
                }
                return instance;
            }
        }
        public Tracer()
        {
            TraceResult = new TraceResult();
            TraceResultBuilder.TraceResult = TraceResult;
        }

        
        public void StartTrace()
        {
            lock (SyncRoot)
            {
                var threadItem = TraceResult.FirstOrCreate(Thread.CurrentThread.ManagedThreadId);
                TraceResultBuilder.StartTrace(threadItem);
            }
        }

        public void StopTrace()
        {
            lock (SyncRoot)
            {
                var threadItem = TraceResult.Find(x => x.ThreadId == Thread.CurrentThread.ManagedThreadId);
                TraceResultBuilder.StopTrace(threadItem);
            }
            
        }

    }
}
