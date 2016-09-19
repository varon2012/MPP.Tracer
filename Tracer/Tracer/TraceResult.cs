using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult : Dictionary<int, TraceResultItem>
    {
        public TraceResultItem FirstOrCreate(int threadId)
        {
            if (ContainsKey(threadId))
                return this[threadId];
            var traceResult = new TraceResultItem(threadId);
            Add(threadId, new TraceResultItem(threadId));
            return traceResult;
        }
    }
}
