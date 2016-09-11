using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public long time { get; set; }
        public TraceResult(long time) 
        {
            this.time = time;
        }
    }
}
