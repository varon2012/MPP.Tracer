using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class Tree
    {
        public Node Root;

        public Tree()
        {
            Root = null;
        }

        public void AddNode(string parentName, string methodName, string methodClassName, DateTime startTime)
        {
            Node newNode = new Node(methodName, methodClassName, startTime);
            Node parent = GetParentNode(parentName, Root);
            if (parent == null)
            {
                Root = newNode;
            }
            else
            {
                parent.Children.Add(newNode);
            }
        }

        public void CompleteNode()
        

        public Node GetParentNode(string parentMethodName, Node tempNode)
        {
            Node parentNode = null;
            if(parentMethodName == Root.MethodName)
                parentNode =  Root;
            else 
            { 
                foreach (Node node in tempNode.Children)
                {
                    if (node.MethodName == parentMethodName)
                        parentNode =  node;
                    else
                    {
                        if (node.Children.Count > 0)
                        {
                            GetParentNode(parentMethodName, node);
                        }
                    }
                }
            }
            return parentNode;
        }
    }
}
