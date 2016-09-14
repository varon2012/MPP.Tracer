using System.Xml;

public class XmlTraceResultFormatter : ITraceResultFormatter
{
	public string OutputFileName;

	public XmlTraceResultFormatter(string outputFileName = "TracerOutput.xml")
	{
		OutputFileName = outputFileName;
	}

	public void Format(TraceResult traceResult)
	{
		XmlDocument document = new XmlDocument();
		var root = document.CreateElement("root");
		document.AppendChild(root);

		var tracedThreadsData = traceResult.ThreadsData;
		foreach (var threadData in tracedThreadsData)
		{
			var threadNode = document.CreateElement("thread");

			var IdAttribute = document.CreateAttribute("id");
			IdAttribute.Value = threadData.Key.ToString();
			threadNode.Attributes.Append(IdAttribute);
			var TimeAttribute = document.CreateAttribute("time");
			TimeAttribute.Value = threadData.Value.ExecutionTime.ToString();
			threadNode.Attributes.Append(TimeAttribute);

			root.AppendChild(threadNode);

			var threadRoot = traceResult.GetThreadRootComponent(threadData.Key);
			ProcessNode(threadRoot, document, threadNode);
		}

		document.Save(OutputFileName);
	}

	public void ProcessNode(BasicTreeNode<TraceComponent> node, XmlDocument document, XmlElement parent)
	{
		// Create node for current method
		var methodNode = document.CreateElement("method");

		XmlAttribute[] nodeAttributes = new XmlAttribute[4]
		{
			document.CreateAttribute("name"),
			document.CreateAttribute("time"),
			document.CreateAttribute("class"),
			document.CreateAttribute("params")
		};

		nodeAttributes[0].Value = node.Data.MethodName;
		nodeAttributes[1].Value = node.Data.ExecutionTime.ToString();
		nodeAttributes[2].Value = node.Data.ClassName;
		nodeAttributes[3].Value = node.Data.ParamCount.ToString();

		foreach (var attribute in nodeAttributes)
		{
			methodNode.Attributes.Append(attribute);
		}

		parent.AppendChild(methodNode);

		// Process children
		foreach (var traceResultNode in node.Children)
		{
			ProcessNode(traceResultNode, document, methodNode);
		}
	}
}
