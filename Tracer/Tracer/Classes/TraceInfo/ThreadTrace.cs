using System.Collections.Generic;
using System.Linq;

namespace Trace.Classes.TraceInfo
{
    public class ThreadTrace
    {
        private readonly Stack<MethodTrace> _stackOfMethods;
        public List<MethodTrace> AllMethodsInfo { get; }

        public ThreadTrace()
        {
            _stackOfMethods = new Stack<MethodTrace>();
            AllMethodsInfo = new List<MethodTrace>();
        }

        public void StartListenMethod(MethodTrace methodInfo)
        {
            if (_stackOfMethods.Count == 0)
            {
                AllMethodsInfo.Add(methodInfo);
            }
            else
            {
                _stackOfMethods.Peek().AddNestedMethod(methodInfo);
            }
            _stackOfMethods.Push(methodInfo);
        }

        public void StopListenMethod()
        {
            _stackOfMethods.Pop().StopMeteringTime();
        }

        public long ExecutionTime => AllMethodsInfo.Select(x => x.GetExecutionTime()).Sum();
    }
}
