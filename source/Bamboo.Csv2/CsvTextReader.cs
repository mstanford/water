//
// AUTOGENERATED 7/17/2009 5:09:41 PM
//
using System;

namespace Bamboo.Csv2
{
	public class CsvTextReader
	{
		private System.IO.TextReader _reader;

		public int Line = 1;
		public int Column = 1;

		public CsvTextReader(System.IO.TextReader reader)
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

		public void Close()
		{
			this._reader.Close();
		}

	}
}
