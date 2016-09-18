using System;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Tracer instance = new Tracer();

        private Tracer(){}

        public static Tracer Instance => instance;

        public void StartTrace()
        {
            throw new NotImplementedException();
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }
    }
}


