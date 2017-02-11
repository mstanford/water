//
// AUTOGENERATED 1/1/2009 12:42:21 PM
//
using System;

namespace Ice
{
	public class Node : System.IComparable
	{
		public int Type;
		public string Value;
		public System.Collections.Generic.List<Node> Nodes = new System.Collections.Generic.List<Node>();

		public Node(int type)
		{
			Type = type;
		}

		public Node(int type, string value)
		{
			Type = type;
			Value = value;
		}

		public int CompareTo(object obj)
		{
			Node node = (Node)obj;
			int cmp = this.Type.CompareTo(node.Type);
			if (cmp != 0)
			{
				return cmp;
			}
			if (this.Value != null && node.Value != null)
			{
				cmp = this.Value.CompareTo(node.Value);
				if (cmp != 0)
				{
					return cmp;
				}
			}
			else if (this.Value != null)
			{
				return 1;
			}
			else if (node.Value != null)
			{
				return -1;
			}
			int min = Math.Min(this.Nodes.Count, node.Nodes.Count);
			for (int i = 0; i < min; i++)
			{
				cmp = this.Nodes[i].CompareTo(node.Nodes[i]);
				if (cmp != 0)
				{
					return cmp;
				}
			}
			return (this.Nodes.Count == node.Nodes.Count) ? 0 : ((this.Nodes.Count > node.Nodes.Count) ? -1 : 1);
		}

	}
}
