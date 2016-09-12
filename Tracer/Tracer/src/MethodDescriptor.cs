using System;


namespace MPPTracer
{
    public class MethodDescriptor
    {
        public String Name { get; private set; }
        public String ClassName { get; private set; }
        public int ParamsNumber { get; private set; }
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
