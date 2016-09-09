using System.Collections.Generic;

public class BasicTreeNode<T>
{
	public List<BasicTreeNode<T>> Children = new List<BasicTreeNode<T>>();
	public BasicTreeNode<T> Parent = null;
	public T Data;

	public delegate void ProcessorFunction(T data, int depth);

	public int Depth
	{
		get
		{
			var CurrentNode = this;
			int depth = 0;

			while (CurrentNode != null)
			{
				CurrentNode = CurrentNode.Parent;
				depth++;
			}

			return depth;
		}
	}

	public BasicTreeNode(T data, BasicTreeNode<T> parent = null)
	{
		Data = data;
		Parent = parent;
	}

	public void Visit(ProcessorFunction function)
	{
		function(Data, Depth);

		foreach (var child in Children)
		{
			child.Visit(function);
		}
	}

	
}
