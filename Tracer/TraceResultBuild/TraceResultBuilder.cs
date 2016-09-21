using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuild
{
    internal class TraceResultBuilder
    {
        private readonly IDictionary<long, ThreadInfo> threadsInfo;

        internal TraceResultBuilder(IDictionary<long, ThreadInfo> threadsInfo)
        {
            this.threadsInfo = threadsInfo;
        }

        internal TraceResult GetResult()
        {
            return CreateTraceResult();
        }

        private TraceResult CreateTraceResult()
        {
            return new TraceResult()
            {
                Value = GetThreadsDictionary()
            };
        }
        private Dictionary<long, ThreadInfoResult> GetThreadsDictionary()
        {
            Dictionary<long, ThreadInfoResult> traceResult = new Dictionary<long, ThreadInfoResult>();

            foreach (KeyValuePair<long, ThreadInfo> threadInfo in threadsInfo)
            {
                long threadId = threadInfo.Key;
                ThreadInfoResult threadInfoResult = new ThreadInfoResultBuilder(threadInfo.Value).GetResult();
                traceResult.Add(threadId, threadInfoResult);
            }

            return traceResult;
        }
    }
}
