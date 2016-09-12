using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class JsonView: ITracerFormatter {
        private List<Tracer.TracerThreadTree> _threadList;

        public JsonView(List<Tracer.TracerThreadTree> threadList) {
            _threadList = threadList;
        }

        public string Parse() {
            string jsonString = JsonConvert.SerializeObject(_threadList);

            string fileName = DateTime.UtcNow.ToFileTimeUtc().ToString() + ".json";
            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(jsonString);
            }

            return fileName;
        }

        public Stream ParseToStream() {
            string jsonString = JsonConvert.SerializeObject(_threadList);
            Stream stream = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(jsonString);
            }

            return stream;
        }
    }
}
