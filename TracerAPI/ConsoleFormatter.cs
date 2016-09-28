using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class ConsoleFormatter: ITraceResultFormatter
    {
        private List<string> stack = new List<string>();
       
        public void Format(TraceResult traceResult)
        {
            int level = 0;
            
            if (traceResult.Result.Count > 0) {
                Console.WriteLine("Threads (");
                stack.Add(")");
                level++;
                foreach (int key in traceResult.Result.Keys)
                {
                    Console.WriteLine( GetIndention(level)+"Thread id = {0}: (", key);
                    stack.Add(")");
                    
                    AddMethodsToTread(traceResult[key].Root, level);
                    Console.WriteLine(GetIndention(level) + stack.Last());
                    stack.RemoveAt(stack.Count-1);
                }
                level--;
                Console.WriteLine(GetIndention(level) + stack.Last());
                stack.RemoveAt(stack.Count-1);
            }
        }

        private void AddMethodsToTread(Node node, int level)
        {
            level++;
            Console.WriteLine(GetIndention(level) + "Method name = {0},",node.MethodName);
            level++;
            Console.WriteLine(GetIndention(level) + "parameters = {0},", node.NumberOfParameters);
            Console.WriteLine(GetIndention(level) + "class-name = {0},", node.MethodClassName); 
            Console.Write(GetIndention(level) + "time = {0}:(", node.WholeTime);
            level--;
            if (node.Children.Count == 0)
            {
                Console.WriteLine(")");
            }
            else
            {
                Console.WriteLine();
                stack.Add(")");
                foreach(Node child in node.Children){
                    AddMethodsToTread(child, level);
                }
                Console.WriteLine(GetIndention(level) + stack.Last());
                stack.RemoveAt(stack.Count - 1);
            }
        }

        private string GetIndention(int level)
        {
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            for (int i = 0; i < level; i++)
                stringBuilder.Append("  ");
            return stringBuilder.ToString();
        }
    }
}
