using System;
using System.Linq;
using System.Text;

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
            String builder = String.Empty;
            for (int i = 0; i < count - 1; i++)
            {
                builder = builder.Insert(0, "--");
            }
            
            return builder.ToString();
        }
    }  
}
