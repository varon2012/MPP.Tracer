﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    interface IFormatter
    {
        void Format(TraceResult traceResult);
    }
}
