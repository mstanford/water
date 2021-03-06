//
// AUTOGENERATED 1/21/2009 8:31:01 PM
//
using System;

namespace Bamboo.CSharp.Parser
{
	public class CSharpTextReader
	{
		private System.IO.TextReader _reader;

		public int Line = 1;
		public int Column = 1;

		public CSharpTextReader(System.IO.TextReader reader)
		{
			this._reader = reader;
		}

		public int Peek()
		{
			return this._reader.Peek();
		}

		public int Read()
		{
			char ch = (char)this._reader.Read();
			if(ch == '\n')
			{
				Line++;
				Column = 1;
			}
			return ch;
		}

	}
}
