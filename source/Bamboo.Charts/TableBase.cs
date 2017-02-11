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
	public abstract class TableBase : ChartBase
	{
		private System.Collections.Comparer _comparer = System.Collections.Comparer.DefaultInvariant;
		private bool _shrinkToFit = true;

		//TODO
		//Alternating row at 50% color.

		//TODO put in stylesheet.
		//Format negative numbers with red color, parens, or minus sign.
//		private bool _show_comma;
//		private bool _show_parentheses;

		public TableBase()
		{
		}

		public bool ShrinkToFit
		{
			get { return this._shrinkToFit; }
			set { this._shrinkToFit = value; }
		}

		public System.Collections.Comparer Comparer
		{
			get { return this._comparer; }
			set { this._comparer = value; }
		}

		protected void Render(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, List<Cell[]> cells, System.Drawing.RectangleF rectangle)
		{
			string borderColor = (string)styleStack["BorderColor"];
			int borderWidth = (int)styleStack["BorderWidth"];
			Bamboo.Css.Font font = (Bamboo.Css.Font)styleStack["Font"];
			bool horizontalBorder = (bool)styleStack["HorizontalBorder"];
			bool outerBorder = (bool)styleStack["OuterBorder"];
			int padding = (int)styleStack["Padding"];
			int precision = (int)styleStack["Precision"];
			bool verticalBorder = (bool)styleStack["VerticalBorder"];

			System.Drawing.Pen borderColorPen = palette.Pen(borderColor);



			int columns = (cells.Count == 0) ? 0 : cells[0].Length;
			int rows = cells.Count;

			bool tooBig = true;
			float excessWidth = 0;
			float excessHeight = 0;
			Bamboo.Css.Font gridFont = new Bamboo.Css.Font(font.Name, font.Size, font.Bold);
//TODO DELETE			float gridFontSize = font.Size;
			while (tooBig && gridFont.Size > 0)
			{
				//
				// Prepare the grid for painting.
				//
				int cellLeft = (int)Math.Ceiling(rectangle.Left);
				int cellTop = (int)Math.Ceiling(rectangle.Top);

				if (outerBorder)
				{
					cellLeft += borderWidth;
					cellTop += borderWidth;
				}

				for (int y = 0; y < rows; y++)
				{
					for (int x = 0; x < columns; x++)
					{
						object value = cells[y][x].Value;
						int colspan = cells[y][x].Colspan;
						int rowspan = cells[y][x].Rowspan;
						int colspanMinusOne = colspan - 1;
						int rowspanMinusOne = rowspan - 1;



						//
						// Read:  Value
						//
						// Write: Text
						//
						if (value == null)
						{
							cells[y][x].Text = String.Empty;
						}
						else if (value is double
							|| value is decimal
							|| value is float
							|| value is int
							|| value is long)
						{
							double d = System.Convert.ToDouble(value);
							//TODO
							//						if(this._show_parentheses && n < 0)
							//						{
							//							return "(" + Math.Abs(d).ToString() + ")";
							//						}
							//						else
							{
								cells[y][x].Text = d.ToString("N" + precision);
							}
						}
						else
						{
							cells[y][x].Text = value.ToString();
						}



						//
						// Read:  HasValue
						//        Text
						//        Style
						//        Rowspan
						//        Colspan
						//
						// Write: MeasuredWidth
						//        MeasuredHeight
						//        BorderColspan (forward)
						//        BorderRowspan (forward)
						//        BackgroundColspan (forward)
						//        BackgroundRowspan (forward)
						//
						if (cells[y][x].HasValue)
						{
							int backgroundColorArgb = palette.Color(cells[y][x].BackgroundColor).ToArgb();

							// Measure the text in the cell.
							System.Drawing.SizeF size = MeasureString(palette.Graphics, cells[y][x].Text, palette.Font(gridFont.Name, gridFont.Size));
							size.Width += padding + padding;
							size.Height += padding + padding;
							cells[y][x].MeasuredWidth = size.Width;
							cells[y][x].MeasuredHeight = size.Height;

							for (int xx = 0; xx < colspan; xx++)
							{
								if (y + rowspanMinusOne < (rows - 1))
								{
									if (horizontalBorder)
									{
										cells[y + rowspanMinusOne][x + xx].BorderColspan = 1;
									}
								}
							}

							for (int yy = 0; yy < rowspan; yy++)
							{
								if (x + colspanMinusOne < (columns - 1))
								{
									if (verticalBorder)
									{
										cells[y + yy][x + colspanMinusOne].BorderRowspan = 1;
									}
								}
							}

							for (int xx = 0; xx < colspan; xx++)
							{
								for (int yy = 0; yy < rowspan; yy++)
								{
									cells[y + yy][x + xx].BackgroundColspan = 1;
									cells[y + yy][x + xx].BackgroundRowspan = 1;
									cells[y + yy][x + xx].BackgroundColorArgb = backgroundColorArgb;
								}
							}
						}
					}
				}



				//
				// Make all the cells in the same column the same width.
				//
				// Read:  Colspan
				//        MeasuredWidth
				//
				// Write: ColumnWidth (x-forward)
				//        Left
				//        Colspan (x-forward)
				//
				for (int x = 0; x < columns; x++)
				{
					int columnWidth = 0;

					//
					// Get greatest width.
					//
					for (int y = 0; y < rows; y++)
					{
						if (cells[y][x].Colspan == 1)
						{
							int w2 = (int)Math.Ceiling(cells[y][x].MeasuredWidth);
							if (w2 > columnWidth)
							{
								columnWidth = w2;
							}
						}
					}

					for (int y = 0; y < rows; y++)
					{
						//
						// Set width.
						//
						cells[y][x].ColumnWidth = columnWidth;
						cells[y][x].Left = cellLeft;

						int colspan = cells[y][x].Colspan;
						if (colspan > 1)
						{
							//
							// Shift right.
							//
							cells[y][x + 1].Colspan = colspan - 1;
							cells[y][x + 1].MeasuredWidth = cells[y][x].MeasuredWidth - columnWidth; //TODO I'm not sure about this.
						}
					}

					cellLeft += columnWidth;
					if (verticalBorder && x < (columns - 1))
					{
						cellLeft += borderWidth;
					}
				}



				//
				// Make all the cells in the same row the same height.
				//
				// Read:  Rowspan
				//        MeasuredHeight
				//
				// Write: RowHeight (y-forward)
				//        Top
				//        Rowspan (y-forward)
				//
				for (int y = 0; y < rows; y++)
				{
					int rowHeight = 0;

					//
					// Get greatest height.
					//
					for (int x = 0; x < columns; x++)
					{
						if (cells[y][x].Rowspan == 1)
						{
							int h2 = (int)Math.Ceiling(cells[y][x].MeasuredHeight);
							if (h2 > rowHeight)
							{
								rowHeight = h2;
							}
						}
					}

					for (int x = 0; x < columns; x++)
					{
						//
						// Set height.
						//
						cells[y][x].RowHeight = rowHeight;
						cells[y][x].Top = cellTop;

						int rowspan = cells[y][x].Rowspan;
						if (rowspan > 1)
						{
							//
							// Shift down.
							//
							cells[y + 1][x].Rowspan = rowspan - 1;
							cells[y + 1][x].MeasuredHeight = cells[y][x].MeasuredHeight - rowHeight; //TODO I'm not sure about this.
						}
					}

					cellTop += rowHeight;
					if (horizontalBorder && y < (rows - 1))
					{
						cellTop += borderWidth;
					}
				}



				for (int y = 0; y < rows; y++)
				{
					for (int x = 0; x < columns; x++)
					{
						int colspan = cells[y][x].Colspan;
						int colspanMinusOne = colspan - 1;
						int rowspan = cells[y][x].Rowspan;
						int rowspanMinusOne = rowspan - 1;
						int borderColspan = cells[y][x].BorderColspan;
						int borderRowspan = cells[y][x].BorderRowspan;
						int backgroundColspan = cells[y][x].BackgroundColspan;
						int backgroundRowspan = cells[y][x].BackgroundRowspan;
						int backgroundColorArgb = cells[y][x].BackgroundColorArgb;



						//
						// Read:  Colspan
						//        Left (x-forward)
						//        ColumnWidth (x-forward)
						//
						// Write: Width
						//
						if (colspan > 1)
						{
							cells[y][x].Width = cells[y][x + colspanMinusOne].Left + cells[y][x + colspanMinusOne].ColumnWidth - cells[y][x].Left;
						}
						else
						{
							cells[y][x].Width = cells[y][x].ColumnWidth;
						}



						//
						// Read:  Rowspan
						//        Top (y-forward)
						//        RowHeight (y-forward)
						//
						// Write: Height
						//
						if (rowspan > 1)
						{
							cells[y][x].Height = cells[y + rowspanMinusOne][x].Top + cells[y + rowspanMinusOne][x].RowHeight - cells[y][x].Top;
						}
						else
						{
							cells[y][x].Height = cells[y][x].RowHeight;
						}



						//
						// Read:  BorderColspan (x-forward)
						//
						// Write: BorderColspan
						//
						if (borderColspan > 0)
						{
							int length = borderColspan;
							for (int xx = (x + 1); xx < columns; xx++)
							{
								if (cells[y][xx].BorderColspan > 0)
								{
									length += cells[y][xx].BorderColspan;
									cells[y][xx].BorderColspan = 0;
								}
								else
								{
									break;
								}
							}
							cells[y][x].BorderColspan = length;
						}



						//
						// Read:  BorderRowspan (y-forward)
						//
						// Write: BorderRowspan
						//
						if (borderRowspan > 0)
						{
							int length = borderRowspan;
							for (int yy = (y + 1); yy < rows; yy++)
							{
								if (cells[yy][x].BorderRowspan > 0)
								{
									length += cells[yy][x].BorderRowspan;
									cells[yy][x].BorderRowspan = 0;
								}
								else
								{
									break;
								}
							}
							cells[y][x].BorderRowspan = length;
						}



						if (backgroundColspan > 0)
						{
							//
							// Read:  BackgroundColspan (x-forward)
							//        BackgroundColor (x-forward)
							//
							// Write: BackgroundColspan (x-forward)
							//
							int length = backgroundColspan;
							for (int xx = (x + 1); xx < columns; xx++)
							{
								if (cells[y][xx].BackgroundColspan > 0
									&& backgroundColorArgb == cells[y][xx].BackgroundColorArgb)
								{
									length += cells[y][xx].BackgroundColspan;
									cells[y][xx].BackgroundColspan = 0;
								}
								else
								{
									break;
								}
							}
							backgroundColspan = length;
							cells[y][x].BackgroundColspan = length;
						}



						if (backgroundRowspan > 0)
						{
							//
							// Read:  BackgroundRowspan (y-forward)
							//        BackgroundColor (y-forward)
							//
							// Write: BackgroundRowspan (y-forward)
							//
							int length = backgroundRowspan;
							for (int yy = (y + 1); yy < rows; yy++)
							{
								if (cells[yy][x].BackgroundRowspan > 0
									&& backgroundColorArgb == cells[yy][x].BackgroundColorArgb)
								{
									length += cells[yy][x].BackgroundRowspan;
									cells[yy][x].BackgroundRowspan = 0;
								}
								else
								{
									break;
								}
							}
							backgroundRowspan = length;
							cells[y][x].BackgroundRowspan = length;
						}



						if (backgroundColspan == 0 || backgroundRowspan == 0)
						{
							//
							// Read:  BackgroundColspan
							//        BackgroundRowspan
							//
							// Write: BackgroundColspan
							//        BackgroundRowspan
							//
							cells[y][x].BackgroundColspan = 0;
							cells[y][x].BackgroundRowspan = 0;
						}
					}
				}



				//
				// Set outer border.
				//
				if (outerBorder)
				{
					cellLeft += borderWidth;
					cellTop += borderWidth;
				}

				if (!_shrinkToFit)
				{
				    tooBig = false;
				    excessWidth = 0;
				    excessHeight = 0;
				    this.Width = cellLeft - this.Left;
					this.Height = cellTop - this.Top;
				}
				else if (cellLeft < (rectangle.Left + rectangle.Width) && cellTop < (rectangle.Top + rectangle.Height))
				{
					tooBig = false;
					excessWidth = rectangle.Left + rectangle.Width - cellLeft;
					excessHeight = rectangle.Top + rectangle.Height - cellTop;
				}
				else
				{
					gridFont.Size -= 1;
				}
			}

			//TODO test for no data.
			//if (cells == null)
			//{
			//    //TODO paint it white.
			//    return;
			//}

			if (!tooBig)
			{
				columns = (cells.Count == 0) ? 0 : cells[0].Length;
				rows = cells.Count;

				float x_padding = excessWidth / columns;
				float y_padding = excessHeight / rows;
				for (int y = 0; y < rows; y++)
				{
					for (int x = 0; x < columns; x++)
					{
						cells[y][x].Left += (int)Math.Ceiling(x * x_padding);
						cells[y][x].Top += (int)Math.Ceiling(y * y_padding);
						cells[y][x].Width += (int)Math.Ceiling(x_padding);
						cells[y][x].Height += (int)Math.Ceiling(y_padding);
					}
					cells[y][columns - 1].Width = (int)Math.Ceiling(rectangle.Left) + (int)Math.Ceiling(rectangle.Width) - cells[y][columns - 1].Left;
				}
				for (int x = 0; x < columns; x++)
				{
					cells[rows - 1][x].Height = (int)Math.Ceiling(rectangle.Top) + (int)Math.Ceiling(rectangle.Height) - cells[rows - 1][x].Top;
				}

				int halfBorderWidth = (borderWidth / 2);

				for (int y = 0; y < rows; y++)
				{
					for (int x = 0; x < columns; x++)
					{
						int backgroundColspan = cells[y][x].BackgroundColspan;
						int backgroundRowspan = cells[y][x].BackgroundRowspan;

						// Background
						if (backgroundColspan > 0 && backgroundRowspan > 0)
						{
							int backgroundColspanMinusOne = backgroundColspan - 1;
							int backgroundRowspanMinusOne = backgroundRowspan - 1;
							graphics.FillRectangle(palette.Brush(cells[y][x].BackgroundColor), cells[y][x].Left, cells[y][x].Top, cells[y][x + backgroundColspanMinusOne].Left + cells[y][x + backgroundColspanMinusOne].Width + borderWidth - cells[y][x].Left, cells[y + backgroundRowspanMinusOne][x].Top + cells[y + backgroundRowspanMinusOne][x].Height + borderWidth - cells[y][x].Top);
						}
					}
				}

				for (int y = 0; y < rows; y++)
				{
					for (int x = 0; x < columns; x++)
					{
						//Borders
						if (horizontalBorder && cells[y][x].BorderColspan > 0)
						{
							// Bottom border
							float bottom = cells[y][x].Top + cells[y][x].Height + halfBorderWidth;
							int borderColspanMinusOne = cells[y][x].BorderColspan - 1;
							graphics.DrawLine(borderColorPen, cells[y][x].Left, bottom, cells[y][x + borderColspanMinusOne].Left + cells[y][x + borderColspanMinusOne].Width + borderWidth - 1, bottom);
						}
						if (verticalBorder && cells[y][x].BorderRowspan > 0)
						{
							// Right border
							float right = cells[y][x].Left + cells[y][x].Width + halfBorderWidth;
							int borderRowspanMinusOne = cells[y][x].BorderRowspan - 1;
							graphics.DrawLine(borderColorPen, right, cells[y][x].Top, right, cells[y + borderRowspanMinusOne][x].Top + cells[y + borderRowspanMinusOne][x].Height + borderWidth - 1);
						}

						if (cells[y][x].Value != null && gridFont.Size > 0)
						{
							// Text
							string text = cells[y][x].Text;
							System.Drawing.Brush foregroundColorBrush = palette.Brush(cells[y][x].ForegroundColor);
							System.Drawing.Font textFont = (gridFont.Bold) ? new System.Drawing.Font(gridFont.Name, gridFont.Size, System.Drawing.FontStyle.Bold) : new System.Drawing.Font(gridFont.Name, gridFont.Size);
							float left;
							float top;
							switch (cells[y][x].TextAlign)
							{
								case System.Drawing.ContentAlignment.BottomCenter:
									{
										left = cells[y][x].Left + padding + ((cells[y][x].Width - cells[y][x].MeasuredWidth) / 2);
										top = cells[y][x].Top + padding + (cells[y][x].Height - cells[y][x].MeasuredHeight);
										break;
									}
								case System.Drawing.ContentAlignment.BottomLeft:
									{
										left = cells[y][x].Left + padding;
										top = cells[y][x].Top + padding + (cells[y][x].Height - cells[y][x].MeasuredHeight);
										break;
									}
								case System.Drawing.ContentAlignment.BottomRight:
									{
										left = cells[y][x].Left + padding + (cells[y][x].Width - cells[y][x].MeasuredWidth);
										top = cells[y][x].Top + padding + (cells[y][x].Height - cells[y][x].MeasuredHeight);
										break;
									}
								case System.Drawing.ContentAlignment.MiddleCenter:
									{
										left = cells[y][x].Left + padding + ((cells[y][x].Width - cells[y][x].MeasuredWidth) / 2);
										top = cells[y][x].Top + padding + ((cells[y][x].Height - cells[y][x].MeasuredHeight) / 2);
										break;
									}
								case System.Drawing.ContentAlignment.MiddleLeft:
									{
										left = cells[y][x].Left + padding;
										top = cells[y][x].Top + padding + ((cells[y][x].Height - cells[y][x].MeasuredHeight) / 2);
										break;
									}
								case System.Drawing.ContentAlignment.MiddleRight:
									{
										left = cells[y][x].Left + padding + (cells[y][x].Width - cells[y][x].MeasuredWidth);
										top = cells[y][x].Top + padding + ((cells[y][x].Height - cells[y][x].MeasuredHeight) / 2);
										break;
									}
								case System.Drawing.ContentAlignment.TopLeft:
									{
										left = cells[y][x].Left + padding;
										top = cells[y][x].Top + padding;
										break;
									}
								case System.Drawing.ContentAlignment.TopCenter:
									{
										left = cells[y][x].Left + padding + ((cells[y][x].Width - cells[y][x].MeasuredWidth) / 2);
										top = cells[y][x].Top + padding;
										break;
									}
								case System.Drawing.ContentAlignment.TopRight:
									{
										left = cells[y][x].Left + padding + (cells[y][x].Width - cells[y][x].MeasuredWidth);
										top = cells[y][x].Top + padding;
										break;
									}
								default:
									{
										throw new System.Exception("Unknown ContentAlignment.");
									}
							}
							DrawString(graphics, text, textFont, foregroundColorBrush, left, top);
						}
					}
				}





				if (outerBorder)
				{
					// Left border
					graphics.DrawLine(borderColorPen, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Top + rectangle.Height);

					// Top border
					graphics.DrawLine(borderColorPen, rectangle.Left, rectangle.Top, rectangle.Left + rectangle.Width, rectangle.Top);

					// Right border
					graphics.DrawLine(borderColorPen, rectangle.Left + rectangle.Width, rectangle.Top, rectangle.Left + rectangle.Width, rectangle.Top + rectangle.Height);

					// Bottom border
					graphics.DrawLine(borderColorPen, rectangle.Left, rectangle.Top + rectangle.Height, rectangle.Left + rectangle.Width, rectangle.Top + rectangle.Height);
				}
			}
		}

	}
}
