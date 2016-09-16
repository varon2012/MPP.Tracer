using System;
using System.Collections.Generic;
using Tracer.Contracts;
using Tracer.Model.ViewModels;

namespace Tracer.Service
{
    public class ConsoleResultFormatter : ITraceResultFormatter
    {
        #region Public Methods
        public void Format(TraceResult traceResult)
        {
            if (traceResult == null || traceResult.Threads == null)
                throw new ArgumentNullException("TraceResult is empty");
            foreach (var thread in traceResult.Threads)
            {
                Console.WriteLine(string.Format("Thread id: {0}, Lead time: {1}",thread.Id.ToString(), thread.LeadTime));
                OutputMethod(thread.Methods,1);
            }
        }

        #endregion

        #region Private Methods

        private void OutputMethod(List<MethodViewModel> methods, int nestingLevel)
        {
            string tabs = new string('\t', nestingLevel);
            foreach (var method in methods)
            {
                Console.WriteLine("{0}Class name: {1}, Method name: {2}, Params count: {3}, Lead time: {4}",
                        tabs,method.ClassName, method.MethodName, method.ParamsCount, method.LeadTime);
                OutputMethod(method.InternalMethods, ++nestingLevel);
            }
        }

        #endregion
    }
}
