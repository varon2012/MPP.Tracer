using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class ConsoleView: ITracerFormatter {
        private List<Tracer.TracerThreadTree> _threadList;

        public ConsoleView(List<Tracer.TracerThreadTree> threadList) {
            _threadList = threadList;
        }

        public string Parse() {
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < _threadList.Count; i++) {
                line.Append("Thread number: ").AppendLine(_threadList[i].ThreadId.ToString())
                    .Append(GetTracersTree(_threadList[i].Child, ""))
                    .AppendLine("------");
            }

            Console.WriteLine(line.ToString());
            return "";
        }

        private string GetTracersTree(List<Tracer.TracerTree> tree, string separator) {
            StringBuilder line = new StringBuilder();

            for (int i = 0; i < tree.Count; i++) {
                Tracer.TracerTree tracer = tree[i];
                Tracer.TracerInfo tracerInfo = tracer.Element;

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

        public Stream ParseToStream() {
            string stringObj = Parse();
            Stream stream = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(stringObj);
            }

            return stream;
        }
    }
}
