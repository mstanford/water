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
	public class BinaryDecoder : Decoder
	{

		public BinaryDecoder()
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
			return (char)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8));
		}

		public override short DecodeInt16(System.IO.Stream stream)
		{
			return (short)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8));
		}

		public override ushort DecodeUInt16(System.IO.Stream stream)
		{
			return (ushort)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8));
		}

		public override int DecodeInt32(System.IO.Stream stream)
		{
			return (int)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));
		}

		public override uint DecodeUInt32(System.IO.Stream stream)
		{
			return (uint)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));
		}

		public override unsafe float DecodeSingle(System.IO.Stream stream)
		{
			uint u = DecodeUInt32(stream);
			return *((float*)&u);
		}

		public override long DecodeInt64(System.IO.Stream stream)
		{
			uint lo = (uint)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));

			uint hi = (uint)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));

			return (long)((ulong)hi) << 32 | lo;
		}

		public override ulong DecodeUInt64(System.IO.Stream stream)
		{
			uint lo = (uint)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));

			uint hi = (uint)(
				(byte)stream.ReadByte() |
				((byte)stream.ReadByte() << 8) |
				((byte)stream.ReadByte() << 16) |
				((byte)stream.ReadByte() << 24));

			return ((ulong)hi) << 32 | lo;
		}

		public override unsafe double DecodeDouble(System.IO.Stream stream)
		{
			ulong u = DecodeUInt64(stream);
			return *((double*)&u);
		}

		public override decimal DecodeDecimal(System.IO.Stream stream)
		{
			int[] bits = new int[4];
			bits[0] = DecodeInt32(stream);
			bits[1] = DecodeInt32(stream);
			bits[2] = DecodeInt32(stream);
			bits[3] = DecodeInt32(stream);
			return new Decimal(bits);
		}

		public override string DecodeString(System.IO.Stream stream)
		{
			uint length = DecodeUInt32(stream);
			char[] ach = new char[length];
			for (int i = 0; i < length; i++)
			{
				ach[i] = DecodeChar(stream);
			}
			return new string(ach);
		}

		public override System.DateTime DecodeDate(System.IO.Stream stream)
		{
			return new System.DateTime(DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream));
		}

		public override System.DateTime DecodeTime(System.IO.Stream stream)
		{
			return new System.DateTime(0, 0, 0, DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream));
		}

		public override System.DateTime DecodeDateTime(System.IO.Stream stream)
		{
			return new System.DateTime(DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream), DecodeInt32(stream));
		}

	}
}
