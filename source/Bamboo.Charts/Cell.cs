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

namespace Bamboo.Charts
{
	public struct Cell
	{
		// These values are filled in by the builder.
		public bool HasValue;
		public CellType Type;
		public int Colspan;
		public int Rowspan;
		public object Value;

//TODO store in bit set.  and use Brush array indexer.
		public string BackgroundColor;
//TODO store in bit set.  and use Pen array indexer.
		public string ForegroundColor;
		public System.Drawing.ContentAlignment TextAlign;

		// Formatting
		public string Text;

		// Physical measurements
		public int Left;
		public int Top;
		public int Width;
		public int Height;
		public float MeasuredWidth;
		public float MeasuredHeight;
		public int ColumnWidth;
		public int RowHeight;

		// Optimization to minimize calls to GDI+
		public int BorderColspan;
		public int BorderRowspan;
		public int BackgroundColspan;
		public int BackgroundRowspan;
		public int BackgroundColorArgb;

	}
}
