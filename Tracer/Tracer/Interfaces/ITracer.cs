using Trace.Classes;

namespace Trace.Interfaces
{
    internal interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}
