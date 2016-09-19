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
        public long Time { get; set; }

        public MethodInfo(string name, string className, int paramsNumber,long time)
        {
            Name = name;
            ClassName = className;
            ParamsNumber = paramsNumber;
            Time = time;
        }
    }
}
