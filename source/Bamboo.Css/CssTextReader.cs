//
// AUTOGENERATED 2/26/2009 12:33:14 PM
//
using System;

namespace Bamboo.Css
{
	public class CssTextReader
	{
		private System.IO.TextReader _reader;

		public int Line = 1;
		public int Column = 1;

		public CssTextReader(System.IO.TextReader reader)
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
