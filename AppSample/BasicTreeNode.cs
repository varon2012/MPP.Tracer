using System.Collections.Generic;

public class BasicTreeNode<T>
{
	#region Fields
	private List<BasicTreeNode<T>> _children = new List<BasicTreeNode<T>>();
	private BasicTreeNode<T> _parent;
	public T Data;
	#endregion

	#region Properties
	public List<BasicTreeNode<T>> Children
	{
		get
		{
			return _children;
		}
	}

	public BasicTreeNode<T> Parent
	{
		get
		{
			return _parent;
		}
	}

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
	#endregion

	public delegate void ProcessorFunction(T data, int depth);

	public BasicTreeNode(T data, BasicTreeNode<T> parent = null)
	{
		Data = data;
		_parent = parent;
		_children = new List<BasicTreeNode<T>>();
	}

	public void Visit(ProcessorFunction function)
	{
		function(Data, Depth);

		foreach (var child in _children)
		{
			child.Visit(function);
		}
	}

	
}
