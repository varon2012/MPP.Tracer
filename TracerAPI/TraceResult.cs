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
        private ConcurrentDictionary<int, Tree> result = new ConcurrentDictionary<int, Tree>();
        public ConcurrentDictionary<int, Tree> Result {get { return result; }}

        public Tree this[int id]
        {
            set { result[id] = value; }
            get { return result[id];  }
        }

        public Tree GetTreeByThreadId(int threadId)
        {
            if (Result.ContainsKey(threadId))
                return this[threadId];            
            else
            {
                Tree tree = new Tree();
                Result.TryAdd(threadId,tree);
                return tree;
            }
        }
    }
}
