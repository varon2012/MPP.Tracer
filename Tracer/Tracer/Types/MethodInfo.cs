using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace BSUIR.Mishin.Tracer.Types
{
    public class MethodInfo
    {
        public string MethodName { private set; get; }
        public string ClassName { private set; get; }
        public double Time { private set; get; }
        public int CountParams { private set; get; }

        private DateTime _startTime;
        private MethodBase _currentMethod;

        internal void StartMethodTrace(MethodBase method)
        {
            MethodName = method.Name;
            ClassName = method.DeclaringType.FullName;
            CountParams = method.GetParameters().Length;

            _currentMethod = method;
            _startTime = DateTime.UtcNow;
        }

        internal bool isEqual(MethodBase method)
        {
            return ReferenceEquals(_currentMethod, method);
        }

        internal void StopMethodTrace()
        {
            Time = (DateTime.UtcNow - _startTime).TotalMilliseconds;
        }
    }
}
