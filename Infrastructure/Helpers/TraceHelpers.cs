using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Infrastructure.Models;

namespace Infrastructure.Helpers
{
    public static class TraceHelpers
    {
        public static MethodModel GetMethodModel(StackTrace stackTrace)
        {
            return new MethodModel
            {
                ClassName = stackTrace.GetFrame(1).GetMethod().ReflectedType.Name,
                MethodName = stackTrace.GetFrame(1).GetMethod().Name,
                ParametersCount = stackTrace.GetFrame(1).GetMethod().GetParameters().Length,
                FullName =
                    string.Format("{0}.{1}", stackTrace.GetFrame(1).GetMethod().ReflectedType.Name,
                        stackTrace.GetFrame(1).GetMethod().Name),
                ParameterInfos = stackTrace.GetFrame(1).GetMethod().GetParameters()
            };
        }

       
    }
}