using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.Mishin.Tracer.Types {
    public struct TracerTree {
        public TracerInfo Element;
        public List<TracerTree> Child;
    }
}
