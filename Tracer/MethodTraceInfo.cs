using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    internal sealed class MethodTraceInfo
    {
        private readonly List<MethodTraceInfo> fNestedCalls;
        private readonly Stopwatch fStopwatch;

        // Internals
        
        internal string Name { get; }
        internal string ClassName { get; }
        internal int ParametersCount { get; }
        internal long ExecutionTime => fStopwatch.ElapsedMilliseconds;

        internal MethodTraceInfo(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.Name;
            ParametersCount = method.GetParameters().Length;
            fStopwatch = new Stopwatch();
            fStopwatch.Start();

            fNestedCalls = new List<MethodTraceInfo>();
        }

        internal void FinishMethodTrace()
        {
            fStopwatch.Stop();
        }

        internal void AddNestedCall(MethodTraceInfo methodTraceInfo)
        {
            fNestedCalls.Add(methodTraceInfo);            
        }

        internal IEnumerable<MethodTraceInfo> NestedCalls => fNestedCalls;
    }
}
