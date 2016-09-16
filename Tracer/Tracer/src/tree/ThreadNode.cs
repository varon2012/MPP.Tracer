
namespace MPPTracer.Tree
{
    public class ThreadNode : InternalNode
    {
        public ThreadNode() : base(null)
        {

        }
        public override void StopLastTrace(long endTime)
        {
            MethodNode method = GetLastAddedMethod();
            method.StopLastTrace(endTime);
        }

    }
}
