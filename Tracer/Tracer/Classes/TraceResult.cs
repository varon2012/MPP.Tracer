using System.Collections.Concurrent;
using System.Reflection;
using Trace.Classes.Information;

namespace Trace.Classes
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadInfo> threadsInfo;

        public TraceResult()
        {
            this.threadsInfo = new ConcurrentDictionary<int, ThreadInfo>();
        }

        public void StartMethodTrace(int idThread, MethodBase methodBase)
        {
            var threadInfo = threadsInfo.GetOrAdd(idThread, new ThreadInfo());
        }
    }
}
