using System.Xml;

public class XmlTraceResultFormatter : ITraceResultFormatter
{
	public string OutputFileName = "TracerOutput.xml";

	public void Format(TraceResult traceResult)
	{
		XmlDocument document = new XmlDocument();
		var Root = document.CreateElement("root");
		document.AppendChild(Root);

		var tracedThreadsData = traceResult.ThreadsData;
		foreach (var threadData in tracedThreadsData)
		{
			var ThreadNode = document.CreateElement("thread");

			var IdAttribute = document.CreateAttribute("id");
			IdAttribute.Value = threadData.Key.ToString();
			ThreadNode.Attributes.Append(IdAttribute);
			var TimeAttribute = document.CreateAttribute("time");
			TimeAttribute.Value = threadData.Value.ExecutionTime.ToString();
			ThreadNode.Attributes.Append(TimeAttribute);

			Root.AppendChild(ThreadNode);

			var threadRoot = traceResult.GetThreadRootComponent(threadData.Key);
			ProcessNode(threadRoot, document, ThreadNode);
		}

		document.Save(OutputFileName);
	}

	public void ProcessNode(BasicTreeNode<TraceResult.TraceComponent> node, XmlDocument document, XmlElement parent)
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

		foreach (var Attribute in nodeAttributes)
		{
			methodNode.Attributes.Append(Attribute);
		}

		parent.AppendChild(methodNode);

		// Process children
		foreach (var traceResultNode in node.Children)
		{
			ProcessNode(traceResultNode, document, methodNode);
		}
	}
}
