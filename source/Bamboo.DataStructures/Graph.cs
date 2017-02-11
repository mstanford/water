// ------------------------------------------------------------------------------
// 
// Copyright (c) 2009 Swampware, Inc.
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Bamboo.DataStructures
{
	public class Graph<T>
	{
		private List<Node<T>> _nodes = new List<Node<T>>();

		public Graph()
		{
		}

		public Node<T> this[T label]
		{
			get
			{
				foreach (Node<T> node in this._nodes)
				{
					if (node.Label.Equals(label))
					{
						return node;
					}
				}
				return null;
			}
		}

		public List<Node<T>> Nodes
		{
			get { return this._nodes; }
		}

		public void Add(T nodeValue)
		{
			Node<T> node = this[nodeValue];
			if (node == null)
			{
				node = new Node<T>(nodeValue);
				this._nodes.Add(node);
			}
		}

		public void Add(T nodeValue, T edgeValue)
		{
			Node<T> node = this[nodeValue];
			if (node == null)
			{
				node = new Node<T>(nodeValue);
				this._nodes.Add(node);
			}
			if (!node[edgeValue])
			{
				node.Edges.Add(edgeValue);
			}
		}

		public static Graph<T> Copy(Graph<T> graph)
		{
			Graph<T> graph2 = new Graph<T>();
			foreach (Node<T> node in graph.Nodes)
			{
				graph2.Nodes.Add(Copy(node));
			}
			return graph2;
		}

		private static Node<T> Copy(Node<T> node)
		{
			Node<T> node2 = new Node<T>(node.Label);
			foreach (T edge in node.Edges)
			{
				node2.Edges.Add(edge);
			}
			return node2;
		}

		public static List<T> Sort(Graph<T> graph)
		{
			graph = Copy(graph);

			List<T> L = new List<T>();
			List<Node<T>> S = new List<Node<T>>();
			for (int i = (graph.Nodes.Count - 1); i >= 0; i--)
			{
				if (graph.Nodes[i].Edges.Count == 0)
				{
					S.Add(graph.Nodes[i]);
					graph.Nodes.RemoveAt(i);
				}
			}
			while (S.Count > 0)
			{
				Node<T> n = S[S.Count - 1];
				S.RemoveAt(S.Count - 1);
				L.Add(n.Label);
				for (int i = (graph.Nodes.Count - 1); i >= 0; i--)
				{
					Node<T> m = graph.Nodes[i];
					for (int j = (m.Edges.Count - 1); j >= 0; j--)
					{
						T e = m.Edges[j];
						if (e.Equals(n.Label))
						{
							m.Edges.RemoveAt(j);
						}
					}
					if (m.Edges.Count == 0)
					{
						S.Add(m);
						graph.Nodes.RemoveAt(i);
					}
				}
			}
			if (graph.Nodes.Count > 0)
			{
				throw new System.Exception("Graph has at least one cycle.");
			}
			return L;
		}

		public class Node<U>
		{
			private U _label;
			private List<U> _edges = new List<U>();

			public Node(U label)
			{
				this._label = label;
			}

			public bool this[U label]
			{
				get
				{
					foreach (U edge in this._edges)
					{
						if (edge.Equals(label))
						{
							return true;
						}
					}
					return false;
				}
			}

			public U Label
			{
				get { return this._label; }
			}

			public List<U> Edges
			{
				get { return this._edges; }
			}

			public void Add(U edgeValue)
			{
				if (!this[edgeValue])
				{
					this._edges.Add(edgeValue);
				}
			}

		}

	}
}
