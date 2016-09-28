using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class Tree
    {
        private Node root;
        public Node Root { get { return root; } set { root = value; } }

        public Tree()
        {
            Root = null;
        }

        public void AddNode(string parentName, string methodName, int numberOfParams, string methodClassName, DateTime startTime)
        {
            Node newNode = new Node(methodName, numberOfParams, methodClassName, startTime);
            Node parent = GetParentNodeByName(parentName, Root);
            if (parent == null)
            {
                Root = newNode;
            }
            else
            {
                parent.Children.Add(newNode);
            }
        }

        public void CompleteNode(string methodName, DateTime stopTime)
        {
            Node node = GetNodeByName(methodName, Root);
            node.StopTime = stopTime;
            node.WholeTime = node.StopTime.Millisecond +
                             node.StopTime.Second * 1000 +
                             node.StopTime.Minute * 1000 * 60 -
                             (node.StartTime.Millisecond +
                             node.StartTime.Second * 1000 +
                             node.StartTime.Minute * 1000 * 60);
        }
        

        public Node GetParentNodeByName(string parentMethodName, Node tempNode)
        {
            Node parentNode = null;
            if(Root != null)
            { 
                if(parentMethodName == tempNode.MethodName)
                    parentNode =  tempNode;
                else 
                { 
                    if(tempNode.Children.Count > 0)
                    {
                        Node lastChild = tempNode.Children.Last();
                        parentNode = GetParentNodeByName(parentMethodName, lastChild);
                        if (parentNode != null)
                            return parentNode;
                    }
                }
            }
            return parentNode;
        }

        public Node GetNodeByName(string methodName, Node tempNode)
        {
            Node node = null;
            if(tempNode.MethodName == methodName)
                node = tempNode;
            else
            {
                if (tempNode.Children.Count > 0) 
                {
                    Node lastChild = tempNode.Children.Last();
                    node = GetNodeByName(methodName, lastChild);
                    if (node != null)
                        return node;
                }
            }
            return node;
        }
    }
}
