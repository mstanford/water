//
// AUTOGENERATED 1/1/2009 12:12:19 PM
//
using System;

namespace Ice
{
	public class Token
	{
		public int Type;
		public string Value;

		public Token(int type)
		{
			Type = type;
			Value = null;
		}

		public Token(int type, string value)
		{
			Type = type;
			Value = value;
		}

	}
}