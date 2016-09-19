using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodInfo
    {
        public string Name { get; }
        public string ClassName { get; }
        public int ParamsNumber { get; }
        private long Time;

        public long TraceTime
        {
            get
            {
                return Time;
            }
            set
            {
                Time = (Time == -1) ? value : Time;
            }
        }

        public MethodInfo(string name, string className, int paramsNumber,long Time)
        {
            Name = name;
            ClassName = className;
            ParamsNumber = paramsNumber;
            Time = -1;
        }
    }
}
