using System.Collections.Generic;
using System.Linq;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuild
{
    internal class MethodInfoResultBuilder
    {
        private readonly MethodInfo methodInfo;
        internal MethodInfoResultBuilder(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        internal MethodInfoResult GetResult()
        {
            return CreateMethodInfoResult();
        }

        private MethodInfoResult CreateMethodInfoResult()
        {
            List<MethodInfoResult> childList = GetChildList();

            MethodInfoResult method = new MethodInfoResult
            {
                Name = methodInfo.Name,
                ChildMethods = childList,
                ClassName = methodInfo.ClassName,
                ExecutionTime = methodInfo.ExecutionTime,
                ParamsCount = methodInfo.ParamsCount
            };

            return method;
        }

        private List<MethodInfoResult> GetChildList()
        {
            List<MethodInfoResult> childList = (from method in methodInfo.MethodsInfo
                                                select new MethodInfoResultBuilder(method).GetResult())
                                                        .ToList();
            return childList;
        }
    }
}
