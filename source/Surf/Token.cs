//
// AUTOGENERATED 12/24/2008 11:42:14 AM
//
using System;

namespace Surf
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