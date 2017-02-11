// ------------------------------------------------------------------------------
// 
// Copyright (c) 2008 Swampware, Inc.
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

namespace Surf
{
	public class Bitmap : System.IComparable
	{
		private System.Collections.BitArray _bits;

		public Bitmap(int length)
		{
			this._bits = new System.Collections.BitArray(length);
		}

		public Bitmap(System.Collections.BitArray bits)
		{
			this._bits = bits;
		}

		public int Length
		{
			get { return this._bits.Length; }
		}

		public bool Get(int index)
		{
			return this._bits.Get(index);
		}

		public void Set(int index, bool value)
		{
			this._bits.Set(index, value);
		}

		public Surf.Bitmap And(Surf.Bitmap bitmap)
		{
			if (this.Length != bitmap.Length)
			{
				throw new System.Exception("Bitmaps are not the same size.");
			}
			Surf.Bitmap C = new Surf.Bitmap(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				C.Set(i, this.Get(i) && bitmap.Get(i));
			}
			return C;
		}

		public Surf.Bitmap Not()
		{
			Surf.Bitmap B = new Surf.Bitmap(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				B.Set(i, !this.Get(i));
			}
			return B;
		}

		public Surf.Bitmap Or(Surf.Bitmap bitmap)
		{
			if (this.Length != bitmap.Length)
			{
				throw new System.Exception("Bitmaps are not the same size.");
			}
			Surf.Bitmap C = new Surf.Bitmap(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				C.Set(i, this.Get(i) || bitmap.Get(i));
			}
			return C;
		}

		public Surf.Bitmap Copy()
		{
			Surf.Bitmap B = new Surf.Bitmap(this.Length);
			for (int i = 0; i < this.Length; i++)
			{
				B.Set(i, this.Get(i));
			}
			return B;
		}

		public int CompareTo(object obj)
		{
			Bitmap bitmap = (Bitmap)obj;
			int min = Math.Min(this.Length, bitmap.Length);
			for (int i = 0; i < min; i++)
			{
				int cmp = this.Get(i).CompareTo(bitmap.Get(i));
				if (cmp != 0)
				{
					return cmp;
				}
			}
			return (this.Length == bitmap.Length) ? 0 : ((this.Length > bitmap.Length) ? -1 : 1);
		}

		public override bool Equals(object obj)
		{
			Bitmap bitmap = (Bitmap)obj;
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Get(i) != bitmap.Get(i))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		public override string ToString()
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("[");
			for (int i = 0; i < this.Length; i++)
			{
				stringBuilder.Append(this.Get(i) ? "1" : "0");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

	}
}
