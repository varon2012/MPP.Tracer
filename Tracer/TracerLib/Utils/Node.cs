using System.Collections.Generic;

namespace TracerLib.Utils
{
	internal class Node<T>
	{
		public List<Node<T>> Children;

		public T Item { get; set; }

		public Node(T item)
		{
			Item = item;
			Children = new List<Node<T>>();
		}

		public void AddChild(Node<T> node)
		{
			Children.Add(node);
		}	
	}
}