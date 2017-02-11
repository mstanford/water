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
	public class Palette
	{
		private System.Drawing.Graphics _graphics;
        private Dictionary<string, System.Drawing.Color> _colors = new Dictionary<string, System.Drawing.Color>();
        private Dictionary<string, System.Drawing.Brush> _brushes = new Dictionary<string, System.Drawing.Brush>();
        private Dictionary<string, System.Drawing.Pen> _pens = new Dictionary<string, System.Drawing.Pen>();
        private Dictionary<string, System.Drawing.Font> _fonts = new Dictionary<string, System.Drawing.Font>();

		public Palette()
		{
			this._graphics = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1));
		}

		public System.Drawing.Graphics Graphics
		{
			get { return this._graphics; }
		}

		public System.Drawing.Color Color(string name)
		{
			if(_colors.ContainsKey(name))
			{
				return _colors[name];
			}
			System.Drawing.Color color = System.Drawing.Color.FromName(name);
			_colors.Add(name, color);
			return color;
		}

		public System.Drawing.Brush Brush(string name)
		{
			if(_brushes.ContainsKey(name))
			{
				return _brushes[name];
			}
			System.Drawing.Brush brush = new System.Drawing.SolidBrush(Color(name));
			_brushes.Add(name, brush);
			return brush;
		}

		public System.Drawing.Pen Pen(string name)
		{
			if(_pens.ContainsKey(name))
			{
				return _pens[name];
			}
			System.Drawing.Pen pen = new System.Drawing.Pen(Brush(name));
			_pens.Add(name, pen);
			return pen;
		}

		public System.Drawing.Pen Pen(string name, float width)
		{
			string fullname = name + "@" + width.ToString();
			if(_pens.ContainsKey(fullname))
			{
				return _pens[fullname];
			}
			System.Drawing.Pen pen = new System.Drawing.Pen(Brush(name), width);
			_pens.Add(fullname, pen);
			return pen;
		}

		public System.Drawing.Pen Pen(string name, float width, System.Drawing.Drawing2D.DashStyle dash)
		{
			string fullname = name + "@" + width.ToString() + dash.ToString();
			if (_pens.ContainsKey(fullname))
			{
				return _pens[fullname];
			}
			System.Drawing.Pen pen = new System.Drawing.Pen(Brush(name), width);
			pen.DashStyle = dash;
			_pens.Add(fullname, pen);
			return pen;
		}

		public System.Drawing.Font Font(Bamboo.Css.Font font)
		{
			if (font.Bold)
			{
				return Font(font.Name, font.Size);
			}
			else
			{
				return Font(font.Name, font.Size, System.Drawing.FontStyle.Bold);
			}
		}

		public System.Drawing.Font Font(string name, float size)
		{
			string fullname = name + "@" + size.ToString();
			if(_fonts.ContainsKey(fullname))
			{
				return _fonts[fullname];
			}
			System.Drawing.Font font = new System.Drawing.Font(name, size);
			_fonts.Add(fullname, font);
			return font;
		}

		public System.Drawing.Font Font(string name, float size, System.Drawing.FontStyle fontStyle)
		{
			string fullname = name + "@" + size.ToString() + "@" + fontStyle.ToString();
			if(_fonts.ContainsKey(fullname))
			{
				return _fonts[fullname];
			}
			System.Drawing.Font font = new System.Drawing.Font(name, size, fontStyle);
			_fonts.Add(fullname, font);
			return font;
		}

	}
}
