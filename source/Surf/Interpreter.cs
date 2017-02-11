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

namespace Surf
{
	public class Interpreter : Evaluator
	{
		private Surf.Parser _parser = new Surf.Parser();
		private Surf.Generator _generator = new Surf.Generator();

		private Surf.Set _definitions = new Surf.Set();
		private Surf.Set _variables = new Surf.Set();
		private Surf.Set _dependencies = new Surf.Set();

		public System.IO.TextWriter Output;
		public System.IO.TextWriter Trace;

		public Interpreter()
		{
		}

		public void Interpret(System.IO.TextReader input)
		{
			while (input.Peek() != -1)
			{
				string line = input.ReadLine();  //TODO add newline seperator to grammar
				if (line.Length > 0)
				{
					Surf.TextReader reader = new Surf.TextReader(new System.IO.StringReader(line));

					try
					{
						while (reader.Peek() != -1)
						{
							Surf.Node node = this._parser.Parse(reader);
							if (this.Trace != null)
							{
								this.Trace.Write(">");
								this._generator.Generate(node, this.Trace);
								this.Trace.WriteLine();
							}
							object result = Evaluate(node);
							if (result != null && this.Output != null)
							{
								Surf.Printer.Print(result, this.Output);
								this.Output.WriteLine();
							}
							if (this.Trace != null)
							{
								this.Trace.WriteLine();
							}
						}
					}
					catch (System.Exception exception)
					{
						this.Output.WriteLine("ERROR: " + exception.Message + "\n" + exception.StackTrace);
						this.Output.WriteLine();
					}
				}
			}
		}

		#region Evaluate

		protected override object EvaluateIDENTIFIERExpressionTail(Node node)
		{
			if (node.Nodes[1].Nodes.Count == 0)
			{
				return Evaluate(node.Nodes[0]);
			}
			else if (
				node.Nodes[1].Nodes.Count == 2 &&
				node.Nodes[1].Nodes[0].Type == NodeType.ASSIGNMENT &&
				node.Nodes[1].Nodes[1].Type == NodeType.Expression)
			{
				string identifier = node.Nodes[0].Value;
				Node value = node.Nodes[1].Nodes[1];
				Assign(identifier, value);
				return Evaluate(value);
			}
			else if (
				node.Nodes[1].Nodes.Count == 2 &&
				node.Nodes[1].Nodes[0].Type == NodeType.UNION &&
				node.Nodes[1].Nodes[1].Type == NodeType.Expression)
			{
				object a = Evaluate(node.Nodes[0]);
				object b = Evaluate(node.Nodes[1].Nodes[1]);
				return Apply("union", ToList(a, b));
			}
			else if (
				node.Nodes[1].Nodes.Count == 2 &&
				node.Nodes[1].Nodes[0].Type == NodeType.INTERSECTION &&
				node.Nodes[1].Nodes[1].Type == NodeType.Expression)
			{
				object a = (object)Evaluate(node.Nodes[0]);
				object b = (object)Evaluate(node.Nodes[1].Nodes[1]);
				return Apply("intersection", ToList(a, b));
			}
			else if (
				node.Nodes[1].Nodes.Count == 2 &&
				node.Nodes[1].Nodes[0].Type == NodeType.DIFFERENCE &&
				node.Nodes[1].Nodes[1].Type == NodeType.Expression)
			{
				object a = (object)Evaluate(node.Nodes[0]);
				object b = (object)Evaluate(node.Nodes[1].Nodes[1]);
				return Apply("difference", ToList(a, b));
			}
			else if (
				node.Nodes[1].Nodes.Count == 2 &&
				node.Nodes[1].Nodes[0].Type == NodeType.EQUALS &&
				node.Nodes[1].Nodes[1].Type == NodeType.Expression)
			{
				object a = Evaluate(node.Nodes[0]);
				object b = Evaluate(node.Nodes[1].Nodes[1]);
				return Apply("equals", ToList(a, b));
			}
			else if (
				node.Nodes[1].Nodes.Count == 3 &&
				node.Nodes[1].Nodes[0].Type == NodeType.LEFT_PAREN &&
				node.Nodes[1].Nodes[1].Type == NodeType.ExpressionList &&
				node.Nodes[1].Nodes[2].Type == NodeType.RIGHT_PAREN)
			{
				string identifier = node.Nodes[0].Value;
				System.Collections.ArrayList expressions = (System.Collections.ArrayList)Evaluate(node.Nodes[1].Nodes[1]);

				return Apply(identifier, expressions);
			}
			else
			{
				throw new System.Exception("Invalid expression.");
			}
		}

		protected override object EvaluateExpressionExpressionListTail(Node node)
		{
			object expression = Evaluate(node.Nodes[0]);
			if (node.Nodes[1].Nodes.Count > 0)
			{
				System.Collections.ArrayList list = (System.Collections.ArrayList)Evaluate(node.Nodes[1]);
				list.Insert(0, expression);
				return list;
			}
			else
			{
				System.Collections.ArrayList list = new System.Collections.ArrayList();
				list.Add(expression);
				return list;
			}
		}

		protected override object EvaluateCOMMAExpressionExpressionListTail(Node node)
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			object expression = Evaluate(node.Nodes[1]);
			list.Add(expression);
			if (node.Nodes[2].Nodes.Count > 0)
			{
				System.Collections.ArrayList tail = (System.Collections.ArrayList)Evaluate(node.Nodes[2]);
				list.AddRange(tail);
			}
			return list;
		}

		protected override object EvaluateSet(Node node)
		{
			Surf.Set set = new Surf.Set();
			System.Collections.ArrayList items = (System.Collections.ArrayList)Evaluate(node.Nodes[1]);
			foreach (object item in items)
			{
				set.Add(item);
			}
			return set;
		}

		protected override object EvaluateTuple(Node node)
		{
			Surf.Tuple tuple = new Surf.Tuple();
			System.Collections.ArrayList items = (System.Collections.ArrayList)Evaluate(node.Nodes[1]);
			foreach (object item in items)
			{
				tuple.Add(item);
			}
			return tuple;
		}

		protected override object EvaluateBOOLEAN(string value)
		{
			Assert(value.Equals("true") || value.Equals("false"), "Invalid boolean.");
			return Boolean.Parse(value);
		}

		protected override object EvaluateINTEGER(string value)
		{
			return Int32.Parse(value);
		}

		protected override object EvaluateFLOAT(string value)
		{
			return Double.Parse(value);
		}

		protected override object EvaluateCHARACTER(string value)
		{
			return Char.Parse(value.Substring(1, value.Length - 2));
		}

		protected override object EvaluateSTRING(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		protected override object EvaluateBITMAP(string value)
		{
			value = value.Substring(1, value.Length - 2);
			Surf.Bitmap bitArray = new Surf.Bitmap(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				bitArray.Set(i, (value[i] == '1'));
			}
			return bitArray;
		}

		protected override object EvaluateIDENTIFIER(string value)
		{
			if (!this._variables.IsDefined(value))
			{
				return null;
			}
			return this._variables.Apply(value);
		}

		#endregion

		private object Apply(string identifier, System.Collections.ArrayList expressions)
		{
			#region Apply

			switch (identifier)
			{
				case "variables":
					{
						Assert(expressions.Count == 0, "Invalid arguments.");
						return this._variables.Domain();
					}
				case "definitions":
					{
						Assert(expressions.Count == 0, "Invalid arguments.");
						return this._definitions;
					}
				case "dependencies":
					{
						Assert(expressions.Count == 0, "Invalid arguments.");
						return this._dependencies;
					}
				case "equals":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						object a = expressions[0];
						object b = expressions[1];
						return a.Equals(b);
					}
				case "union":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						if (expressions[0] == null || expressions[1] == null)
						{
							return null;
						}
						else if (expressions[0] is Surf.Set && expressions[1] is Surf.Set)
						{
							Set a = (Set)expressions[0];
							Set b = (Set)expressions[1];
							return a.Union(b);
						}
						else if (expressions[0] is Surf.Bitmap && expressions[1] is Surf.Bitmap)
						{
							Surf.Bitmap a = (Surf.Bitmap)expressions[0];
							Surf.Bitmap b = (Surf.Bitmap)expressions[1];
							return a.Or(b);
						}
						else
						{
							throw new System.Exception("Invalid expression.");
						}
					}
				case "intersection":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						if (expressions[0] == null || expressions[1] == null)
						{
							return null;
						}
						else if (expressions[0] is Surf.Set && expressions[1] is Surf.Set)
						{
							Set a = (Set)expressions[0];
							Set b = (Set)expressions[1];
							return a.Intersection(b);
						}
						else if (expressions[0] is Surf.Bitmap && expressions[1] is Surf.Bitmap)
						{
							Surf.Bitmap a = (Surf.Bitmap)expressions[0];
							Surf.Bitmap b = (Surf.Bitmap)expressions[1];
							return a.And(b);
						}
						else
						{
							throw new System.Exception("Invalid expression.");
						}
					}
				case "difference":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						if (expressions[0] == null || expressions[1] == null)
						{
							return null;
						}
						else if (expressions[0] is Surf.Set && expressions[1] is Surf.Set)
						{
							Set a = (Set)expressions[0];
							Set b = (Set)expressions[1];
							return a.Difference(b);
						}
						else if (expressions[0] is Surf.Bitmap && expressions[1] is Surf.Bitmap)
						{
							Surf.Bitmap a = (Surf.Bitmap)expressions[0];
							Surf.Bitmap b = (Surf.Bitmap)expressions[1];
							return a.And(b.Not());
						}
						else
						{
							throw new System.Exception("Invalid expression.");
						}
					}
				case "cartesian_product":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						Set a = (Set)expressions[0];
						Set b = (Set)expressions[1];
						if (a == null || b == null)
						{
							return null;
						}
						return a.CartesianProduct(b);
					}
				case "domain":
					{
						Assert(expressions.Count == 1, "Invalid arguments.");

						Set a = (Set)expressions[0];
						if (a == null)
						{
							return null;
						}
						return a.Domain();
					}
				case "range":
					{
						Assert(expressions.Count == 1, "Invalid arguments.");

						Set a = (Set)expressions[0];
						if (a == null)
						{
							return null;
						}
						return a.Range();
					}
				case "transpose":
					{
						Assert(expressions.Count == 1, "Invalid arguments.");

						Set a = (Set)expressions[0];
						if (a == null)
						{
							return null;
						}
						return a.Transpose();
					}
				case "compose":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						Set a = (Set)expressions[0];
						Set b = (Set)expressions[1];
						if (a == null || b == null)
						{
							return null;
						}
						return a.Compose(b);
					}
				case "nest":
					{
						Assert(expressions.Count == 1, "Invalid arguments.");

						Set a = (Set)expressions[0];
						if (a == null)
						{
							return null;
						}
						return a.Nest();
					}
				case "unnest":
					{
						Assert(expressions.Count == 1, "Invalid arguments.");

						Set a = (Set)expressions[0];
						if (a == null)
						{
							return null;
						}
						return a.Unnest();
					}
				case "in":
					{
						Assert(expressions.Count == 2, "Invalid arguments.");

						Set S = (Set)expressions[0];
						object key = expressions[1];
						if (S == null || key == null)
						{
							return null;
						}
						return S.IsDefined(key);
					}
				case "method":
					{
						Assert(expressions.Count == 4, "Invalid arguments.");

						string name = (string)expressions[0];
						string type_name = (string)expressions[1];
						string assembly_name = (string)expressions[2];
						string method_name = (string)expressions[3];

						Water.Operators.LoadMethodOperator.LoadMethod(name, type_name, assembly_name, method_name);
						return null;
					}
				default:
					{
						if (Water.Environment.IsConstant(identifier))
						{
							Water.Operator op = (Water.Operator)Water.Environment.Identify(identifier);
							return op.Evaluate(new Water.List(expressions));
						}
						else if (this._variables.IsDefined(identifier))
						{
							Assert(expressions.Count > 0, "Invalid arguments.");
							Set S = (Set)this._variables.Apply(identifier);
							if (expressions.Count == 1)
							{
								return S.Apply(expressions[0]);
							}
							else
							{
								return S.Apply(new Surf.Tuple(expressions));
							}
						}
						else
						{
							throw new System.Exception("Function does not exist.");
						}
					}
			}

			#endregion
		}

		private void Assign(string name, Node node)
		{
			// Set defintion.
			this._definitions.Define(name, node);

			// Set variable.
			object value = Evaluate(node);
			if (value != null)
			{
				this._variables.Define(name, value);
			}

			// Rebuild dependency graph.
			Surf.Set dependencies = Dependencies(node);
			if (dependencies.Count > 0)
			{
				this._dependencies.Define(name, dependencies);
			}

			// Recalculate dependencies.
			this.RecalculateDependencies(name);
		}

		#region Dependencies

		private void RecalculateDependencies(string name)
		{
			Surf.Set dependents = this._dependencies.Unnest().Transpose().Nest();

			Surf.Set recalculationList = new Surf.Set();
			RecalculationList(name, dependents, recalculationList);

			Surf.Set dependencyGraph = new Surf.Set();
			DependencyGraph(this._dependencies, recalculationList, dependencyGraph);

			if (dependencyGraph.Count > 0)
			{
				Surf.Tuple topologicalSort = new Surf.Tuple();
				TopologicalSort(name, dependencyGraph, topologicalSort);

				foreach (string identifier in topologicalSort)
				{
					object value = Evaluate((Node)this._definitions.Apply(identifier));
					if (value != null)
					{
						this._variables.Define(identifier, value);
					}
				}
			}
		}

		private static void RecalculationList(string name, Surf.Set dependents, Surf.Set recalculationList)
		{
			if (recalculationList.Contains(name))
			{
				throw new System.Exception("Cycle detected.");
			}

			recalculationList.Add(name);

			if (dependents.IsDefined(name))
			{
				foreach (string dependent in (Surf.Set)dependents.Apply(name))
				{
					RecalculationList(dependent, dependents, recalculationList);
				}
			}
		}

		private static void DependencyGraph(Surf.Set dependencies, Surf.Set recalculationList, Surf.Set dependencyGraph)
		{
			foreach (string identifier in recalculationList)
			{
				if (dependencies.IsDefined(identifier))
				{
					Surf.Set dependencyList = ((Surf.Set)dependencies.Apply(identifier)).Intersection(recalculationList);
					if (dependencyList.Count > 0)
					{
						dependencyGraph.Define(identifier, dependencyList);
					}
				}
			}
		}

		private static void TopologicalSort(string name, Surf.Set dependencyGraph, Surf.Tuple topologicalSort)
		{
			Surf.Set completed = new Surf.Set();
			completed.Add(name);

			while (dependencyGraph.Count > 0)
			{
				for (int i = 0; i < dependencyGraph.Count; i++)
				{
					Surf.Tuple tuple = (Surf.Tuple)dependencyGraph[i];

					if(((Surf.Set)tuple[1]).Difference(completed).Count == 0)
					{
						topologicalSort.Add(tuple[0]);
						completed.Add(tuple[0]);
						dependencyGraph.Remove(i);
						break;
					}
				}
			}
		}

		private static Surf.Set Dependencies(Node node)
		{
			Surf.Set dependencies = new Surf.Set();
			Dependencies(node, dependencies);
			return dependencies;
		}

		private static void Dependencies(Node node, Surf.Set dependencies)
		{
			if (node.Type == NodeType.IDENTIFIER)
			{
				dependencies.Add(node.Value);
			}
			foreach (Node node2 in node.Nodes)
			{
				Dependencies(node2, dependencies);
			}
		}

		#endregion

		private static void Assert(bool expression, string message)
		{
			if (!expression)
			{
				throw new System.Exception(message);
			}
		}

		private static System.Collections.ArrayList ToList(params object[] items)
		{
			System.Collections.ArrayList list = new System.Collections.ArrayList();
			foreach(object item in items)
			{
				list.Add(item);
			}
			return list;
		}

	}
}
