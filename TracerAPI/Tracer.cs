using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TracerAPI
{
    public class Tracer: ITracer
    {
        private TraceResult TraceInfo;
        private static volatile Tracer instance = null;
        private static readonly Object syncObj = new Object();
       

        private Tracer()
        {
            TraceInfo = new TraceResult();
        }

        public static Tracer Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                        }                        
                    }
                }
                return instance;
            }
        }

        public void StartTrace()
        {
            DateTime startTime = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            StackFrame methodFrame = stackTrace.GetFrame(1);
            StackFrame parentFrame = stackTrace.GetFrame(2);
            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId;

            string signature = methodFrame.GetMethod().ToString();
            string methodName = GetMethodNameFromSignature(signature);

            string parameters = GetMethodParametersFromSignature(signature);
            int numberOfParams = GetNumberOfParameters(parameters);

            signature = parentFrame.GetMethod().ToString();
            string parentName = GetMethodNameFromSignature(signature);

            string methodClassName = methodFrame.GetMethod().ReflectedType.ToString();

            AddStartInfo(threadId, parentName, methodName, numberOfParams, methodClassName, startTime);
  
        }
        public void StopTrace()
        {
            DateTime stopTime = DateTime.Now;

            StackTrace stackTrace = new StackTrace();
            int frameCount = stackTrace.FrameCount;
    
            StackFrame methodStackFrame = stackTrace.GetFrame(1);
            StackFrame parentStackFrame = stackTrace.GetFrame(2);

            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId;

            string signature = methodStackFrame.GetMethod().ToString();
            string methodName = GetMethodNameFromSignature(signature);

            AddStopInfo(threadId, methodName, stopTime);
        }

        public TraceResult GetTraceResult() 
        {
            return TraceInfo; 
        }

        private void AddStartInfo(int threadId, string parentName,
                                        string methodName, int numberOfParams, string methodClassName, 
                                        DateTime startTime)
        {
            Tree tree;           
            tree = Tracer.Instance.GetTraceResult().GetTreeByThreadId(threadId);           
            tree.AddNode(parentName, methodName, numberOfParams, methodClassName, startTime);
        }

        private void AddStopInfo(int threadId, string methodName, DateTime stopTime)
        {
            Tree tree;
            tree = Tracer.Instance.GetTraceResult().GetTreeByThreadId(threadId);
            tree.CompleteNode(methodName, stopTime);
        }

        private bool HasOneParameter(string parametersString)
        {
            return (parametersString.IndexOf(')') - parametersString.IndexOf('(')) > 1;
           
        }

        private string GetMethodNameFromSignature(string signature)
        {
            char[] separator = { ' ' };
            string[] methodSignatureParts = signature.Split(separator);
            int parametersStartPosition = methodSignatureParts[1].IndexOf('(');
            string methodName = methodSignatureParts[1].Substring(0, parametersStartPosition);
            return methodName;
        }

        private string GetMethodParametersFromSignature(string signature)
        {
            int startParametersPosition = signature.IndexOf('(');
            string parameters = signature.Substring(startParametersPosition);
            return parameters;
        }

        private int GetNumberOfParameters(string parameters)
        {
            int numberOfParams = 0;
            char []separator = {','};
            string []parametersParts = parameters.Split(separator);
            if (parametersParts.Count() == 1)
            {
                if(HasOneParameter(parameters))
                {
                    numberOfParams = 1;
                }
            }
            else
            {
                if (parametersParts.Count() > 1)
                {
                    numberOfParams = parametersParts.Count();
                }
            }
            return numberOfParams;
        }
    }
}
