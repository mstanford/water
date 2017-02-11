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
	public class PackedEncoder : Encoder
	{

		public PackedEncoder()
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
			EncodeUInt32((uint)c, stream);
		}

		public override void EncodeInt16(short s, System.IO.Stream stream)
		{
			EncodeUInt32((uint)s, stream);
		}

		public override void EncodeUInt16(ushort u, System.IO.Stream stream)
		{
			EncodeUInt32((uint)u, stream);
		}

		public override void EncodeInt32(int i, System.IO.Stream stream)
		{
			EncodeUInt32((uint)i, stream);
		}

		public override void EncodeUInt32(uint u, System.IO.Stream stream)
		{
			// Convert bytes from 8 bits to 7 bits using the 8th bit for a carry flag.
			while (u >= 0x80)
			{
				stream.WriteByte((byte)(u | 0x80));
				u >>= 7;
			}
			stream.WriteByte((byte)u);
		}

		public override unsafe void EncodeSingle(float f, System.IO.Stream stream)
		{
			EncodeUInt32(*(uint*)&f, stream);
		}

		public override void EncodeInt64(long l, System.IO.Stream stream)
		{
			EncodeUInt64((ulong)l, stream);
		}

		public override void EncodeUInt64(ulong u, System.IO.Stream stream)
		{
			// Convert bytes from 8 bits to 7 bits using the 8th bit for a carry flag.
			while (u >= 0x80)
			{
				stream.WriteByte((byte)(u | 0x80));
				u >>= 7;
			}
			stream.WriteByte((byte)u);
		}

		public override unsafe void EncodeDouble(double d, System.IO.Stream stream)
		{
			EncodeUInt64(*(ulong*)&d, stream);
		}

		public override void EncodeDecimal(decimal d, System.IO.Stream stream)
		{
			decimal a = decimal.Truncate(d);
			decimal b = d - a;
			decimal c = b;
			int decimal_places = 0;
			while (c > 0)
			{
				b *= 10;
				c = b - decimal.Truncate(b);
				decimal_places++;
			}

			EncodeUInt32((uint)a, stream);
			stream.WriteByte((byte)decimal_places);
			EncodeUInt32((uint)b, stream);
		}

		public override void EncodeString(string s, System.IO.Stream stream)
		{
			EncodeUInt32((uint)s.Length, stream);

			int temp = 0;
			int position = 0;
			for (int i = 0; i < s.Length; i++)
			{
				byte b = (byte)s[i];
				temp |= (b & 127) << position;
				position += 7;

				while (position > 8)
				{
					stream.WriteByte((byte)temp);
					temp >>= 8;
					position -= 8;
				}
			}
			while (position > 0)
			{
				stream.WriteByte((byte)temp);
				temp >>= 8;
				position -= 8;
			}
		}

		public override void EncodeDate(System.DateTime d, System.IO.Stream stream)
		{
			uint u = (uint)d.Day;
			u |= (uint)d.Month << 6;
			u |= (uint)d.Year << 11;
			EncodeUInt32(u, stream);
		}

		public override void EncodeTime(System.DateTime t, System.IO.Stream stream)
		{
			uint u = (uint)t.Hour;
			u |= (uint)t.Minute << 6;
			u |= (uint)t.Second << 13;
			u |= (uint)t.Millisecond << 20;
			EncodeUInt32(u, stream);
		}

		public override void EncodeDateTime(DateTime d, System.IO.Stream stream)
		{
			uint u = (uint)d.Day;
			u |= (uint)d.Month << 6;
			u |= (uint)d.Year << 11;
			EncodeUInt32(u, stream);

			u = (uint)d.Hour;
			u |= (uint)d.Minute << 6;
			u |= (uint)d.Second << 13;
			u |= (uint)d.Millisecond << 20;
			EncodeUInt32(u, stream);
		}

	}
}
