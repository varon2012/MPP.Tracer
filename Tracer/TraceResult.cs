using System.Collections;
using System.Reflection;

namespace Tracer
{
    public class TraceResult
    {
        private readonly ThreadInfoList threadInfoList;

        internal TraceResult()
        {
            threadInfoList = new ThreadInfoList();
        }

        internal void StartMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            threadInfoList.StartMethodTraceByThread(threadId, methodBaseInfo);
        }

        internal void StopMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            threadInfoList.StopMethodTraceByThread(threadId, methodBaseInfo);
        }

        public IEnumerable ThreadInfoDictionary => threadInfoList.ThreadsInfo;
    }
}
