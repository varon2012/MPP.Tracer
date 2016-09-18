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
        BlockingCollection<Tree> Collection = new BlockingCollection<Tree>();

    }
}
