using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly object syncRoot = new object();

        private Tracer()
        {

        }

        void StartTrace()
        {
            lock (syncRoot)
            {

            }
        }

        void StopTrace()
        {
            lock (syncRoot)
            {

            }
        }

        TraceResult GetTraceResult()
        {
            return null;
        }

        public static Tracer Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Tracer();
                    }
                }                    
            }               
            return instance;
        }
    }
}
