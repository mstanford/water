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
	public class BinaryReader
	{
		private System.IO.BinaryReader _reader;
		private byte _type;

		public BinaryReader(System.IO.Stream stream)
			: this(new System.IO.BinaryReader(stream))
		{
		}

		public BinaryReader(System.IO.BinaryReader reader)
		{
			if (!System.BitConverter.IsLittleEndian)
				throw new System.Exception("Architecture is not little endian.");

			this._reader = reader;
		}

		public byte Type
		{
			get { return this._type; }
		}

		public object Read()
		{
			this._type = this._reader.ReadByte();

			switch(this._type)
			{
				case BinaryType.NULL:
					{
						return null;
					}
				case BinaryType.BOOL:
					{
						bool b = this._reader.ReadBoolean();
						return b;
					}
				case BinaryType.BOOL_ARRAY:
					{
						int length = this._reader.ReadInt32();
						bool[] ab = new bool[length];
						for (int i = 0; i < length; i++)
						{
							ab[i] = this._reader.ReadBoolean();
						}
						return ab;
					}
				case BinaryType.BOOL_LIST:
					{
						int count = this._reader.ReadInt32();
						List<bool> lb = new List<bool>(count);
						for (int i = 0; i < count; i++)
						{
							lb[i] = this._reader.ReadBoolean();
						}
						return lb;
					}
				case BinaryType.BYTE:
					{
						byte b = this._reader.ReadByte();
						return b;
					}
				case BinaryType.BYTE_ARRAY:
					{
						int length = this._reader.ReadInt32();
						byte[] ab = new byte[length];
						for (int i = 0; i < length; i++)
						{
							ab[i] = this._reader.ReadByte();
						}
						return ab;
					}
				case BinaryType.BYTE_LIST:
					{
						int count = this._reader.ReadInt32();
						List<byte> lb = new List<byte>(count);
						for (int i = 0; i < count; i++)
						{
							lb[i] = this._reader.ReadByte();
						}
						return lb;
					}
				case BinaryType.SBYTE:
					{
						sbyte sb = this._reader.ReadSByte();
						return sb;
					}
				case BinaryType.SBYTE_ARRAY:
					{
						int length = this._reader.ReadInt32();
						sbyte[] asb = new sbyte[length];
						for (int i = 0; i < length; i++)
						{
							asb[i] = this._reader.ReadSByte();
						}
						return asb;
					}
				case BinaryType.SBYTE_LIST:
					{
						int count = this._reader.ReadInt32();
						List<sbyte> lsb = new List<sbyte>(count);
						for (int i = 0; i < count; i++)
						{
							lsb[i] = this._reader.ReadSByte();
						}
						return lsb;
					}
				case BinaryType.CHAR:
					{
						char ch = this._reader.ReadChar();
						return ch;
					}
				case BinaryType.CHAR_ARRAY:
					{
						int length = this._reader.ReadInt32();
						char[] ach = new char[length];
						for (int i = 0; i < length; i++)
						{
							ach[i] = this._reader.ReadChar();
						}
						return ach;
					}
				case BinaryType.CHAR_LIST:
					{
						int count = this._reader.ReadInt32();
						List<char> lch = new List<char>(count);
						for (int i = 0; i < count; i++)
						{
							lch[i] = this._reader.ReadChar();
						}
						return lch;
					}
				case BinaryType.SHORT:
					{
						short sh = this._reader.ReadInt16();
						return sh;
					}
				case BinaryType.SHORT_ARRAY:
					{
						int length = this._reader.ReadInt32();
						short[] ash = new short[length];
						for (int i = 0; i < length; i++)
						{
							ash[i] = this._reader.ReadInt16();
						}
						return ash;
					}
				case BinaryType.SHORT_LIST:
					{
						int count = this._reader.ReadInt32();
						List<short> lsh = new List<short>(count);
						for (int i = 0; i < count; i++)
						{
							lsh[i] = this._reader.ReadInt16();
						}
						return lsh;
					}
				case BinaryType.USHORT:
					{
						ushort us = this._reader.ReadUInt16();
						return us;
					}
				case BinaryType.USHORT_ARRAY:
					{
						int length = this._reader.ReadInt32();
						ushort[] aus = new ushort[length];
						for (int i = 0; i < length; i++)
						{
							aus[i] = this._reader.ReadUInt16();
						}
						return aus;
					}
				case BinaryType.USHORT_LIST:
					{
						int count = this._reader.ReadInt32();
						List<ushort> lus = new List<ushort>(count);
						for (int i = 0; i < count; i++)
						{
							lus[i] = this._reader.ReadUInt16();
						}
						return lus;
					}
				case BinaryType.INT:
					{
						int n = this._reader.ReadInt32();
						return n;
					}
				case BinaryType.INT_ARRAY:
					{
						int length = this._reader.ReadInt32();
						int[] an = new int[length];
						for (int i = 0; i < length; i++)
						{
							an[i] = this._reader.ReadInt32();
						}
						return an;
					}
				case BinaryType.INT_LIST:
					{
						int count = this._reader.ReadInt32();
						List<int> ln = new List<int>(count);
						for (int i = 0; i < count; i++)
						{
							ln[i] = this._reader.ReadInt32();
						}
						return ln;
					}
				case BinaryType.UINT:
					{
						uint ui = this._reader.ReadUInt32();
						return ui;
					}
				case BinaryType.UINT_ARRAY:
					{
						int length = this._reader.ReadInt32();
						uint[] aui = new uint[length];
						for (int i = 0; i < length; i++)
						{
							aui[i] = this._reader.ReadUInt32();
						}
						return aui;
					}
				case BinaryType.UINT_LIST:
					{
						int count = this._reader.ReadInt32();
						List<uint> lui = new List<uint>(count);
						for (int i = 0; i < count; i++)
						{
							lui[i] = this._reader.ReadUInt32();
						}
						return lui;
					}
				case BinaryType.LONG:
					{
						long l = this._reader.ReadInt64();
						return l;
					}
				case BinaryType.LONG_ARRAY:
					{
						int length = this._reader.ReadInt32();
						long[] al = new long[length];
						for (int i = 0; i < length; i++)
						{
							al[i] = this._reader.ReadInt64();
						}
						return al;
					}
				case BinaryType.LONG_LIST:
					{
						int count = this._reader.ReadInt32();
						List<long> ll = new List<long>(count);
						for (int i = 0; i < count; i++)
						{
							ll[i] = this._reader.ReadInt64();
						}
						return ll;
					}
				case BinaryType.ULONG:
					{
						ulong ul = this._reader.ReadUInt64();
						return ul;
					}
				case BinaryType.ULONG_ARRAY:
					{
						int length = this._reader.ReadInt32();
						ulong[] aul = new ulong[length];
						for (int i = 0; i < length; i++)
						{
							aul[i] = this._reader.ReadUInt64();
						}
						return aul;
					}
				case BinaryType.ULONG_LIST:
					{
						int count = this._reader.ReadInt32();
						List<ulong> lul = new List<ulong>(count);
						for (int i = 0; i < count; i++)
						{
							lul[i] = this._reader.ReadUInt64();
						}
						return lul;
					}
				case BinaryType.DECIMAL:
					{
						decimal d = this._reader.ReadDecimal();
						return d;
					}
				case BinaryType.DECIMAL_ARRAY:
					{
						int length = this._reader.ReadInt32();
						decimal[] ad = new decimal[length];
						for (int i = 0; i < length; i++)
						{
							ad[i] = this._reader.ReadDecimal();
						}
						return ad;
					}
				case BinaryType.DECIMAL_LIST:
					{
						int count = this._reader.ReadInt32();
						List<decimal> ld = new List<decimal>(count);
						for (int i = 0; i < count; i++)
						{
							ld[i] = this._reader.ReadDecimal();
						}
						return ld;
					}
				case BinaryType.FLOAT:
					{
						float f = this._reader.ReadSingle();
						return f;
					}
				case BinaryType.FLOAT_ARRAY:
					{
						int length = this._reader.ReadInt32();
						float[] af = new float[length];
						for (int i = 0; i < length; i++)
						{
							af[i] = this._reader.ReadSingle();
						}
						return af;
					}
				case BinaryType.FLOAT_LIST:
					{
						int count = this._reader.ReadInt32();
						List<float> lf = new List<float>(count);
						for (int i = 0; i < count; i++)
						{
							lf[i] = this._reader.ReadSingle();
						}
						return lf;
					}
				case BinaryType.DOUBLE:
					{
						double d = this._reader.ReadDouble();
						return d;
					}
				case BinaryType.DOUBLE_ARRAY:
					{
						int length = this._reader.ReadInt32();
						double[] ad = new double[length];
						for (int i = 0; i < length; i++)
						{
							ad[i] = this._reader.ReadDouble();
						}
						return ad;
					}
				case BinaryType.DOUBLE_LIST:
					{
						int count = this._reader.ReadInt32();
						List<double> ld = new List<double>(count);
						for (int i = 0; i < count; i++)
						{
							ld[i] = this._reader.ReadDouble();
						}
						return ld;
					}
				case BinaryType.STRING:
					{
						string str = this._reader.ReadString();
						return str;
					}
				case BinaryType.STRING_ARRAY:
					{
						int length = this._reader.ReadInt32();
						string[] astr = new string[length];
						for (int i = 0; i < length; i++)
						{
							astr[i] = this._reader.ReadString();
						}
						return astr;
					}
				case BinaryType.STRING_LIST:
					{
						int count = this._reader.ReadInt32();
						List<string> lstr = new List<string>(count);
						for (int i = 0; i < count; i++)
						{
							lstr[i] = this._reader.ReadString();
						}
						return lstr;
					}
				case BinaryType.DATETIME:
					{
						DateTime dt = System.DateTime.FromBinary(this._reader.ReadInt64());
						return dt;
					}
				case BinaryType.DATETIME_ARRAY:
					{
						int length = this._reader.ReadInt32();
						DateTime[] adt = new DateTime[length];
						for (int i = 0; i < length; i++)
						{
							adt[i] = System.DateTime.FromBinary(this._reader.ReadInt64());
						}
						return adt;
					}
				case BinaryType.DATETIME_LIST:
					{
						int count = this._reader.ReadInt32();
						List<DateTime> ldt = new List<DateTime>(count);
						for (int i = 0; i < count; i++)
						{
							ldt[i] = System.DateTime.FromBinary(this._reader.ReadInt64());
						}
						return ldt;
					}
				case BinaryType.DICTIONARY:
					{
						System.Collections.Hashtable d = new System.Collections.Hashtable();
						int count = this._reader.ReadInt32();
						for (int i = 0; i < count; i++)
						{
							object key = this.Read();
							object value = this.Read();
							d.Add(key, value);
						}
						this._type = BinaryType.DICTIONARY;
						return d;
					}
				case BinaryType.LIST:
					{
						System.Collections.ArrayList l = new System.Collections.ArrayList();
						int count = this._reader.ReadInt32();
						for (int i = 0; i < count; i++)
						{
							l.Add(this.Read());
						}
						this._type = BinaryType.LIST;
						return l;
					}
				default:
					{
						throw new System.Exception("Invalid type: " + this._type);
					}
			}
		}

	}
}
