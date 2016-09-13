using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer {
    interface ITracer {
        void Start();
        List<TracerThreadTree> Stop();

        void StartTrace();
        void StopTrace();
    }
}
