using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Trace.Classes.TraceInfo;

namespace Trace.Classes
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, ThreadTrace> _threadsInfo;

        public TraceResult()
        {
            _threadsInfo = new ConcurrentDictionary<int, ThreadTrace>();
        }

        public void StartListenThread(int idThread, MethodBase methodBase)
        {
            var threadInfo = _threadsInfo.GetOrAdd(idThread, new ThreadTrace());
            threadInfo.StartListenMethod( new MethodTrace(methodBase));
        }

        public void StopListenThread(int idThread)
        {
            ThreadTrace threadInfo;

            if (_threadsInfo.TryGetValue(idThread, out threadInfo))
            {
                threadInfo.StopListenMethod();
            }
        }

        public IEnumerable<KeyValuePair<int, ThreadTrace>> ThreadsInfo => _threadsInfo;
    }
}
