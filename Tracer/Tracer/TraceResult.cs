using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public Dictionary<int, Tree<MethodInfo>> MethodInfoDictionary { get; set; }
        private readonly Dictionary<int, Tree<MethodInfo>> currentThreadMethods;

        public TraceResult()
        {
            MethodInfoDictionary = new Dictionary<int, Tree<MethodInfo>>();
            currentThreadMethods = new Dictionary<int, Tree<MethodInfo>>();
        }

        public void AddToTree(MethodBase methodBase)
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            Tree<MethodInfo> method = new Tree<MethodInfo>(
                new MethodInfo
                {
                    MethodName = methodBase.Name,
                    ClassName = methodBase.DeclaringType.ToString(),
                    Watcher = Stopwatch.StartNew(),
                    NumberParametres = methodBase.GetParameters().Length
                }, null);

            if (!MethodInfoDictionary.ContainsKey(currentThreadId))
            {
                MethodInfoDictionary.Add(currentThreadId, null);
                method.PreviousNode = null;
                currentThreadMethods.Add(currentThreadId, null);
            }

            if (currentThreadMethods[currentThreadId] == null)
            {
                MethodInfoDictionary[currentThreadId] = method;
            }
            else
            {
                currentThreadMethods[currentThreadId].NodesList.Add(method);
                method.PreviousNode = currentThreadMethods[currentThreadId];
            }
            currentThreadMethods[currentThreadId] = method;
        }

        public void RemoveFromTree()
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if (currentThreadMethods[currentThreadId] != null)
            {
                currentThreadMethods[currentThreadId].Node.Watcher.Stop();
                currentThreadMethods[currentThreadId] = currentThreadMethods[currentThreadId].PreviousNode;
            }
        }
    }
}
