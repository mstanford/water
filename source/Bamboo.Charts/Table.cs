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
	/// <summary>
	/// Summary description for OneWayTable.
	/// </summary>
	public class Table : TableBase
	{

		public Table()
		{
			this.DefaultStyleSheet = LoadStyleSheet("Table.css");
		}

		protected override void RenderChart(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table)
		{
			styleStack.PushTag("Table");



			string backColor = (string)styleStack["BackColor"];
			Bamboo.Css.Font font = (Bamboo.Css.Font)styleStack["Font"];
			string foreColor = (string)styleStack["ForeColor"];
			int padding = (int)styleStack["Padding"];

			//TODO put in stylesheet
			System.Drawing.Font titleFont = palette.Font(font.Name, font.Size * 1.2f, System.Drawing.FontStyle.Bold);

			System.Drawing.Pen backColorPen = palette.Pen(backColor);
			System.Drawing.Brush backColorBrush = palette.Brush(backColor);
			System.Drawing.Brush foreColorBrush = palette.Brush(foreColor);



			// Background
			graphics.DrawRectangle(backColorPen, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
			graphics.FillRectangle(backColorBrush, rectangle);
			rectangle = new System.Drawing.RectangleF(rectangle.Left + padding, rectangle.Top + padding, rectangle.Width - padding - padding, rectangle.Height - padding - padding);

			// Title
			if (title != null)
			{
				rectangle = DrawTitle(title, titleFont, foreColorBrush, graphics, rectangle);
			}



			int width = table.Columns.Count;
			int height = table.Rows.Count;

			List<Cell[]> cellsList = new List<Cell[]>(height + 1);
			Cell[] cells;

			styleStack.PushTag("Header");
			backColor = (string)styleStack["BackColor"];
			foreColor = (string)styleStack["ForeColor"];
			System.Drawing.ContentAlignment textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
			cells = new Cell[width];
			for (int x = 0; x < width; x++)
			{
				cells[x].HasValue = true;
				cells[x].Type = CellType.Header;
				cells[x].Colspan = 1;
				cells[x].Rowspan = 1;
				cells[x].Value = table.Columns[x];
				cells[x].BackgroundColor = backColor;
				cells[x].ForegroundColor = foreColor;
				cells[x].TextAlign = textAlign;
			}
			cellsList.Add(cells);
			styleStack.PopTag();


			styleStack.PushTag("Cell");
			backColor = (string)styleStack["BackColor"];
			foreColor = (string)styleStack["ForeColor"];
			textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
			for (int y = 0; y < height; y++)
			{
				Bamboo.DataStructures.Tuple row = table.Rows[y];
				cells = new Cell[width];
				for (int x = 0; x < width; x++)
				{
					//TODO push cell.
					cells[x].HasValue = true;
					cells[x].Type = CellType.Cell;
					cells[x].Colspan = 1;
					cells[x].Rowspan = 1;
					cells[x].Value = row[x];
					cells[x].BackgroundColor = backColor;
					cells[x].ForegroundColor = foreColor;
					cells[x].TextAlign = textAlign;
				}
				cellsList.Add(cells);
			}
			styleStack.PopTag();


			Render(graphics, palette, styleStack, cellsList, rectangle);



			styleStack.PopTag();
		}

    }
}
