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
	public class PivotTable : TableBase
	{
		private List<string> _columnColumns;
		private List<string> _rowColumns;
		private List<string> _dataColumns;

		public PivotTable()
		{
			this.DefaultStyleSheet = LoadStyleSheet("Table.css");
		}

		public List<string> ColumnColumns
		{
			get { return this._columnColumns; }
			set { this._columnColumns = value; }
		}

		public List<string> RowColumns
		{
			get { return this._rowColumns; }
			set { this._rowColumns = value; }
		}

		public List<string> DataColumns
		{
			get { return this._dataColumns; }
			set { this._dataColumns = value; }
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



			List<Cell[]> cellsList = new List<Cell[]>();
			Cell[] cells;
			int x;
			int y = 0;


			//
			// Get a table of column headers.
			//
			int[] columnHeaders = new int[this._columnColumns.Count];
			for (int i = 0; i < columnHeaders.Length; i++)
			{
				columnHeaders[i] = i;
			}
			Bamboo.DataStructures.Table columnHeadersTable = Bamboo.Sql2.Iterators.GroupByIterator.Group(table, columnHeaders);


			//
			// Calculate the width of the cell arrays (i.e., table rows).
			//
			int width = (this._columnColumns.Count == 0) ? this._rowColumns.Count + this._dataColumns.Count : this._rowColumns.Count + (columnHeadersTable.Rows.Count * this._dataColumns.Count);


			//
			// Create the column headers.
			//
			styleStack.PushTag("Header");
			backColor = (string)styleStack["BackColor"];
			foreColor = (string)styleStack["ForeColor"];
			System.Drawing.ContentAlignment textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
			for (int i = 0; i < this._columnColumns.Count; i++)
			{
				cells = new Cell[width];
				cellsList.Add(cells);
				x = this._rowColumns.Count;
				Bamboo.DataStructures.Tuple row = null;
				for (int j = 0; j < columnHeadersTable.Rows.Count; j++)
				{
					Bamboo.DataStructures.Tuple row2 = columnHeadersTable.Rows[j];
					if (row == null || !row.Equals(0, row2, 0, i + 1))
					{
						//TODO push header
//						Bamboo.Css.StyleRule styleRule = GetStyle(styleSheet, "Header", this2._columnColumns, row2, i + 1);

						cells[x].HasValue = true;
						cells[x].Type = CellType.Header;
						cells[x].Colspan = this._dataColumns.Count;
						cells[x].Rowspan = 1;
						cells[x].Value = row2[i];
//						if (styleRule == null)
//						{
							cells[x].BackgroundColor = backColor;
							cells[x].ForegroundColor = foreColor;
							cells[x].TextAlign = textAlign;
//						}
//						else
//						{
//							cells[x].BackgroundColor = (string)styleRule["BackColor"];
//							cells[x].ForegroundColor = (string)styleRule["ForeColor"];
//							cells[x].TextAlign = (System.Drawing.ContentAlignment)styleRule["TextAlign"];
//						}
					}
					else
					{
						int k = 1;
						int index = x - k;
						k++;
						while (cells[index].Colspan == 0)
						{
							index = x - k;
							k++;
						}
						cells[index].Colspan += this._dataColumns.Count;
					}
					row = row2;
					x += this._dataColumns.Count;
				}
				y++;
			}

			cells = new Cell[width];
			cellsList.Add(cells);
			x = this._rowColumns.Count;
			for (int i = 0; i < Math.Max(columnHeadersTable.Rows.Count, 1); i++)
			{
				for (int k = 0; k < this._dataColumns.Count; k++)
				{
//					Bamboo.Css.StyleRule styleRule = GetStyle(styleSheet, new Statibase.Tuple(new object[] { this2._dataColumns[k] }), 0, 1);

					cells[x].HasValue = true;
					cells[x].Type = CellType.Header;
					cells[x].Colspan = 1;
					cells[x].Rowspan = 1;
					cells[x].Value = this._dataColumns[k];
//					if (styleRule == null)
//					{
						cells[x].BackgroundColor = backColor;
						cells[x].ForegroundColor = foreColor;
						cells[x].TextAlign = textAlign;
//					}
//					else
//					{
//						cells[x].BackgroundColor = (string)styleRule["BackColor"];
//						cells[x].ForegroundColor = (string)styleRule["ForeColor"];
//						cells[x].TextAlign = (System.Drawing.ContentAlignment)styleRule["TextAlign"];
//					}
					x++;
				}
			}
			y++;
			styleStack.PopTag();



			backColor = (string)styleStack["BackColor"];

			//
			// Add the empty cell if necessary.
			//
			if (this._rowColumns.Count > 0)
			{
				cellsList[0][0].HasValue = true;
				cellsList[0][0].Type = CellType.Empty;
				cellsList[0][0].Colspan = this._rowColumns.Count;
				cellsList[0][0].Rowspan = this._columnColumns.Count + 1;
				cellsList[0][0].BackgroundColor = backColor;
			}



			//
			// Get a table of row headers.
			//
			int[] rowHeadersPlusData = new int[this._columnColumns.Count + this._rowColumns.Count + this._dataColumns.Count];
			int ii = 0;
			for (int i = 0; i < this._rowColumns.Count; i++)
			{
				rowHeadersPlusData[ii] = this._columnColumns.Count + i;
				ii++;
			}
			for (int i = 0; i < this._columnColumns.Count; i++)
			{
				rowHeadersPlusData[ii] = i;
				ii++;
			}
			for (int i = 0; i < this._dataColumns.Count; i++)
			{
				rowHeadersPlusData[ii] = this._columnColumns.Count + this._rowColumns.Count + i; ;
				ii++;
			}
			table = Bamboo.Sql2.Iterators.OrderByIterator.Sort(table, rowHeadersPlusData);


			//
			// Fill in the row headers and data.
			//
			if (this._rowColumns.Count > 0)
			{
				int[] rowHeaderIndexes = new int[this._rowColumns.Count];
				int[] yPositions = new int[columnHeadersTable.Rows.Count + 1];

				cells = new Cell[width];
				styleStack.PushTag("Header");
				backColor = (string)styleStack["BackColor"];
				foreColor = (string)styleStack["ForeColor"];
				textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
				for (int j = 0; j < this._rowColumns.Count; j++)
				{
//					Bamboo.Css.StyleRule styleRule = GetStyle(styleSheet, table.Rows[0], this2._columnColumns.Count + j, 1);

					cells[j].HasValue = true;
					cells[j].Type = CellType.Header;
					cells[j].Colspan = 1;
					cells[j].Rowspan = 1;
					cells[j].Value = table.Rows[0][this._columnColumns.Count + j];
//					if (styleRule == null)
//					{
						cells[j].BackgroundColor = backColor;
						cells[j].ForegroundColor = foreColor;
						cells[j].TextAlign = textAlign;
//					}
//					else
//					{
//						cells[j].BackgroundColor = (string)styleRule["BackColor"];
//						cells[j].ForegroundColor = (string)styleRule["ForeColor"];
//						cells[j].TextAlign = (System.Drawing.ContentAlignment)styleRule["TextAlign"];
//					}
					rowHeaderIndexes[j] = cellsList.Count;
				}
				styleStack.PopTag();

				styleStack.PushTag("Cell");
				backColor = (string)styleStack["BackColor"];
				foreColor = (string)styleStack["ForeColor"];
				textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
				for (int k = this._rowColumns.Count; k < width; k++)
				{
					cells[k].HasValue = true;
					cells[k].Type = CellType.Cell;
					cells[k].Colspan = 1;
					cells[k].Rowspan = 1;
					cells[k].BackgroundColor = backColor;
					cells[k].ForegroundColor = foreColor;
					cells[k].TextAlign = textAlign;
				}
				styleStack.PopTag();

				for (int l = 0; l < yPositions.Length; l++)
				{
					yPositions[l] = cellsList.Count;
				}
				cellsList.Add(cells);


				Bamboo.DataStructures.Tuple previousRow = null;
				for (int i = 0; i < table.Rows.Count; i++)
				{
					Bamboo.DataStructures.Tuple row = table.Rows[i];

					if (previousRow != null)
					{
						for (int j = 0; j < this._rowColumns.Count; j++)
						{
							//TODO use an array of values instead of fetching the last row.
							if (!cellsList[rowHeaderIndexes[j]][j].Value.Equals(row[this._columnColumns.Count + j]))
							{
//								Bamboo.Css.StyleRule styleRule = GetStyle(styleSheet, row, this2._columnColumns.Count + j, 1);
								styleStack.PushTag("Header");
								backColor = (string)styleStack["BackColor"];
								foreColor = (string)styleStack["ForeColor"];
								textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];

								// The row keys have changed.  Write a new row.
								cells = new Cell[width];

								cells[j].HasValue = true;
								cells[j].Type = CellType.Header;
								cells[j].Colspan = 1;
								cells[j].Rowspan = 1;
								cells[j].Value = row[this._columnColumns.Count + j];
//								if (styleRule == null)
//								{
									cells[j].BackgroundColor = backColor;
									cells[j].ForegroundColor = foreColor;
									cells[j].TextAlign = textAlign;
//								}
//								else
//								{
//									cells[j].BackgroundColor = (string)styleRule["BackColor"];
//									cells[j].ForegroundColor = (string)styleRule["ForeColor"];
//									cells[j].TextAlign = (System.Drawing.ContentAlignment)styleRule["TextAlign"];
//								}
								rowHeaderIndexes[j] = cellsList.Count;

								for (int k = 0; k < j; k++)
								{
									cellsList[rowHeaderIndexes[k]][k].Rowspan++;
								}
								for (int k = (j + 1); k < this._rowColumns.Count; k++)
								{
									cells[k].HasValue = true;
									cells[k].Type = CellType.Header;
									cells[k].Colspan = 1;
									cells[k].Rowspan = 1;
									cells[k].Value = row[this._columnColumns.Count + k];
//									if (styleRule == null)
//									{
										cells[k].BackgroundColor = backColor;
										cells[k].ForegroundColor = foreColor;
										cells[k].TextAlign = textAlign;
//									}
//									else
//									{
//										cells[k].BackgroundColor = (string)styleRule["BackColor"];
//										cells[k].ForegroundColor = (string)styleRule["ForeColor"];
//										cells[k].TextAlign = (System.Drawing.ContentAlignment)styleRule["TextAlign"];
//									}
									rowHeaderIndexes[k] = cellsList.Count;
								}
								styleStack.PopTag();

								styleStack.PushTag("Cell");
								backColor = (string)styleStack["BackColor"];
								foreColor = (string)styleStack["ForeColor"];
								textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
								for (int k = this._rowColumns.Count; k < width; k++)
								{
									cells[k].HasValue = true;
									cells[k].Type = CellType.Cell;
									cells[k].Colspan = 1;
									cells[k].Rowspan = 1;
									cells[k].BackgroundColor = backColor;
									cells[k].ForegroundColor = foreColor;
									cells[k].TextAlign = textAlign;
								}
								styleStack.PopTag();

								for (int l = 0; l < yPositions.Length; l++)
								{
									yPositions[l] = cellsList.Count;
								}
								cellsList.Add(cells);
								break;
							}
						}
					}



					if (columnHeadersTable.Rows.Count == 0)
					{
						int columnNumber = 0;
						x = (columnNumber * this._dataColumns.Count) + this._rowColumns.Count;
						y = yPositions[columnNumber];

						while (y >= cellsList.Count)
						{
							cells = new Cell[width];

							for (int k = 0; k < this._rowColumns.Count; k++)
							{
								cellsList[rowHeaderIndexes[k]][k].Rowspan++;
							}

							styleStack.PushTag("Cell");
							backColor = (string)styleStack["BackColor"];
							foreColor = (string)styleStack["ForeColor"];
							textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
							for (int k = this._rowColumns.Count; k < width; k++)
							{
								cells[k].HasValue = true;
								cells[k].Type = CellType.Cell;
								cells[k].Colspan = 1;
								cells[k].Rowspan = 1;
								cells[k].BackgroundColor = backColor;
								cells[k].ForegroundColor = foreColor;
								cells[k].TextAlign = textAlign;
							}
							styleStack.PopTag();

							cellsList.Add(cells);
						}

						for (int j = 0; j < this._dataColumns.Count; j++)
						{
							cellsList[y][x + j].Value = row[this._columnColumns.Count + this._rowColumns.Count + j];
						}
						yPositions[columnNumber]++;
					}
					else
					{
						for (int m = 0; m < columnHeadersTable.Rows.Count; m++)
						{
							Bamboo.DataStructures.Tuple columnRow = columnHeadersTable.Rows[m];
							if (row.Equals(0, columnRow, 0, this._columnColumns.Count))
							{
								int columnNumber = m;
								x = (columnNumber * this._dataColumns.Count) + this._rowColumns.Count;
								y = yPositions[columnNumber];

								while (y >= cellsList.Count)
								{
									cells = new Cell[width];

									for (int k = 0; k < this._rowColumns.Count; k++)
									{
										cellsList[rowHeaderIndexes[k]][k].Rowspan++;
									}

									styleStack.PushTag("Cell");
									backColor = (string)styleStack["BackColor"];
									foreColor = (string)styleStack["ForeColor"];
									textAlign = (System.Drawing.ContentAlignment)styleStack["TextAlign"];
									for (int k = this._rowColumns.Count; k < width; k++)
									{
										cells[k].HasValue = true;
										cells[k].Type = CellType.Cell;
										cells[k].Colspan = 1;
										cells[k].Rowspan = 1;
										cells[k].BackgroundColor = backColor;
										cells[k].ForegroundColor = foreColor;
										cells[k].TextAlign = textAlign;
									}
									styleStack.PopTag();

									cellsList.Add(cells);
								}

								for (int j = 0; j < this._dataColumns.Count; j++)
								{
									cellsList[y][x + j].Value = row[this._columnColumns.Count + this._rowColumns.Count + j];
								}
								yPositions[columnNumber]++;
								break;
							}
						}
					}



					previousRow = row;
				}
			}


			Render(graphics, palette, styleStack, cellsList, rectangle);



			styleStack.PopTag();
		}

		private static Bamboo.Css.StyleRule GetStyle(
			Bamboo.Css.StyleSheet styleSheet,
			string rule,
			System.Collections.IList keys,
			Bamboo.DataStructures.Tuple values, 
			int length)
		{
			foreach (Bamboo.Css.StyleRule styleRule in styleSheet.Rules)
			{
				if (rule.Equals(styleRule.Name) && styleRule is Bamboo.Css.KeyValueStyleRule)
				{
					Bamboo.Css.KeyValueStyleRule keyValueStyleRule = (Bamboo.Css.KeyValueStyleRule)styleRule;
					for (int i = 0; i < length; i++)
					{
						if (keys[i].Equals(keyValueStyleRule.Key) && values[i].Equals(keyValueStyleRule.Value))
						{
							return styleRule;
						}
					}
				}
			}
			return null;
		}

	}
}
