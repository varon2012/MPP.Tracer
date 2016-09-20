using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        private readonly static object locker = new object();

        public void Format(TraceResult traceResult)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("root\n");
            int countOfThreads;

            lock(locker)
            {
                countOfThreads = traceResult.threadList.Values.Count();
            }

            ThreadNode tempTreadNode;
            
            for(int i = 0; i < countOfThreads; i++)
            {
                lock(locker)
                {
                    tempTreadNode = traceResult.threadList.Values.ElementAt(i);
                }
                builder.Append("\nthread = " + tempTreadNode.ID.ToString() +"\n");
                for(int j = 0; j < tempTreadNode.MethodsTree.Count; j++)
                {
                    MethodNode tempMethod = tempTreadNode.MethodsTree.ElementAt(j);
                    builder.Append(GetSpaces(tempMethod.Heignt));
                    builder.Append("method name=" + tempMethod.Info.Name + " time=" + tempMethod.Info.Time.ToString()
                        + "ms class=" + tempMethod.Info.ClassName + " params="
                        + tempMethod.Info.ParamsNumber.ToString()+"\n");

                }
            }
            Console.WriteLine(builder.ToString());
        }

        private string GetSpaces(int count)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < count - 1; i++)
            {
                builder.Append("--");
            }
            
            return builder.ToString();
        }
    }  
}
