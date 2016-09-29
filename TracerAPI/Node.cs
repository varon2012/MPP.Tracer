using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class Node
    {
        public string MethodName { get; set; }
        public string MethodClassName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public long WholeTime { get; set; }
        public int NumberOfParameters { get; set; }

        private readonly List<Node> children = new List<Node>();
        public List<Node> Children {get { return children; }}

        public Node(string methodName, int numberOfParams, string methodClassName, DateTime startTime)
        {
            MethodName = methodName;
            NumberOfParameters = numberOfParams;
            MethodClassName = methodClassName;
            StartTime = startTime;
        }                     
    }
}
