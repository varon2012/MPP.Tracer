using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Trace.Classes.Information;
using MethodInfo = Trace.Classes.Information.MethodInfo;

namespace Trace.Classes
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, ThreadInfo> _threadsInfo;

        public TraceResult()
        {
            _threadsInfo = new ConcurrentDictionary<int, ThreadInfo>();
        }

        public void StartListenThread(int idThread, MethodBase methodBase)
        {
            var threadInfo = _threadsInfo.GetOrAdd(idThread, new ThreadInfo());
            threadInfo.StartListenMethod( new MethodInfo(methodBase));
        }

        public void StopListenThread(int idThread)
        {
            ThreadInfo threadInfo;

            if (_threadsInfo.TryGetValue(idThread, out threadInfo))
            {
                threadInfo.StopListenMethod();
            }
        }

        public IEnumerable<KeyValuePair<int, ThreadInfo>> ThreadsInfo => _threadsInfo;
    }
}
