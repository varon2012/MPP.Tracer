namespace MPPTracer
{
    public class MethodDescriptor
    {
        public string Name { get; }
        public string ClassName { get; }
        public int ParamsNumber { get; }
        private long traceTime;
        public long TraceTime
        {
            get
            {
                return traceTime;
            }
            set
            {
                traceTime = (traceTime == -1) ? value : traceTime;
            }
        }

        public MethodDescriptor(string name, string className, int paramsNumber)
        {
            Name = name;
            ClassName = className;
            ParamsNumber = paramsNumber;
            traceTime = -1;
        }

    }
}
