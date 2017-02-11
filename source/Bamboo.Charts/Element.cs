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
	public abstract class Element
	{
		private int _left;
		private int _top;
		private int _width;
		private int _height;
		private object _tag;

		public Element()
		{
		}

		public int Left
		{
			get { return this._left; }
			set { this._left = value; }
		}

		public int Top
		{
			get { return this._top; }
			set { this._top = value; }
		}

		public int Width
		{
			get { return this._width; }
			set { this._width = value; }
		}

		public int Height
		{
			get { return this._height; }
			set { this._height = value; }
		}

		public object Tag
		{
			get { return this._tag; }
			set { this._tag = value; }
		}

		public abstract void Render(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack);

		protected static void DrawString(System.Drawing.Graphics graphics, string s, System.Drawing.Font font, System.Drawing.Brush brush, float x, float y)
		{
			System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
			stringFormat.FormatFlags = stringFormat.FormatFlags | System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
			stringFormat.SetMeasurableCharacterRanges(new System.Drawing.CharacterRange[] { new System.Drawing.CharacterRange(0, s.Length) });
			stringFormat.SetTabStops(0, new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 });
			graphics.DrawString(s, font, brush, x, y, stringFormat);
		}

		public static System.Drawing.SizeF MeasureString(System.Drawing.Graphics graphics, string s, System.Drawing.Font font)
		{
			System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
			stringFormat.FormatFlags = stringFormat.FormatFlags | System.Drawing.StringFormatFlags.MeasureTrailingSpaces;
			stringFormat.SetMeasurableCharacterRanges(new System.Drawing.CharacterRange[] { new System.Drawing.CharacterRange(0, s.Length) });
			stringFormat.SetTabStops(0, new float[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80 });
			System.Drawing.Region[] regions = graphics.MeasureCharacterRanges(s, font, new System.Drawing.RectangleF(0, 0, s.Length * font.Height, font.Height), stringFormat);
			System.Drawing.RectangleF rect = regions[0].GetBounds(graphics);
			return rect.Size;
		}

	}
}
