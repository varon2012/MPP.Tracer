
namespace Tracer.Tree
{
    public class ThreadNode : ChildNode
    {

        public int Id { get; private set;}

        internal ThreadNode(int id)
        {
            Id = id;
        }

        public override void FixateCountEnd(long endTime)
        {
            MethodNode method = GetLastAddedMethod();
            method.FixateCountEnd(endTime);
        }

    }
}
