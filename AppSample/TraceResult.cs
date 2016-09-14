using System.Threading;
using System.Collections.Concurrent;

public class TraceResult
{
	public ConcurrentDictionary<int, ThreadData> ThreadsData = new ConcurrentDictionary<int, ThreadData>();

	public void StartComponent(string methodName, int paramCount, string className)
	{
		int threadId = Thread.CurrentThread.ManagedThreadId;

		// Add new thread data, if it is first call in thread
		if (!ThreadsData.ContainsKey(threadId))
		{
			ThreadData newThreadData = new ThreadData();
			ThreadsData.TryAdd(threadId, newThreadData);
		}

		var currentData = ThreadsData[threadId];
		TraceComponent newComponent = new TraceComponent();
		newComponent.Watch.Start();
		newComponent.MethodName = methodName;
		newComponent.ParamCount = paramCount;
		newComponent.ClassName = className;

		var ComponentNode = new BasicTreeNode<TraceComponent>(newComponent, currentData.CurrentNode);

		if (currentData.CurrentNode == null)
		{
			currentData.RootComponent = ComponentNode;
		}
		else
		{
			currentData.CurrentNode.Children.Add(ComponentNode);
		}

		currentData.CurrentNode = ComponentNode;
	}

	public void StopComponent()
	{
		var currentData = GetCurrentThreadData();

		if (currentData.CurrentNode == null)
		{
			throw new InvalidTraceException("Trace has been stopped before start.");
		}

		var component = currentData.CurrentNode.Data;
		component.Watch.Stop();
		component.ExecutionTime = component.Watch.Elapsed.Milliseconds;

		currentData.CurrentNode = currentData.CurrentNode.Parent;
	}

	public bool Validate()
	{
		foreach (var item in ThreadsData)
		{
			if (item.Value.CurrentNode != null)
			{
				return false;
			}
		}

		return true;
	}

	public BasicTreeNode<TraceComponent> GetThreadRootComponent(int ThreadId)
	{
		return ThreadsData[ThreadId].RootComponent;
	}

	private ThreadData GetCurrentThreadData()
	{
		int threadId = Thread.CurrentThread.ManagedThreadId;
		return ThreadsData[threadId];
	}

	public void SetThreadTime(long time)
	{
		int ThreadId = Thread.CurrentThread.ManagedThreadId;
		ThreadsData[ThreadId].ExecutionTime = time;
	}
}
