using System.Collections.Generic;
using System.Linq;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuild
{
    internal class ThreadInfoResultBuilder
    {
        private readonly ThreadInfo threadInfo;
        internal ThreadInfoResultBuilder(ThreadInfo threadInfo)
        {
            this.threadInfo = threadInfo;
        }

        internal ThreadInfoResult GetResult()
        {
            return CreateThreadInfoResult();
        }

        private ThreadInfoResult CreateThreadInfoResult()
        {
            List<MethodInfoResult> childList = GetChildList();

            ThreadInfoResult thread = new ThreadInfoResult()
            {
                ChildMethods = childList,
                ExecutionTime = threadInfo.ExecutionTime
            };

            return thread;
        }

        private List<MethodInfoResult> GetChildList()
        {
            List<MethodInfoResult> childList = (from methodInfo in threadInfo.MethodsInfo
                select new MethodInfoResultBuilder(methodInfo).GetResult())
                .ToList();
            return childList;
        }
    }
}
