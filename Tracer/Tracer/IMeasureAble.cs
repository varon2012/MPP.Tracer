using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    interface IMeasureable
    {
        DateTime StartTime { get; }
        //Stopwatch StartTime { get; }
        void Measure();
    }
}
