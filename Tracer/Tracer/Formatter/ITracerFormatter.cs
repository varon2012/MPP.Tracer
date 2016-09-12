using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using BSUIR.Mishin.Tracer;

namespace BSUIR.Mishin.Tracer.Formatter {
    interface ITracerFormatter {
        string Parse();
        Stream ParseToStream();
    }
}
