using System.Collections.Generic;
using System.Linq;

namespace Trace.Classes.Information
{
    public class ThreadInfo
    {
        private readonly Stack<MethodInfo> _stackOfMethods;
        public List<MethodInfo> AllMethodsInfo { get; }

        public ThreadInfo()
        {
            _stackOfMethods = new Stack<MethodInfo>();
            AllMethodsInfo = new List<MethodInfo>();
        }

        public void StartListenMethod(MethodInfo methodInfo)
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
