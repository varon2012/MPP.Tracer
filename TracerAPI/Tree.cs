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
            node.WholeTime = node.StopTime.Millisecond - node.StartTime.Millisecond;
        }
        

        public Node GetParentNodeByName(string parentMethodName, Node tempNode)
        {
            Node parentNode = null;
            if(Root != null)
            { 
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
                            Node temp  = null;
                            if (parentNode == null && node.Children.Count > 0)
                            {
                              temp =  GetParentNodeByName(parentMethodName, node);
                              if (temp != null)
                                  return temp;
                            }
                        }
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
                    foreach(Node child in tempNode.Children)
                    {
                        if(child.MethodName == methodName)
                        {
                            node = child;
                            break;
                        }
                        Node temp = null;
                        if (node == null) 
                        { 
                            temp = GetNodeByName(methodName, child);
                            if (temp != null)
                                return temp;
                        }
                    }
                }
            }
            return node;
        }
    }
}
