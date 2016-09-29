using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        private int globalLevel;

        public void Format(TraceResult result)
        {
            foreach (var thread in result.Children.Values)
            {
                Console.WriteLine("THREAD ID={0}", thread.ModelId);
                PrintMethods(thread.Methods.ToList());
            }
        }

        public void PrintMethods(List<MethodModel> methods)
        {
            globalLevel++;
            var tab = " ";
            for (var i = 0; i < globalLevel; i++)
                tab += " ";
            foreach (var method in methods)
            {
                Console.WriteLine("{0}{1}({2})  {3}  count={4}", tab, method.MethodName, method.ClassName,
                    (int) method.Time.TotalMilliseconds, method.ParametersCount);
                if (method.Children.Count > 0)
                    PrintMethods(method.Children.ToList());
            }
            globalLevel--;
        }
    }
}