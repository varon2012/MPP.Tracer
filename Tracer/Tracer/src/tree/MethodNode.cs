using MPPTracer.Format;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MPPTracer.Tree
{
    public class MethodNode : ChildNode
    {
        public long StartTime { private get; set; }
        public MethodDescriptor Descriptor { get;}
    

        public long EndTime
        {
            set
            {
                    Descriptor.TraceTime = value - StartTime;
                    
            }
        }

        public Boolean TracingFinished
        {
            get
            {
                return (Descriptor.TraceTime > 0);
            }
        }

        public MethodNode(MethodDescriptor descriptor, ChildNode parent) : base(parent)
        {
            Descriptor = descriptor;
        }

        public override void StopLastTrace(long endTime)
        {
            if (NoNestedMethods() || NestedTracingsFinished())
            {
                EndTime = endTime;
            }
            else
            {
                MethodNode method = GetLastAddedMethod();
                method.StopLastTrace(endTime);
            }
        }
    }
}
