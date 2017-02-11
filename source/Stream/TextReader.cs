//
// AUTOGENERATED 12/30/2008 4:01:13 PM
//
using System;

namespace Stream
{
	public class TextReader
	{
		private System.IO.TextReader _reader;

		public int Line = 1;
		public int Column = 1;

		public TextReader(System.IO.TextReader reader)
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
