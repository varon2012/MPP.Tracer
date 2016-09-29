using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure
{
    public class Tracer : ITracer
    {
        private readonly Dictionary<int, ThreadModel> threads;

        public Tracer()
        {
            threads = new Dictionary<int, ThreadModel>();
        }

        public void StartTrace()
        {
            lock (threads)
            {
                var stackTrace = new StackTrace();
                var method = TraceHelpers.GetMethodModel(stackTrace);

                if (!threads.ContainsKey(Thread.CurrentThread.ManagedThreadId)) { 
                    threads.Add(Thread.CurrentThread.ManagedThreadId, new ThreadModel
                            {
                                ModelId = Thread.CurrentThread.ManagedThreadId
                    });
                }
                threads[Thread.CurrentThread.ManagedThreadId].AddStartPartMethod(method);
                    
            }
        }

        public void StopTrace()
        {
            lock (threads)
            {
                threads.Values.First(x => x.ModelId == Thread.CurrentThread.ManagedThreadId).AddEndPartMethod();
            }
        }

        public TraceResult GetTraceResult()
        {
            lock (threads)
            {
                foreach (var thread in threads)
                    thread.Value.Time =
                        TimeSpan.FromMilliseconds(thread.Value.Methods.Sum(x => (int) x.Time.TotalMilliseconds));
                return new TraceResult {Children = threads};
            }
        }
    }
}