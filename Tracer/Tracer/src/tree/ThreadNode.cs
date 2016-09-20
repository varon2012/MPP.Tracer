using System.Collections.Generic;

namespace MPPTracer.Tree
{
    public class ThreadNode : InternalNode
    {
        public readonly int ID;
        public ThreadNode(int id)
        {
            ID = id;
        }
        public override void StopLastTrace(long endTime)
        {
            MethodNode method = GetLastAddedMethod();
            method.StopLastTrace(endTime);
        }
        public long GetTraceTime()
        {
            IEnumerator<MethodNode> enumerator = GetEnumerator();
            long traceTime = 0;
            while (enumerator.MoveNext())
            {
                MethodDescriptor descriptor = enumerator.Current.Descriptor;
                traceTime += descriptor.TraceTime; 
            }

            return traceTime;

        }

    }
}
