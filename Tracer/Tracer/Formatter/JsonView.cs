using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Concurrent;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class JsonView: ITracerFormatter {
        private string _fileName;

        public JsonView(string fileName) {
            _fileName = fileName;
        }

        public void Parse(Dictionary<int, List<MethodsTree>> threadList)
        {
            string jsonString = JsonConvert.SerializeObject(threadList);

            FileStream file = new FileStream(_fileName, FileMode.Create, FileAccess.Write);

            using(StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(jsonString);
            }
        }
    }
}
