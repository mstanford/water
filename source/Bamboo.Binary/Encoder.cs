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
	public abstract class Encoder
	{

		public abstract void EncodeError(System.Exception e, System.IO.Stream stream);
		public abstract void EncodeBoolean(bool b, System.IO.Stream stream);
		public abstract void EncodeByte(byte b, System.IO.Stream stream);
		public abstract void EncodeSByte(sbyte s, System.IO.Stream stream);
		public abstract void EncodeChar(char c, System.IO.Stream stream);
		public abstract void EncodeInt16(short s, System.IO.Stream stream);
		public abstract void EncodeUInt16(ushort u, System.IO.Stream stream);
		public abstract void EncodeInt32(int i, System.IO.Stream stream);
		public abstract void EncodeUInt32(uint u, System.IO.Stream stream);
		public abstract void EncodeSingle(float f, System.IO.Stream stream);
		public abstract void EncodeInt64(long l, System.IO.Stream stream);
		public abstract void EncodeUInt64(ulong u, System.IO.Stream stream);
		public abstract void EncodeDouble(double d, System.IO.Stream stream);
		public abstract void EncodeDecimal(decimal d, System.IO.Stream stream);
		public abstract void EncodeString(string s, System.IO.Stream stream);
		public abstract void EncodeDate(System.DateTime d, System.IO.Stream stream);
		public abstract void EncodeTime(System.DateTime t, System.IO.Stream stream);
		public abstract void EncodeDateTime(DateTime d, System.IO.Stream stream);

	}
}
