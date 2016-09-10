using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class Tracer: ITracer
    {

        private ConcurrentStack<TracerNode> _tray = new ConcurrentStack<TracerNode>();

        public void StartTrace()
        {
            
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }
    }

}
