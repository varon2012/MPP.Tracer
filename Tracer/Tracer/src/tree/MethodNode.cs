namespace MPPTracer.Tree
{
    public class MethodNode : InternalNode
    {
        public readonly long startTime;
        public MethodDescriptor Descriptor { get;}

        public bool TracingFinished
        {
            get
            {
                return (Descriptor.TraceTime > 0);
            }
        }

        public MethodNode(long startTime, MethodDescriptor descriptor)
        {
            Descriptor = descriptor;
            this.startTime = startTime;
        }

        public override void StopLastTrace(long endTime)
        {
            if (NoNestedMethods() || NestedTracingsFinished())
            {
                Descriptor.TraceTime = endTime - startTime;
            }
            else
            {
                MethodNode method = GetLastAddedMethod();
                method.StopLastTrace(endTime);
            }
        }
    }
}
