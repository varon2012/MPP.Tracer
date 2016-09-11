using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResultItem
    {
        public int ThreadId { get; private set; }
        public int Time { get; set; }


        public List<TraceMethodItem> Methods = new List<TraceMethodItem>();

        public TraceResultItem(int threadId)
        {
            ThreadId = threadId;
        }
    }
}
