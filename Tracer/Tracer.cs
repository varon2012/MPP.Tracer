namespace Tracer
{
    public class Tracer : ITracer
    {
        public static Tracer Instance;

        public static Tracer GetInstance()
        {
            if (Instance == null)
            {
                
            }
            return Instance;
        }

        protected Tracer()
        {
            
        }

        public void StartTrace()
        {
            throw new System.NotImplementedException();
        }

        public void StopTrace()
        {
            throw new System.NotImplementedException();
        }

        public TraceResult GetTraceResult()
        {
            throw new System.NotImplementedException();
        }
    }
}