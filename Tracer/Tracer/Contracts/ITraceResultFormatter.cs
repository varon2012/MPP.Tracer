using Tracer.Model.ViewModels;

namespace Tracer.Contracts
{
    public interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}
