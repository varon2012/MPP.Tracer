using System;


namespace MPPTracer
{
    public class MethodDescriptor
    {
        public String Name { get; }
        public String ClassName { get; }
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
                traceTime = (traceTime == -1) ? value : this.traceTime;
            }
        }

        public MethodDescriptor(String name, String className, int paramsNumber)
        {
            Name = name;
            ClassName = className;
            ParamsNumber = paramsNumber;
            traceTime = -1;
        }

    }
}
