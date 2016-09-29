using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class ConsoleFormatter: ITraceResultFormatter
    {
        private readonly Stack<string> stack = new Stack<string>();
       
        public void Format(TraceResult traceResult)
        {
            int level = 0;
            string indention;

            if (traceResult.Result.Count > 0) {

                Console.WriteLine("Threads (");
                stack.Push(")");

                level++;
                indention = new String(' ', level * 2);
                foreach (int key in traceResult.Result.Keys)
                {
                    Console.WriteLine(indention + "Thread id = {0}: (", key);
                    stack.Push(")");
                    
                    AddMethodsToTread(traceResult[key].Root, level);
                    Console.WriteLine(indention + stack.Last());
                    stack.Pop();
                }

                level--;
                indention = new String(' ', level * 2);
                Console.WriteLine(indention + stack.Last());
                stack.Pop();
            }
        }

        private void AddMethodsToTread(Node node, int level)
        {
            string indention;

            level++;
            indention = new String(' ', level * 2);
            Console.WriteLine(indention + "Method name = {0},",node.MethodName);

            level++;
            indention = new String(' ', level * 2);
            Console.WriteLine(indention + "parameters = {0},", node.NumberOfParameters);
            Console.WriteLine(indention + "class-name = {0},", node.MethodClassName); 
            Console.Write(indention + "time = {0}:(", node.WholeTime);

            level--;
            if (node.Children.Count == 0)
            {
                Console.WriteLine(")");
            }
            else
            {
                Console.WriteLine();
                stack.Push(")");
                foreach(Node child in node.Children){
                    AddMethodsToTread(child, level);
                }

                indention = new String(' ', level * 2);
                Console.WriteLine(indention + stack.Last());
                stack.Pop();
            }
        }
    }
}
