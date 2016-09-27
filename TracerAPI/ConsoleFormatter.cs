using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerAPI
{
    public class ConsoleFormatter
    {
        private List<string> stack = new List<string>();
        private StringBuilder stringBuilder = new StringBuilder(string.Empty);
       
        public void Format(TraceResult traceResult)
        {
            int level = 0;
            
            if (traceResult.Result.Count > 0) {
                Console.WriteLine("<Threads>");
                stack.Add("</Threads>");
                level++;
                foreach (int key in traceResult.Result.Keys)
                {
                    Console.WriteLine(GetIndention(level)+"<Thread id = {0}>",key);
                    stack.Add("</Thread>");
                    
                    AddMethodsToTread(traceResult[key].Root, level);
                    Console.WriteLine(stack.Last());
                    stack.RemoveAt(stack.Count-1);
                }
                level--;
                Console.WriteLine(stack.Last());
                stack.RemoveAt(stack.Count-1);
            }
        }

        private void AddMethodsToTread(Node node, int level)
        {
            level++;
            Console.Write(GetIndention(level) + 
                "<Method name = {0} num-of-param = {1} method-class-name = {2} time = {3} ",
                node.MethodName, node.NumberOfParameters, node.MethodClassName, node.WholeTime);
            if (node.Children.Count == 0)
            {
                Console.WriteLine("/>");
            }
            else
            {
                Console.WriteLine(">");
                stack.Add("</Method>");
                foreach(Node child in node.Children){
                    AddMethodsToTread(child, level);
                }
                Console.WriteLine(stack.Last());
                stack.RemoveAt(stack.Count - 1);
            }
        }

        private string GetIndention(int level)
        {
            StringBuilder stringBuilder = new StringBuilder(string.Empty);
            for (int i = 0; i < level; i++)
                stringBuilder.Append("   ");
            return stringBuilder.ToString();
        }
    }
}
