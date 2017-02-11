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
	public class BinaryEncoder : Encoder
	{

		public BinaryEncoder()
		{
		}

		public override void EncodeError(System.Exception e, System.IO.Stream stream)
		{
			EncodeString(e.Message + "\n" + e.StackTrace, stream);
		}

		public override void EncodeBoolean(bool b, System.IO.Stream stream)
		{
			stream.WriteByte((byte)(b ? 1 : 0));
		}

		public override void EncodeByte(byte b, System.IO.Stream stream)
		{
			stream.WriteByte(b);
		}

		public override void EncodeSByte(sbyte s, System.IO.Stream stream)
		{
			stream.WriteByte((byte)s);
		}

		public override void EncodeChar(char c, System.IO.Stream stream)
		{
            stream.WriteByte((byte)c);
            stream.WriteByte((byte)(c >> 8));
		}

		public override void EncodeInt16(short s, System.IO.Stream stream)
		{
			stream.WriteByte((byte)s);
			stream.WriteByte((byte)(s >> 8));
		}

		public override void EncodeUInt16(ushort u, System.IO.Stream stream)
		{
			stream.WriteByte((byte)u);
			stream.WriteByte((byte)(u >> 8));
		}

		public override void EncodeInt32(int i, System.IO.Stream stream)
		{
			stream.WriteByte((byte)i);
			stream.WriteByte((byte)(i >> 8));
			stream.WriteByte((byte)(i >> 16));
			stream.WriteByte((byte)(i >> 24));
		}

		public override void EncodeUInt32(uint u, System.IO.Stream stream)
		{
			stream.WriteByte((byte)u);
			stream.WriteByte((byte)(u >> 8));
			stream.WriteByte((byte)(u >> 16));
			stream.WriteByte((byte)(u >> 24));
		}

		public override unsafe void EncodeSingle(float f, System.IO.Stream stream)
		{
			uint u = *(uint*)&f;
			stream.WriteByte((byte)u);
			stream.WriteByte((byte)(u >> 8));
			stream.WriteByte((byte)(u >> 16));
			stream.WriteByte((byte)(u >> 24));
		}

		public override void EncodeInt64(long l, System.IO.Stream stream)
		{
			stream.WriteByte((byte)l);
			stream.WriteByte((byte)(l >> 8));
			stream.WriteByte((byte)(l >> 16));
			stream.WriteByte((byte)(l >> 24));
			stream.WriteByte((byte)(l >> 32));
			stream.WriteByte((byte)(l >> 40));
			stream.WriteByte((byte)(l >> 48));
			stream.WriteByte((byte)(l >> 56));
		}

		public override void EncodeUInt64(ulong u, System.IO.Stream stream)
		{
			stream.WriteByte((byte)u);
			stream.WriteByte((byte)(u >> 8));
			stream.WriteByte((byte)(u >> 16));
			stream.WriteByte((byte)(u >> 24));
			stream.WriteByte((byte)(u >> 32));
			stream.WriteByte((byte)(u >> 40));
			stream.WriteByte((byte)(u >> 48));
			stream.WriteByte((byte)(u >> 56));
		}

		public override unsafe void EncodeDouble(double d, System.IO.Stream stream)
		{
			ulong u  = *(ulong*)&d;
			stream.WriteByte((byte)u);
			stream.WriteByte((byte)(u >> 8));
			stream.WriteByte((byte)(u >> 16));
			stream.WriteByte((byte)(u >> 24));
			stream.WriteByte((byte)(u >> 32));
			stream.WriteByte((byte)(u >> 40));
			stream.WriteByte((byte)(u >> 48));
			stream.WriteByte((byte)(u >> 56));
		}

		public override void EncodeDecimal(decimal d, System.IO.Stream stream)
		{
			int[] an = Decimal.GetBits(d);
			EncodeInt32(an[0], stream);
			EncodeInt32(an[1], stream);
			EncodeInt32(an[2], stream);
			EncodeInt32(an[3], stream);
		}

		public override void EncodeString(string s, System.IO.Stream stream)
		{
			EncodeUInt32((uint)s.Length, stream);
			for (int i = 0; i < s.Length; i++)
			{
				EncodeChar(s[i], stream);
			}
		}

		public override void EncodeDate(System.DateTime d, System.IO.Stream stream)
		{
			EncodeInt32(d.Year, stream);
			EncodeInt32(d.Month, stream);
			EncodeInt32(d.Day, stream);
		}

		public override void EncodeTime(System.DateTime t, System.IO.Stream stream)
		{
			EncodeInt32(t.Hour, stream);
			EncodeInt32(t.Minute, stream);
			EncodeInt32(t.Second, stream);
			EncodeInt32(t.Millisecond, stream);
		}

		public override void EncodeDateTime(DateTime d, System.IO.Stream stream)
		{
			EncodeInt32(d.Year, stream);
			EncodeInt32(d.Month, stream);
			EncodeInt32(d.Day, stream);
			EncodeInt32(d.Hour, stream);
			EncodeInt32(d.Minute, stream);
			EncodeInt32(d.Second, stream);
			EncodeInt32(d.Millisecond, stream);
		}

	}
}
