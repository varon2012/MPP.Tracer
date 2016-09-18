using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        private ThreadInfoList threadInfoList;

        public TraceResult()
        {
            threadInfoList = new ThreadInfoList();
        }

        public void StartMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            threadInfoList.StartMethodTraceByThread(threadId, methodBaseInfo);
        }

        public void StopMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            threadInfoList.StopMethodTraceByThread(threadId, methodBaseInfo);
        }
    }
}
