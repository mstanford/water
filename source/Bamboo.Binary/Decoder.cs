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

namespace Bamboo.Binary
{
	public abstract class Decoder
	{

		public abstract System.Exception DecodeError(System.IO.Stream stream);
		public abstract bool DecodeBoolean(System.IO.Stream stream);
		public abstract byte DecodeByte(System.IO.Stream stream);
		public abstract sbyte DecodeSByte(System.IO.Stream stream);
		public abstract char DecodeChar(System.IO.Stream stream);
		public abstract short DecodeInt16(System.IO.Stream stream);
		public abstract ushort DecodeUInt16(System.IO.Stream stream);
		public abstract int DecodeInt32(System.IO.Stream stream);
		public abstract uint DecodeUInt32(System.IO.Stream stream);
		public abstract float DecodeSingle(System.IO.Stream stream);
		public abstract long DecodeInt64(System.IO.Stream stream);
		public abstract ulong DecodeUInt64(System.IO.Stream stream);
		public abstract double DecodeDouble(System.IO.Stream stream);
		public abstract decimal DecodeDecimal(System.IO.Stream stream);
		public abstract string DecodeString(System.IO.Stream stream);
		public abstract System.DateTime DecodeDate(System.IO.Stream stream);
		public abstract System.DateTime DecodeTime(System.IO.Stream stream);
		public abstract System.DateTime DecodeDateTime(System.IO.Stream stream);

	}
}
