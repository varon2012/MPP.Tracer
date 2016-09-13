using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class ConsoleView: ITracerFormatter {
        public string Parse(List<TracerThreadTree> threadList) {
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < threadList.Count; i++) {
                line.Append("Thread number: ").AppendLine(threadList[i].ThreadId.ToString())
                    .Append(GetTracersTree(threadList[i].Child, ""))
                    .AppendLine("------");
            }

            Console.WriteLine(line.ToString());
            return string.Empty;
        }

        private string GetTracersTree(List<TracerTree> tree, string separator) {
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < tree.Count; i++) {
                TracerTree tracer = tree[i];
                TracerInfo tracerInfo = tracer.Element;

                line.Append(separator)
                    .Append("Method: ").Append(tracerInfo.MethodName).Append(", ")
                    .Append("Count of Params: ").Append(tracerInfo.CountParams.ToString()).Append(", ")
                    .Append(tracerInfo.ClassName).Append(". ")
                    .Append("Milliseconds: ").Append(tracerInfo.Time.ToString())
                    .AppendLine();
                if (tracer.Child.Count > 0) line.Append(GetTracersTree(tracer.Child, separator + "  "));
            }

            return line.ToString();
        }

        public Stream ParseToStream(List<TracerThreadTree> threadList) {
            string stringObj = Parse(threadList);
            Stream stream = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(stringObj);
            }

            return stream;
        }
    }
}
