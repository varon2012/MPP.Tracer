using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace TracerAPI
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, Tree> Result = new ConcurrentDictionary<int, Tree>();

        public Tree this[int id]
        {
            set 
            {
                Result[id] = value;
            }
            get
            {
                return Result[id];
            }
        }

        public Tree GetTreeByThreadId(int threadId)
        {
            if (Result.ContainsKey(threadId))
            {
                return this[threadId];
            }
            else
            {
                Tree tree = new Tree();
                Result.TryAdd(threadId,tree);
                return tree;
            }
        }
    }
}
