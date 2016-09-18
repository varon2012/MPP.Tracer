namespace MPPTracer.Tree
{ 
    internal interface INode
    {
        void AddNestedTrace(long startTime, MethodDescriptor descriptor);
        void StopLastTrace(long endTime);
    }
}
