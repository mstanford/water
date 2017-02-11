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

namespace Bamboo.Charts
{
	public abstract class ChartBase : Charts.Element
	{
		private string _title;
		private Bamboo.DataStructures.Table _table;
		private Bamboo.Css.StyleSheet _defaultStyleSheet = null;
		private Bamboo.Css.StyleSheet _styleSheet = null;

		public ChartBase()
		{
			this.Width = 300;
			this.Height = 300;
		}

		public string Title
		{
			get { return this._title; }
			set { this._title = value; }
		}

		public Bamboo.DataStructures.Table Table
		{
			get { return this._table; }
			set { this._table = value; }
		}

		public Bamboo.Css.StyleSheet DefaultStyleSheet
		{
			get { return this._defaultStyleSheet; }
			set { this._defaultStyleSheet = value; }
		}

		public Bamboo.Css.StyleSheet StyleSheet
		{
			get { return this._styleSheet; }
			set { this._styleSheet = value; }
		}

		protected System.Drawing.RectangleF DrawTitle(string title, System.Drawing.Font font, System.Drawing.Brush brush, System.Drawing.Graphics graphics, System.Drawing.RectangleF rectangle)
		{
			System.Drawing.SizeF size = MeasureString(graphics, title, font);
			DrawString(graphics, title, font, brush, rectangle.Left + (rectangle.Width / 2) - (size.Width / 2), rectangle.Top);
			return new System.Drawing.RectangleF(rectangle.Left, rectangle.Top + size.Height, rectangle.Width, rectangle.Height - size.Height);
		}

		public override void Render(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack)
		{
			if (this._table != null)
			{
				int depth = 0;
				if (this._defaultStyleSheet != null)
				{
					styleStack.PushStyleSheet(this._defaultStyleSheet);
					depth++;
				}
				if (this._styleSheet != null)
				{
					styleStack.PushStyleSheet(this._styleSheet);
					depth++;
				}

				RenderChart(graphics, palette, styleStack, new System.Drawing.RectangleF(this.Left, this.Top, this.Width, this.Height), this._title, this._table);

				for (int i = 0; i < depth; i++)
				{
					styleStack.PopStyleSheet();
				}
			}
		}

		protected abstract void RenderChart(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table);

		protected static Bamboo.Css.StyleSheet LoadStyleSheet(string name)
		{
			System.IO.Stream stream = typeof(ChartBase).Assembly.GetManifestResourceStream("Statibase.Charts." + name);
			if (stream == null)
			{
				return null;
			}
			Bamboo.Css.CssReader cssReader = new Bamboo.Css.CssReader(stream);
			Bamboo.Css.StyleSheet styleSheet = cssReader.Read();
			stream.Close();
			return styleSheet;
		}

	}
}
