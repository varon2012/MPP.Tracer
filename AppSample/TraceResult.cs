using System.Diagnostics;

public class TraceResult
{
	public class TraceComponent
	{
		public string MethodName;
		public string ClassName;
		public int ExecutionTime;
		public int ParamCount;

		public Stopwatch Watch = new Stopwatch();
	}

	public BasicTreeNode<TraceComponent> RootComponent = null;
	private BasicTreeNode<TraceComponent> CurrentNode = null;
	public int ThreadId;

	public void StartComponent(string methodName, int paramCount, string className)
	{
		TraceComponent NewComponent = new TraceComponent();
		NewComponent.Watch.Start();
		NewComponent.MethodName = methodName;
		NewComponent.ParamCount = paramCount;
		NewComponent.ClassName = className;

		var ComponentNode = new BasicTreeNode<TraceComponent>(NewComponent, CurrentNode);

		if (CurrentNode == null)
		{
			RootComponent = ComponentNode;
		}
		else
		{
			CurrentNode.Children.Add(ComponentNode);
		}

		CurrentNode = ComponentNode;
	}

	public void StopComponent()
	{
		if (CurrentNode == null)
		{
			throw new InvalidTraceException("Trace has been stopped before start.");
		}

		var component = CurrentNode.Data;
		component.Watch.Stop();
		component.ExecutionTime = component.Watch.Elapsed.Milliseconds;

		CurrentNode = CurrentNode.Parent;
	}

	public bool Validate()
	{
		return (CurrentNode == null);
	}

	public override string ToString() 
	{
		return "";
	}
}
