using Tracer.TraceResultData;

namespace Tracer.Format
{
    public interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}
