using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.Mishin.Tracer.Types {
    public struct TracerThreadTree {
        public int ThreadId;
        public List<TracerTree> Child;
    }
}
