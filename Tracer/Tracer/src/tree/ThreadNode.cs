
namespace MPPTracer.Tree
{
    public class ThreadNode : ChildNode
    {

        public override void StopLastTrace(long endTime)
        {
            MethodNode method = GetLastAddedMethod();
            method.StopLastTrace(endTime);
        }

    }
}
