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
	public class BinaryWriter
	{
		private System.IO.BinaryWriter _writer;

		public BinaryWriter(System.IO.Stream stream)
			: this(new System.IO.BinaryWriter(stream))
		{
		}

		public BinaryWriter(System.IO.BinaryWriter writer)
		{
			if (!System.BitConverter.IsLittleEndian)
				throw new System.Exception("Architecture is not little endian.");

			this._writer = writer;
		}

		public void Write(object o)
		{
			if(o == null)
			{
				this._writer.Write(BinaryType.NULL);
			}
			else if (o is bool)
			{
				bool b = (bool)o;

				this._writer.Write(BinaryType.BOOL);
				this._writer.Write(b);
			}
			else if (o is bool[])
			{
				bool[] ab = (bool[])o;

				this._writer.Write(BinaryType.BOOL_ARRAY);
				this._writer.Write(ab.Length);
				for (int i = 0; i < ab.Length; i++)
				{
					this._writer.Write(ab[i]);
				}
			}
			else if (o is List<bool>)
			{
				List<bool> lb = (List<bool>)o;

				this._writer.Write(BinaryType.BOOL_LIST);
				this._writer.Write(lb.Count);
				for (int i = 0; i < lb.Count; i++)
				{
					this._writer.Write(lb[i]);
				}
			}
			else if (o is byte)
			{
				byte b = (byte)o;

				this._writer.Write(BinaryType.BYTE);
				this._writer.Write(b);
			}
			else if (o is byte[])
			{
				byte[] ab = (byte[])o;

				this._writer.Write(BinaryType.BYTE_ARRAY);
				this._writer.Write(ab.Length);
				for (int i = 0; i < ab.Length; i++)
				{
					this._writer.Write(ab[i]);
				}
			}
			else if (o is List<byte>)
			{
				List<byte> lb = (List<byte>)o;

				this._writer.Write(BinaryType.BYTE_LIST);
				this._writer.Write(lb.Count);
				for (int i = 0; i < lb.Count; i++)
				{
					this._writer.Write(lb[i]);
				}
			}
			else if (o is sbyte)
			{
				sbyte sb = (sbyte)o;

				this._writer.Write(BinaryType.SBYTE);
				this._writer.Write(sb);
			}
			else if (o is sbyte[])
			{
				sbyte[] asb = (sbyte[])o;

				this._writer.Write(BinaryType.SBYTE_ARRAY);
				this._writer.Write(asb.Length);
				for (int i = 0; i < asb.Length; i++)
				{
					this._writer.Write(asb[i]);
				}
			}
			else if (o is List<sbyte>)
			{
				List<sbyte> lsb = (List<sbyte>)o;

				this._writer.Write(BinaryType.SBYTE_LIST);
				this._writer.Write(lsb.Count);
				for (int i = 0; i < lsb.Count; i++)
				{
					this._writer.Write(lsb[i]);
				}
			}
			else if (o is char)
			{
				char ch = (char)o;

				this._writer.Write(BinaryType.CHAR);
				this._writer.Write(ch);
			}
			else if (o is char[])
			{
				char[] ach = (char[])o;

				this._writer.Write(BinaryType.CHAR_ARRAY);
				this._writer.Write(ach.Length);
				for (int i = 0; i < ach.Length; i++)
				{
					this._writer.Write(ach[i]);
				}
			}
			else if (o is List<char>)
			{
				List<char> lch = (List<char>)o;

				this._writer.Write(BinaryType.CHAR_LIST);
				this._writer.Write(lch.Count);
				for (int i = 0; i < lch.Count; i++)
				{
					this._writer.Write(lch[i]);
				}
			}
			else if (o is short)
			{
				short sh = (short)o;

				this._writer.Write(BinaryType.SHORT);
				this._writer.Write(sh);
			}
			else if (o is short[])
			{
				short[] ash = (short[])o;

				this._writer.Write(BinaryType.SHORT_ARRAY);
				this._writer.Write(ash.Length);
				for (int i = 0; i < ash.Length; i++)
				{
					this._writer.Write(ash[i]);
				}
			}
			else if (o is List<short>)
			{
				List<short> lsh = (List<short>)o;

				this._writer.Write(BinaryType.SHORT_LIST);
				this._writer.Write(lsh.Count);
				for (int i = 0; i < lsh.Count; i++)
				{
					this._writer.Write(lsh[i]);
				}
			}
			else if (o is ushort)
			{
				ushort us = (ushort)o;

				this._writer.Write(BinaryType.USHORT);
				this._writer.Write(us);
			}
			else if (o is ushort[])
			{
				ushort[] aus = (ushort[])o;

				this._writer.Write(BinaryType.USHORT_ARRAY);
				this._writer.Write(aus.Length);
				for (int i = 0; i < aus.Length; i++)
				{
					this._writer.Write(aus[i]);
				}
			}
			else if (o is List<ushort>)
			{
				List<ushort> lus = (List<ushort>)o;

				this._writer.Write(BinaryType.USHORT_LIST);
				this._writer.Write(lus.Count);
				for (int i = 0; i < lus.Count; i++)
				{
					this._writer.Write(lus[i]);
				}
			}
			else if (o is int)
			{
				int n = (int)o;

				this._writer.Write(BinaryType.INT);
				this._writer.Write(n);
			}
			else if (o is int[])
			{
				int[] an = (int[])o;

				this._writer.Write(BinaryType.INT_ARRAY);
				this._writer.Write(an.Length);
				for (int i = 0; i < an.Length; i++)
				{
					this._writer.Write(an[i]);
				}
			}
			else if (o is List<int>)
			{
				List<int> ln = (List<int>)o;

				this._writer.Write(BinaryType.INT_LIST);
				this._writer.Write(ln.Count);
				for (int i = 0; i < ln.Count; i++)
				{
					this._writer.Write(ln[i]);
				}
			}
			else if (o is uint)
			{
				uint ui = (uint)o;

				this._writer.Write(BinaryType.UINT);
				this._writer.Write(ui);
			}
			else if (o is uint[])
			{
				uint[] aui = (uint[])o;

				this._writer.Write(BinaryType.UINT_ARRAY);
				this._writer.Write(aui.Length);
				for (int i = 0; i < aui.Length; i++)
				{
					this._writer.Write(aui[i]);
				}
			}
			else if (o is List<uint>)
			{
				List<uint> lui = (List<uint>)o;

				this._writer.Write(BinaryType.UINT_LIST);
				this._writer.Write(lui.Count);
				for (int i = 0; i < lui.Count; i++)
				{
					this._writer.Write(lui[i]);
				}
			}
			else if (o is long)
			{
				long l = (long)o;

				this._writer.Write(BinaryType.LONG);
				this._writer.Write(l);
			}
			else if (o is long[])
			{
				long[] al = (long[])o;

				this._writer.Write(BinaryType.LONG_ARRAY);
				this._writer.Write(al.Length);
				for (int i = 0; i < al.Length; i++)
				{
					this._writer.Write(al[i]);
				}
			}
			else if (o is List<long>)
			{
				List<long> ll = (List<long>)o;

				this._writer.Write(BinaryType.LONG_LIST);
				this._writer.Write(ll.Count);
				for (int i = 0; i < ll.Count; i++)
				{
					this._writer.Write(ll[i]);
				}
			}
			else if (o is ulong)
			{
				ulong ul = (ulong)o;

				this._writer.Write(BinaryType.ULONG);
				this._writer.Write(ul);
			}
			else if (o is ulong[])
			{
				ulong[] aul = (ulong[])o;

				this._writer.Write(BinaryType.ULONG_ARRAY);
				this._writer.Write(aul.Length);
				for (int i = 0; i < aul.Length; i++)
				{
					this._writer.Write(aul[i]);
				}
			}
			else if (o is List<ulong>)
			{
				List<ulong> lul = (List<ulong>)o;

				this._writer.Write(BinaryType.ULONG_LIST);
				this._writer.Write(lul.Count);
				for (int i = 0; i < lul.Count; i++)
				{
					this._writer.Write(lul[i]);
				}
			}
			else if (o is decimal)
			{
				decimal d = (decimal)o;

				this._writer.Write(BinaryType.DECIMAL);
				this._writer.Write(d);
			}
			else if (o is decimal[])
			{
				decimal[] ad = (decimal[])o;

				this._writer.Write(BinaryType.DECIMAL_ARRAY);
				this._writer.Write(ad.Length);
				for (int i = 0; i < ad.Length; i++)
				{
					this._writer.Write(ad[i]);
				}
			}
			else if (o is List<decimal>)
			{
				List<decimal> ld = (List<decimal>)o;

				this._writer.Write(BinaryType.DECIMAL_LIST);
				this._writer.Write(ld.Count);
				for (int i = 0; i < ld.Count; i++)
				{
					this._writer.Write(ld[i]);
				}
			}
			else if (o is float)
			{
				float f = (float)o;

				this._writer.Write(BinaryType.FLOAT);
				this._writer.Write(f);
			}
			else if (o is float[])
			{
				float[] af = (float[])o;

				this._writer.Write(BinaryType.FLOAT_ARRAY);
				this._writer.Write(af.Length);
				for (int i = 0; i < af.Length; i++)
				{
					this._writer.Write(af[i]);
				}
			}
			else if (o is List<float>)
			{
				List<float> lf = (List<float>)o;

				this._writer.Write(BinaryType.FLOAT_LIST);
				this._writer.Write(lf.Count);
				for (int i = 0; i < lf.Count; i++)
				{
					this._writer.Write(lf[i]);
				}
			}
			else if (o is double)
			{
				double d = (double)o;

				this._writer.Write(BinaryType.DOUBLE);
				this._writer.Write(d);
			}
			else if (o is double[])
			{
				double[] ad = (double[])o;

				this._writer.Write(BinaryType.DOUBLE_ARRAY);
				this._writer.Write(ad.Length);
				for (int i = 0; i < ad.Length; i++)
				{
					this._writer.Write(ad[i]);
				}
			}
			else if (o is List<double>)
			{
				List<double> ld = (List<double>)o;

				this._writer.Write(BinaryType.DOUBLE_LIST);
				this._writer.Write(ld.Count);
				for (int i = 0; i < ld.Count; i++)
				{
					this._writer.Write(ld[i]);
				}
			}
			else if (o is string)
			{
				string str = (string)o;

				this._writer.Write(BinaryType.STRING);
				this._writer.Write(str);
			}
			else if (o is string[])
			{
				string[] astr = (string[])o;

				this._writer.Write(BinaryType.STRING_ARRAY);
				this._writer.Write(astr.Length);
				for (int i = 0; i < astr.Length; i++)
				{
					this._writer.Write(astr[i]);
				}
			}
			else if (o is List<string>)
			{
				List<string> lstr = (List<string>)o;

				this._writer.Write(BinaryType.STRING_LIST);
				this._writer.Write(lstr.Count);
				for (int i = 0; i < lstr.Count; i++)
				{
					this._writer.Write(lstr[i]);
				}
			}
			else if (o is DateTime)
			{
				DateTime dt = (DateTime)o;

				this._writer.Write(BinaryType.DATETIME);
				this._writer.Write(dt.ToBinary());
			}
			else if (o is DateTime[])
			{
				DateTime[] adt = (DateTime[])o;

				this._writer.Write(BinaryType.DATETIME_ARRAY);
				this._writer.Write(adt.Length);
				for (int i = 0; i < adt.Length; i++)
				{
					this._writer.Write(adt[i].ToBinary());
				}
			}
			else if (o is List<DateTime>)
			{
				List<DateTime> ldt = (List<DateTime>)o;

				this._writer.Write(BinaryType.DATETIME_LIST);
				this._writer.Write(ldt.Count);
				for (int i = 0; i < ldt.Count; i++)
				{
					this._writer.Write(ldt[i].ToBinary());
				}
			}
			else if (o is System.Collections.IDictionary)
			{
				System.Collections.IDictionary d = (System.Collections.IDictionary)o;

				this._writer.Write(BinaryType.DICTIONARY);
				this._writer.Write(d.Count);
				System.Collections.IEnumerator keys = d.Keys.GetEnumerator();
				System.Collections.IEnumerator values = d.Values.GetEnumerator();
				for (int i = 0; i < d.Count; i++)
				{
					keys.MoveNext();
					values.MoveNext();

					this.Write(keys.Current);
					this.Write(values.Current);
				}
			}
			else if (o is System.Collections.IList)
			{
				System.Collections.IList l = (System.Collections.IList)o;

				this._writer.Write(BinaryType.LIST);
				this._writer.Write(l.Count);
				for (int i = 0; i < l.Count; i++)
				{
					this.Write(l[i]);
				}
			}
			else
			{
				throw new System.Exception("Invalid type: " + o.GetType().FullName);
			}
		}

		public void Flush()
		{
			this._writer.Flush();
		}

	}
}
