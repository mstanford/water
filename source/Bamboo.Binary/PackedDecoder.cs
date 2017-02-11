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
	public class PackedDecoder : Decoder
	{

		public PackedDecoder()
		{
		}

		public override System.Exception DecodeError(System.IO.Stream stream)
		{
			return new System.Exception(DecodeString(stream));
		}

		public override bool DecodeBoolean(System.IO.Stream stream)
		{
			return (((byte)stream.ReadByte() & 1) == 1);
		}

		public override byte DecodeByte(System.IO.Stream stream)
		{
			return (byte)stream.ReadByte();
		}

		public override sbyte DecodeSByte(System.IO.Stream stream)
		{
			return (sbyte)stream.ReadByte();
		}

		public override char DecodeChar(System.IO.Stream stream)
		{
			return (char)DecodeInt32(stream);
		}

		public override short DecodeInt16(System.IO.Stream stream)
		{
			return (short)DecodeInt32(stream);
		}

		public override ushort DecodeUInt16(System.IO.Stream stream)
		{
			return (ushort)DecodeInt32(stream);
		}

		public override int DecodeInt32(System.IO.Stream stream)
		{
			int i = 0;
			int shift = 0;
			byte b;
			do
			{
				b = (byte)stream.ReadByte();
				i |= (b & 0x7F) << shift;
				shift += 7;
			} while ((b & 0x80) != 0);
			return i;
		}

		public override uint DecodeUInt32(System.IO.Stream stream)
		{
			return (uint)DecodeInt32(stream);
		}

		public override unsafe float DecodeSingle(System.IO.Stream stream)
		{
			uint u = DecodeUInt32(stream);
			return *((float*)&u);
		}

		public override long DecodeInt64(System.IO.Stream stream)
		{
			long l = 0;
			int shift = 0;
			byte b;
			do
			{
				b = (byte)stream.ReadByte();
				l |= (long)(b & 0x7F) << shift;
				shift += 7;
			} while ((b & 0x80) != 0);
			return l;
		}

		public override ulong DecodeUInt64(System.IO.Stream stream)
		{
			return (ulong)DecodeInt64(stream);
		}

		public override unsafe double DecodeDouble(System.IO.Stream stream)
		{
			ulong u = DecodeUInt64(stream);
			return *((double*)&u);
		}

		public override decimal DecodeDecimal(System.IO.Stream stream)
		{
			decimal d = DecodeInt32(stream);
			int decimal_places = DecodeByte(stream);
			decimal b = DecodeInt32(stream);
			for (int i = 0; i < decimal_places; i++)
			{
				b /= 10;
			}
			return d + b;
		}

		public override string DecodeString(System.IO.Stream stream)
		{
			uint length = DecodeUInt32(stream);

			uint numBytes = (uint)Math.Ceiling(length * 0.875);

			char[] ach = new char[length];
			uint index = 0;

			int temp = 0;
			int position = 0;
			for (int i = 0; i < numBytes; i++)
			{
				byte b = (byte)stream.ReadByte();
				temp |= (b & 255) << position;
				position += 8;

				while (position > 7)
				{
					ach[index] = (char)(temp & 127);
					index++;
					temp >>= 7;
					position -= 7;
				}
			}
			while (index < length)
			{
				ach[index] = (char)(temp & 127);
				index++;
				temp >>= 7;
				position -= 7;
			}

			return new string(ach);
		}

		public override System.DateTime DecodeDate(System.IO.Stream stream)
		{
			uint u = DecodeUInt32(stream);
			uint y = u >> 11;
			uint m = (u >> 6) & 31;
			uint d = u & 63;
			return new System.DateTime((int)y, (int)m, (int)d);
		}

		public override System.DateTime DecodeTime(System.IO.Stream stream)
		{
			uint u = DecodeUInt32(stream);
			uint mmm = u >> 20;
			uint ss = (u >> 13) & 127;
			uint mm = (u >> 6) & 127;
			uint hh = u & 63;
			return new System.DateTime(0, 0, 0, (int)hh, (int)mm, (int)ss, (int)mmm);
		}

		public override System.DateTime DecodeDateTime(System.IO.Stream stream)
		{
			uint u = DecodeUInt32(stream);
			uint y = u >> 11;
			uint m = (u >> 6) & 31;
			uint d = u & 63;

			u = DecodeUInt32(stream);
			uint mmm = u >> 20;
			uint ss = (u >> 13) & 127;
			uint mm = (u >> 6) & 127;
			uint hh = u & 63;

			return new System.DateTime((int)y, (int)m, (int)d, (int)hh, (int)mm, (int)ss, (int)mmm);
		}

	}
}
