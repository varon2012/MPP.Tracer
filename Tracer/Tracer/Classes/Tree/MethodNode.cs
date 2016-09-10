using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.Tree
{
    internal class MethodNode : AbstractChildNode
    {
        private long startTime = -1;
        private long traceTime = -1;
        private CallerDescriptor caller;

        internal long StartTime
        {
            set
            {
                if (value > 0)
                    startTime = value;       
            }
        }
        internal long EndTime
        {
            set
            {
                if (value > 0)
                    traceTime = value - startTime;
                    
            }
        }
        internal long TraceTime
        {
            get
            {
                return traceTime;
            }
        }
        internal Boolean CountFinished
        {
            get
            {
                return (traceTime > 0);
            }
        }

        internal MethodNode(CallerDescriptor caller)
        {
            this.caller = caller;
        }
    }
}
