using Tracer.Models;

namespace Tracer.Interfaces
{
    public interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}