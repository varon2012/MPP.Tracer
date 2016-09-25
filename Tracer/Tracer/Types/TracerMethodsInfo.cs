using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Concurrent;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Types
{
    public class TracerMethodsInfo
    {
        private readonly Stack<MethodsTree> _methodsStack;
        private readonly List<MethodsTree> _methodsList;

        public List<MethodsTree> MethodListInThread
        {
            get { return _methodsList; }
        }

        public TracerMethodsInfo()
        {
            _methodsStack = new Stack<MethodsTree>();
            _methodsList = new List<MethodsTree>();
        }

        private MethodsTree CreateMethodTreeElement(MethodInfo method)
        {
            MethodsTree methodTreeElement = new MethodsTree();
            methodTreeElement.Element = method;
            methodTreeElement.Childs = new List<MethodsTree>();

            return methodTreeElement;
        }

        internal void StartMethodTrace(MethodBase currentMethod)
        {
            MethodInfo methodInfo = new MethodInfo();
            methodInfo.StartMethodTrace(currentMethod);

            MethodsTree TreeElement = CreateMethodTreeElement(methodInfo);

            if(_methodsStack.Count == 0)
            {
                _methodsList.Add(TreeElement);
            }
            else
            {
                MethodsTree lastMethodInThread = _methodsStack.Peek();
                lastMethodInThread.Childs.Add(TreeElement);
            }

            _methodsStack.Push(TreeElement);
        }

        internal void StopMethodTrace(MethodBase currentMethod)
        {
            if(_methodsStack.Count == 0)
            {
                throw new InvalidOperationException("Stack is empty.");
            }
            MethodsTree lastMethodTreeElementInThread = _methodsStack.Peek();

            if(!lastMethodTreeElementInThread.Element.IsEqual(currentMethod))
            {
                throw new InvalidOperationException("Error in the order of calls.");
            }

            lastMethodTreeElementInThread.Element.StopMethodTrace();
            _methodsStack.Pop();
        }
    }
}
