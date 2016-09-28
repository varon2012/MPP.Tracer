using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class Node
    {
        public string MethodName;
        public string MethodClassName;
        public DateTime StartTime;
        public DateTime StopTime;
        public long WholeTime;        
        public int NumberOfParameters;

        public Node(string methodName, int numberOfParams, string methodClassName, DateTime startTime)
        {
            MethodName = methodName;
            NumberOfParameters = numberOfParams;
            MethodClassName = methodClassName;
            StartTime = startTime;
        }

        public List<Node> Children = new List<Node>();             
    }
}
