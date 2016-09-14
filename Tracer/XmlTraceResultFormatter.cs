using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Tracer
{
    public class XmlTraceResultFormatter: ITraceResultFormatter
    {

        private string _path;

        private static bool IsValidPath(string path)
        {
            if (Path.IsPathRooted(path))
            {
                Regex driveCheck = new Regex(@"^[a-zA-Z]:\\$");
                if (!driveCheck.IsMatch(path.Substring(0, 3))) return false;
            }
            string strTheseAreInvalidFileNameChars = new string(Path.GetInvalidPathChars());
            strTheseAreInvalidFileNameChars += @":/?*" + "\"";
            Regex containsABadCharacter = new Regex("[" + Regex.Escape(strTheseAreInvalidFileNameChars) + "]");
            if (containsABadCharacter.IsMatch(path.Substring(3, path.Length - 3)))
                return false;

            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath(path));
            if (!dir.Exists)
                dir.Create();
            return true;
        }

        public XmlTraceResultFormatter(string filePath)
        {
            if (!IsValidPath(filePath))
            {
                throw new ArgumentException("this path is invalid");
            }
            _path = filePath;
        }

        public void SetFilePath(string path)
        {
            _path = path;
        }


        public void Format(TraceResult traceResult)
        {
            if (traceResult == null)
            {
                return;
            }
            XDocument doc = new XDocument();
            XElement root = new XElement("root");
            foreach (int threadId in traceResult.ThreadNodes.Keys)
            {
                XElement thread = new XElement("thread", 
                    new XAttribute("id", threadId), 
                    new XAttribute("time", traceResult.ThreadNodes[threadId].ExecutionTime.Milliseconds));
                AddMethods(traceResult.ThreadNodes[threadId].MethodNodes, thread);
                root.Add(thread);
            }

            doc.Add(root);
            doc.Save(_path);
        }

        private void AddMethods(List<MethodNode> methodNodes, XElement root)
        {
            foreach (MethodNode methodNode in methodNodes)
            {
                XElement method = new XElement("method",
                    new XAttribute("time", methodNode.ExecutionTime.Milliseconds),
                    new XAttribute("name", methodNode.MethodName),
                    new XAttribute("class", methodNode.ClassName),
                    new XAttribute("parameters", methodNode.ParameterCount));
                AddMethods(methodNode.InnerMethods, method);
                root.Add(method);
            }
        }

    }
}