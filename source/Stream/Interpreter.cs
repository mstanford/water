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

namespace Stream
{
	public class Interpreter : Evaluator
	{
		private Stream.Parser _parser = new Stream.Parser();
		private Stream.Generator _generator = new Stream.Generator();

		private System.Collections.Generic.Dictionary<string, object> _variables = new System.Collections.Generic.Dictionary<string, object>();
		private System.Collections.Generic.Dictionary<string, List<Processor>> _processors = new System.Collections.Generic.Dictionary<string, List<Processor>>();

		public System.IO.TextWriter Output;

		public Interpreter()
		{
		}

		public void Add(string message, Processor processor)
		{
			if (!this._processors.ContainsKey(message))
			{
				this._processors.Add(message, new List<Processor>());
			}
			this._processors[message].Add(processor);
		}

		public void Interpret(System.IO.TextReader input)
		{
			while (input.Peek() != -1)
			{
			    string line = input.ReadLine();  //TODO add newline seperator to grammar
			    if (line.Length > 0)
			    {
					Stream.TextReader reader = new Stream.TextReader(new System.IO.StringReader(line));

			        try
			        {
			            while (reader.Peek() != -1)
			            {
							Stream.Node node = this._parser.Parse(reader);
			                object result = Evaluate(node);
			                if (result != null)
			                {
								throw new System.Exception("Result is not expected.");
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

		protected override object EvaluateIDENTIFIERExpressionTail(Node node)
		{
		//    if (node.Nodes[1].Nodes.Count == 0)
		//    {
		//        return Evaluate(node.Nodes[0]);
		//    }
		//    else if (
		//        node.Nodes[1].Nodes.Count == 2 &&
		//        node.Nodes[1].Nodes[0].Type == NodeType.ASSIGNMENT &&
		//        node.Nodes[1].Nodes[1].Type == NodeType.Expression)
		//    {
		//        string identifier = node.Nodes[0].Value;
		//        Node value = node.Nodes[1].Nodes[1];
		//        Assign(identifier, value);
		//        return Evaluate(value);
		//    }
		//    else if (
		//        node.Nodes[1].Nodes.Count == 2 &&
		//        node.Nodes[1].Nodes[0].Type == NodeType.EQUALS &&
		//        node.Nodes[1].Nodes[1].Type == NodeType.Expression)
		//    {
		//        object a = Evaluate(node.Nodes[0]);
		//        object b = Evaluate(node.Nodes[1].Nodes[1]);
		//        return Apply("equals", ToList(a, b));
		//    }
		//    else 
			if (node.Nodes[1].Nodes.Count == 3 &&
				node.Nodes[1].Nodes[0].Type == NodeType.LEFT_PAREN &&
				node.Nodes[1].Nodes[1].Type == NodeType.ExpressionList &&
				node.Nodes[1].Nodes[2].Type == NodeType.RIGHT_PAREN)
			{
				string identifier = node.Nodes[0].Value;
				System.Collections.ArrayList expressions = (System.Collections.ArrayList)Evaluate(node.Nodes[1].Nodes[1]);
				foreach (Processor processor in this._processors[identifier])
				{
					processor.Process(expressions);
				}
				return null;
			}
			else
		    {
				NodePrinter.Print(node, System.Console.Out);
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

		//protected override object EvaluateCOMMAExpressionExpressionListTail(Node node)
		//{
		//    System.Collections.ArrayList list = new System.Collections.ArrayList();
		//    object expression = Evaluate(node.Nodes[1]);
		//    list.Add(expression);
		//    if (node.Nodes[2].Nodes.Count > 0)
		//    {
		//        System.Collections.ArrayList tail = (System.Collections.ArrayList)Evaluate(node.Nodes[2]);
		//        list.AddRange(tail);
		//    }
		//    return list;
		//}

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

		protected override object EvaluateIDENTIFIER(string value)
		{
			if (!this._variables.ContainsKey(value))
			{
				return null;
			}
			return this._variables[value];
		}

		private static void Assert(bool expression, string message)
		{
			if (!expression)
			{
				throw new System.Exception(message);
			}
		}

	}
}
