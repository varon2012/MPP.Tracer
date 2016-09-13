using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Formatter {
    public class JsonView: ITracerFormatter {
        public string Parse(List<TracerThreadTree> threadList) {
            return ParseFromObjToString(threadList);
        }

        public Stream ParseToStream(List<TracerThreadTree> threadList) {
            return ParseFromObjToStream(threadList);
        }

        private string ParseFromObjToString(List<TracerThreadTree> list) {
            string jsonString = JsonConvert.SerializeObject(list);

            string fileName = DateTime.UtcNow.ToFileTimeUtc().ToString() + ".json";
            FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);

            using (StreamWriter writer = new StreamWriter(file)) {
                writer.Write(jsonString);
            }

            return fileName;
        }

        private Stream ParseFromObjToStream(List<TracerThreadTree> list) {
            string jsonString = JsonConvert.SerializeObject(list);
            Stream stream = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(stream)) {
                writer.Write(jsonString);
            }

            return stream;
        }
    }
}
