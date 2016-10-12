using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodInfo
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public Stopwatch Watcher { get; set; }
        public int NumberParametres { get; set; }
    }
}
