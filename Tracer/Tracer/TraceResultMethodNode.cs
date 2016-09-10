using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal sealed class TraceResultMethodNode
    {
        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
        public int ParamCount { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime StopTime { get; private set; }
        public TimeSpan TotalTime { get; private set; }

        public List<TraceResultMethodNode> InsertedNodes { get; private set; }

        public TraceResultMethodNode(string methodName,
            string className, int paramCount)
        {
            this.InsertedNodes = null;

            this.MethodName = methodName;
            this.ClassName = className;
            this.ParamCount = paramCount;
        }

        public void StartNode()
        {   
            this.StartTime = DateTime.Now;
        }

        public void FinishNode()
        {
            this.StopTime = DateTime.Now;

            this.TotalTime = this.StopTime - this.StartTime;
        }

        public void AddInsertedNode(TraceResultMethodNode newNode)
        {
            if (this.InsertedNodes == null)
                this.InsertedNodes = new List<TraceResultMethodNode>();

            this.InsertedNodes.Add(newNode);
        }
    }
}
