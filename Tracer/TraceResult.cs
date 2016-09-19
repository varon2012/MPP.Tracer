using System.Collections.Generic;

namespace Tracer
{
    public class TraceResult
    {
        private static readonly object LockMethodAdd = new object();

        private static readonly object LockmethodDelete = new object();

        public Dictionary<int, List<MethodsTreeNode>> TraceTree { get; set; }

        public TraceResult() { }

        public void AddMethodToTree()
        {
            lock(LockMethodAdd)
            {

            }
        }
    }
}