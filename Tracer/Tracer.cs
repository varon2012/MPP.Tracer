using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult fTraceResult;

        private static volatile Tracer _tracerInstance;
        private static readonly object SyncRoot = new object();

        public void StartTrace()
        {
            throw new NotImplementedException();
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }

        public TraceResult GeTraceResult()
        {
            throw new NotImplementedException();
        }

        // Static

        public static Tracer GetInstance()
        {
            if (_tracerInstance == null)
            {
                lock (SyncRoot)
                {
                    if (_tracerInstance == null)
                    {
                        _tracerInstance = new Tracer();
                    }
                }
            }

            return _tracerInstance;
        }

        // Internal

        private Tracer()
        {
            fTraceResult = new TraceResult();
        }
    }
}
