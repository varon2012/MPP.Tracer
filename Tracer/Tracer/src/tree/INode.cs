namespace Tracer.Tree
{ 
    public interface INode
    {
        void AddNestedTrace(long startTime, CallerDescriptor caller);
        void StopLastTrace(long endTime);
    }
}
