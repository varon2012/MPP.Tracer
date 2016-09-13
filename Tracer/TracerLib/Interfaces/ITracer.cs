using TracerLib.Utils;

namespace TracerLib.Interfaces
{
	public interface ITracer
	{
		void StartTrace();

		void StopTrace();

		TraceResult GetTraceResult();
	}
}