using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    class Node
    {
        public string MethodName { set; public get; }
        public DateTime MethodStartTime { set; public get; }
        public DateTime MethodStopTime { set; public get; }
        public long MethodWholeTime { set; public get; }
        public string MethodClassName { set; public get; }
        public int NumberOfParameters { set; public get; }

        public List<Node> Children = new List<Node>();             
    }
}
