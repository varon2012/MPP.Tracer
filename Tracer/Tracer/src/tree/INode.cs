namespace MPPTracer.Tree
{ 
    public interface INode
    {
        void AddNestedTrace(long startTime, MethodDescriptor descriptor);
        void StopLastTrace(long endTime);
    }
}
