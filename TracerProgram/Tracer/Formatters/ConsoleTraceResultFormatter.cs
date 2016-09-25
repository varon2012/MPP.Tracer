using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("root\n");
            int countOfThreads;

            countOfThreads = traceResult.threadList.Values.Count();

            ThreadNode tempTreadNode;
            
            for(int i = 0; i < countOfThreads; i++)
            {
                
                tempTreadNode = traceResult.threadList.Values.ElementAt(i);
                
                builder.Append("\nthread = " + tempTreadNode.ID.ToString() + " time = "+tempTreadNode.ThreadWorkingTime  +"ms\n");
                for(int j = 0; j < tempTreadNode.MethodsTree.Count; j++)
                {
                    MethodNode tempMethod = tempTreadNode.MethodsTree.ElementAt(j);
                    builder.Append(new String('-', (tempMethod.Heignt - 1)* 2 ));
                    builder.Append("method name=" + tempMethod.Info.Name + " time=" + tempMethod.Info.Time.ToString()
                        + "ms class=" + tempMethod.Info.ClassName + " params="
                        + tempMethod.Info.ParamsNumber.ToString()+"\n");

                }
            }
            Console.WriteLine(builder.ToString());
        }
    }  
}
