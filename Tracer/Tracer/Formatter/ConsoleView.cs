using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class ConsoleView: ITracerFormatter {
        public void Parse(Dictionary<int, List<MethodsTree>> threadsList)
        {
            StringBuilder line = new StringBuilder();

            foreach(var thread in threadsList)
            {
                line.Append("Thread number: ").AppendLine(thread.Key.ToString())
                    .Append(GetTracersTree(thread.Value, ""))
                    .AppendLine("------");
            }

            Console.WriteLine(line.ToString());
        }

        private string GetTracersTree(List<MethodsTree> tree, string separator)
        {
            StringBuilder line = new StringBuilder();

            for(int i = 0; i < tree.Count; i++)
            {
                MethodsTree currentTree = tree[i];
                MethodInfo methodInfo = currentTree.Element;

                line.Append(separator)
                    .Append("Method: ").Append(methodInfo.MethodName).Append(", ")
                    .Append("Count of Params: ").Append(methodInfo.CountParams.ToString()).Append(", ")
                    .Append(methodInfo.ClassName).Append(". ")
                    .Append("Milliseconds: ").Append(methodInfo.Time.ToString())
                    .AppendLine();
                if(currentTree.Childs.Count > 0) line.Append(GetTracersTree(currentTree.Childs, separator + "  "));
            }

            return line.ToString();
        }
    }
}
