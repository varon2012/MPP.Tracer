using System;

namespace Tracer.Tree
{
    public class MethodNode : ChildNode
    {
        public long StartTime { private get; set; }
        public long TraceTime { get; private set; }
        public CallerDescriptor Caller { private get;  set; }

        public long EndTime
        {
            set
            {
                    TraceTime = value - StartTime;
                    
            }
        }

        public Boolean CountFinished
        {
            get
            {
                return (TraceTime > 0);
            }
        }

        public MethodNode(CallerDescriptor caller)
        {
            Caller = caller;
        }

        public override void FixateCountEnd(long endTime)
        {
            if (NoNestedMethods() || AllCountsFinished())
            {
                EndTime = endTime;
            }
            else
            {
                MethodNode method = GetLastAddedMethod();
                method.FixateCountEnd(endTime);
            }
        }
    }
}
