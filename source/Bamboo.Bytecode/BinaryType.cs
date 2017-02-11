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

namespace Bamboo.Bytecode
{
	public class BinaryType
	{
		public const byte NULL = 0;

		public const byte BOOL = 1;
		public const byte BOOL_ARRAY = 2;
		public const byte BOOL_LIST = 3;

		public const byte BYTE = 4;
		public const byte BYTE_ARRAY = 5;
		public const byte BYTE_LIST = 6;

		public const byte SBYTE = 7;
		public const byte SBYTE_ARRAY = 8;
		public const byte SBYTE_LIST = 9;

		public const byte CHAR = 10;
		public const byte CHAR_ARRAY = 11;
		public const byte CHAR_LIST = 12;

		public const byte SHORT = 13;
		public const byte SHORT_ARRAY = 14;
		public const byte SHORT_LIST = 15;

		public const byte USHORT = 16;
		public const byte USHORT_ARRAY = 17;
		public const byte USHORT_LIST = 18;

		public const byte INT = 19;
		public const byte INT_ARRAY = 20;
		public const byte INT_LIST = 21;

		public const byte UINT = 22;
		public const byte UINT_ARRAY = 23;
		public const byte UINT_LIST = 24;

		public const byte LONG = 25;
		public const byte LONG_ARRAY = 26;
		public const byte LONG_LIST = 27;

		public const byte ULONG = 28;
		public const byte ULONG_ARRAY = 29;
		public const byte ULONG_LIST = 30;

		public const byte DECIMAL = 31;
		public const byte DECIMAL_ARRAY = 32;
		public const byte DECIMAL_LIST = 33;

		public const byte FLOAT = 34;
		public const byte FLOAT_ARRAY = 35;
		public const byte FLOAT_LIST = 36;

		public const byte DOUBLE = 37;
		public const byte DOUBLE_ARRAY = 38;
		public const byte DOUBLE_LIST = 39;

		public const byte STRING = 40;
		public const byte STRING_ARRAY = 41;
		public const byte STRING_LIST = 42;

		public const byte DATETIME = 43;
		public const byte DATETIME_ARRAY = 44;
		public const byte DATETIME_LIST = 45;

		public const byte DICTIONARY = 46;

		public const byte LIST = 47;

		/*
		public const byte NULL    = 0;  // 000 00000
		public const byte ERROR   = 1;  // 000 00001

		public const byte BOOLEAN   = 2;  // 000 00010
		public const byte BYTE      = 3;  // 000 00011
		public const byte SBYTE     = 4;  // 000 00100

		public const byte CHAR     = 5;  // 000 00101
		public const byte INT16    = 6;  // 000 00110
		public const byte UINT16   = 7;  // 000 00111

		public const byte INT32    = 8;   // 000 01000
		public const byte UINT32   = 9;   // 000 01001
		public const byte SINGLE   = 10;  // 000 01010

		public const byte INT64    = 11;  // 000 01011
		public const byte UINT64   = 12;  // 000 01100
		public const byte DOUBLE   = 13;  // 000 01101

		public const byte INT128    = 14;  // 000 01110
		public const byte UINT128   = 15;  // 000 01111
		public const byte QUAD      = 16;  // 000 10000

		public const byte DECIMAL    = 17;  // 000 10001
		public const byte COMPLEX    = 18;  // 000 10010
		public const byte STRING     = 19;  // 000 10011
		public const byte DATE       = 20;  // 000 10100
		public const byte TIME       = 21;  // 000 10101
		public const byte DATETIME   = 22;  // 000 10110
		public const byte TUPLE      = 23;  // 000 10111
		public const byte TABLE      = 24;  // 000 11000

		// 000 11001
		// 000 11010
		// 000 11011
		// 000 11100
		// 000 11101
		// 000 11110
		// 000 11111

		public const byte ARRAY               = 32;   // 001 00000
		public const byte ARRAY2D             = 64;   // 010 00000
		public const byte ARRAY3D             = 96;   // 011 00000
		public const byte GRAPH               = 128;  // 100 00000
		public const byte STRING_DICTIONARY   = 160;  // 101 00000
		public const byte INT32_DICTIONARY    = 192;  // 110 00000
		public const byte OVERFLOW            = 224;  // 111 00000
		*/

	}
}
