using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class MethodInfo
    {
        public string MethodName { get; set; }

        public string ClassName { get; set;}

        public string ParametersNumber { get; set; }

        public MethodInfo(string methodName, string className, string parametersNumber)
        {
            MethodName = methodName;
            ClassName = className;
            ParametersNumber = parametersNumber;
        }
    }
}
