// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace Bamboo.Parsing.Generators.CSharp
{
	public class NodeGenerator
	{

		public static void Generate(string name, string nspace, Bamboo.Parsing.Grammars.Grammar grammar, System.IO.TextWriter writer)
		{
			writer.WriteLine("//");
			writer.WriteLine("// AUTOGENERATED " + System.DateTime.Now + "");
			writer.WriteLine("//");
			writer.WriteLine("using System;");
			writer.WriteLine("");
			writer.WriteLine("namespace " + nspace + "");
			writer.WriteLine("{");
			writer.WriteLine("	public class " + name + "Node : System.IComparable");
			writer.WriteLine("	{");
			writer.WriteLine("		public int Type;");
			writer.WriteLine("		public string Value;");
			writer.WriteLine("		public System.Collections.Generic.List<" + name + "Node> Nodes = new System.Collections.Generic.List<" + name + "Node>();");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Node(int type)");
			writer.WriteLine("		{");
			writer.WriteLine("			Type = type;");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("		public " + name + "Node(int type, string value)");
			writer.WriteLine("		{");
			writer.WriteLine("			Type = type;");
			writer.WriteLine("			Value = value;");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("		public int CompareTo(object obj)");
			writer.WriteLine("		{");
			writer.WriteLine("			" + name + "Node node = (" + name + "Node)obj;");
			writer.WriteLine("			int cmp = this.Type.CompareTo(node.Type);");
			writer.WriteLine("			if (cmp != 0)");
			writer.WriteLine("			{");
			writer.WriteLine("				return cmp;");
			writer.WriteLine("			}");
			writer.WriteLine("			if (this.Value != null && node.Value != null)");
			writer.WriteLine("			{");
			writer.WriteLine("				cmp = this.Value.CompareTo(node.Value);");
			writer.WriteLine("				if (cmp != 0)");
			writer.WriteLine("				{");
			writer.WriteLine("					return cmp;");
			writer.WriteLine("				}");
			writer.WriteLine("			}");
			writer.WriteLine("			else if (this.Value != null)");
			writer.WriteLine("			{");
			writer.WriteLine("				return 1;");
			writer.WriteLine("			}");
			writer.WriteLine("			else if (node.Value != null)");
			writer.WriteLine("			{");
			writer.WriteLine("				return -1;");
			writer.WriteLine("			}");
			writer.WriteLine("			int min = Math.Min(this.Nodes.Count, node.Nodes.Count);");
			writer.WriteLine("			for (int i = 0; i < min; i++)");
			writer.WriteLine("			{");
			writer.WriteLine("				cmp = this.Nodes[i].CompareTo(node.Nodes[i]);");
			writer.WriteLine("				if (cmp != 0)");
			writer.WriteLine("				{");
			writer.WriteLine("					return cmp;");
			writer.WriteLine("				}");
			writer.WriteLine("			}");
			writer.WriteLine("			return (this.Nodes.Count == node.Nodes.Count) ? 0 : ((this.Nodes.Count > node.Nodes.Count) ? -1 : 1);");
			writer.WriteLine("		}");
			writer.WriteLine("");
			writer.WriteLine("	}");
			writer.WriteLine("}");
		}

		private NodeGenerator()
		{
		}

	}
}
