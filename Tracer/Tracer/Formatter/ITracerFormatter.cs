using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using BSUIR.Mishin.Tracer;
using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Formatter {
    interface ITracerFormatter {
        void Parse(Dictionary<int, List<MethodsTree>> threadsList);
    }
}
