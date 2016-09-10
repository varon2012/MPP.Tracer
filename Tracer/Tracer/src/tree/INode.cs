namespace Tracer.Tree
{ 
    public interface INode
    {
        void FixateCountStart(long startTime, CallerDescriptor caller);
        void FixateCountEnd(long endTime);
    }
}
