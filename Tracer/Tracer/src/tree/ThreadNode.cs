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

    }
}
