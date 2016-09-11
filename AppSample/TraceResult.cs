using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

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

	public class ThreadData
	{
		public BasicTreeNode<TraceComponent> RootComponent = null;
		public BasicTreeNode<TraceComponent> CurrentNode = null;
	}

	public ConcurrentDictionary<int, ThreadData> ThreadsData = new ConcurrentDictionary<int, ThreadData>();

	public void StartComponent(string methodName, int paramCount, string className)
	{
		int ThreadId = Thread.CurrentThread.ManagedThreadId;

		// Add new thread data, if it is first call in thread
		if (!ThreadsData.ContainsKey(ThreadId))
		{
			ThreadData NewThreadData = new ThreadData();
			ThreadsData.TryAdd(ThreadId, NewThreadData);
		}

		var CurrentData = ThreadsData[ThreadId];
		TraceComponent NewComponent = new TraceComponent();
		NewComponent.Watch.Start();
		NewComponent.MethodName = methodName;
		NewComponent.ParamCount = paramCount;
		NewComponent.ClassName = className;

		var ComponentNode = new BasicTreeNode<TraceComponent>(NewComponent, CurrentData.CurrentNode);

		if (CurrentData.CurrentNode == null)
		{
			CurrentData.RootComponent = ComponentNode;
		}
		else
		{
			CurrentData.CurrentNode.Children.Add(ComponentNode);
		}

		CurrentData.CurrentNode = ComponentNode;

		Console.WriteLine("Start {0}, Thread {1}", methodName, ThreadId);
	}

	public void StopComponent()
	{
		var CurrentData = GetCurrentThreadData();

		if (CurrentData.CurrentNode == null)
		{
			throw new InvalidTraceException("Trace has been stopped before start.");
		}

		var component = CurrentData.CurrentNode.Data;
		component.Watch.Stop();
		component.ExecutionTime = component.Watch.Elapsed.Milliseconds;

		CurrentData.CurrentNode = CurrentData.CurrentNode.Parent;
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
		try
		{
			return ThreadsData[ThreadId].RootComponent;
		}
		catch (Exception)
		{
			return null;
		}
	}

	private ThreadData GetCurrentThreadData()
	{
		int ThreadId = Thread.CurrentThread.ManagedThreadId;
		try
		{
			return ThreadsData[ThreadId];
		}
		catch (Exception)
		{
			return null;
		}
	}
}
