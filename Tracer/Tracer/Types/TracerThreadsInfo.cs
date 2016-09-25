using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace BSUIR.Mishin.Tracer.Types
{
    public class TracerThreadsInfo
    {
        private ConcurrentDictionary<int, TracerMethodsInfo> _threadsStack;
        private ConcurrentBag<Thread> _threadsList;

        internal TracerThreadsInfo()
        {
            _threadsStack = new ConcurrentDictionary<int, TracerMethodsInfo>();
            _threadsList = new ConcurrentBag<Thread>();
        }

        public Dictionary<int, List<MethodsTree>> GetThreadsStack()
        {
            Dictionary<int, List<MethodsTree>> temp = new Dictionary<int, List<MethodsTree>>();
            foreach(var a in _threadsStack)
            {
                temp.Add(a.Key, a.Value.MethodListInThread);
            }
            return temp;
        }

        internal void StartTrace(MethodBase method, int threadId)
        {
            TracerMethodsInfo currentMethod;
            if(_threadsStack.ContainsKey(threadId))
            {
                currentMethod = _threadsStack[threadId];
            }
            else
            {
                _threadsList.Add(Thread.CurrentThread);
                currentMethod = new TracerMethodsInfo();
                _threadsStack.TryAdd(threadId, currentMethod);
            }

            currentMethod.StartMethodTrace(method);
        }

        internal void StopTrace(MethodBase method, int threadId)
        {
            TracerMethodsInfo currentMethod;
            if(_threadsStack.ContainsKey(threadId))
            {
                currentMethod = _threadsStack[threadId];


            }
            else
            {
                throw new Exception("The current thread didn't have any tracers");
            }

            currentMethod.StopMethodTrace(method);
        }

        internal void WaitStopAllThreads()
        {
            bool isEnd;
            do
            {
                isEnd = true;
                int currentCount = _threadsList.Count;
                foreach(Thread thread in _threadsList)
                {
                    if(thread == Thread.CurrentThread) continue;
                    thread.Join();
                    if(currentCount != _threadsList.Count)
                    {
                        isEnd = false;
                        break;
                    }
                }
            }
            while(!isEnd);
        }
    }
}
