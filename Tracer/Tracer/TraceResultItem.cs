using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResultItem
    {
        internal int ThreadId { get; private set; }
        internal int Time { get; set; }
        
        internal List<TraceMethodItem> Methods = new List<TraceMethodItem>();

        public TraceResultItem(int threadId)
        {
            ThreadId = threadId;
        }
    }
}
