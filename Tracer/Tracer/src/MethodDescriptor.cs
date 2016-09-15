using System;


namespace MPPTracer
{
    public class MethodDescriptor
    {
        public String Name { get; }
        public String ClassName { get; }
        public int ParamsNumber { get; }
        public long TraceTime { get; set; }

        public MethodDescriptor(String name, String className, int paramsNumber)
        {
            Name = name;
            ClassName = className;
            ParamsNumber = paramsNumber;
            TraceTime = -1;
        }

    }
}
