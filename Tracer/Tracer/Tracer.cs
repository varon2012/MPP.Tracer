using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        // singleton
        private static readonly Lazy<Tracer> lazy = 
            new Lazy<Tracer>(() => new Tracer());

        private Tracer()
        {
            TraceResult = new TraceResult();
        }

        public static Tracer GetInstance()
        {
            return lazy.Value;
        }

        private static readonly object lockObject = new object();

        public void StartTrace()
        {
            lock (lockObject)
            {
                TraceResult.AddToTree(new StackTrace().GetFrame(1).GetMethod());
            }
        }

        public void StopTrace()
        {
            lock (lockObject)
            {
                TraceResult.RemoveFromTree();
            }
        }

        public TraceResult TraceResult { get; }
    }
}
