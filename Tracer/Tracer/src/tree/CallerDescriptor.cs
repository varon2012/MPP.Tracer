using System;


namespace Tracer.Tree
{
    public class CallerDescriptor
    {
        private String name;
        private String className;
        private int paramsNumber;

        internal CallerDescriptor(String name, String className, int paramsNumber)
        {
            this.name = name;
            this.className = className;
            this.paramsNumber = paramsNumber;
        }

        internal String Name
        {
            get
            {
                return name;
            }
        }

        internal String ClassName
        {
            get
            {
                return className;
            }
        }

        internal int ParamsNumber
        {
            get
            {
                return paramsNumber;
            }
        }
    }
}
