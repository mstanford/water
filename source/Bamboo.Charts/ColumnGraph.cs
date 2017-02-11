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
	public class ColumnGraph : GraphBase
	{

		public ColumnGraph()
		{
			this.DefaultStyleSheet = LoadStyleSheet("ColumnGraph.css");
		}

		protected override void RenderChart(System.Drawing.Graphics graphics, Charts.Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table)
		{
			styleStack.PushTag("ColumnGraph");



			string axisForeColor = (string)styleStack["AxisForeColor"];
			string axisLineColor = (string)styleStack["AxisLineColor"];
			string backColor = (string)styleStack["BackColor"];
			Bamboo.Css.Font font = (Bamboo.Css.Font)styleStack["Font"];
			string foreColor = (string)styleStack["ForeColor"];
			string gridLineColor = (string)styleStack["GridLineColor"];
			int padding = (int)styleStack["Padding"];
			float tickSize = System.Convert.ToSingle(styleStack["TickSize"]);
			string[] colors = (string[])styleStack["Colors"];

			//TODO put in stylesheet
			System.Drawing.Font titleFont = palette.Font(font.Name, font.Size * 1.2f, System.Drawing.FontStyle.Bold);

			//TODO put in stylesheet
			System.Drawing.Font axisTickFont = palette.Font(font.Name, font.Size * .66f); //TODO put in stylesheet.
			System.Drawing.Font axisTitleFont = palette.Font(font.Name, font.Size, System.Drawing.FontStyle.Bold);

			System.Drawing.Brush axisForeColorBrush = palette.Brush(axisForeColor);
			System.Drawing.Pen axisLineColorPen = palette.Pen(axisLineColor);
			System.Drawing.Pen backColorPen = palette.Pen(backColor);
			System.Drawing.Brush backColorBrush = palette.Brush(backColor);
			System.Drawing.Brush foreColorBrush = palette.Brush(foreColor);
			System.Drawing.Pen gridLineColorPen = palette.Pen(gridLineColor);



			// Background
			graphics.DrawRectangle(backColorPen, rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);
			graphics.FillRectangle(backColorBrush, rectangle);
			rectangle = new System.Drawing.RectangleF(rectangle.Left + padding, rectangle.Top + padding, rectangle.Width - padding - padding, rectangle.Height - padding - padding);

			// Title
			if (title != null)
			{
				rectangle = DrawTitle(title, titleFont, foreColorBrush, graphics, rectangle);
			}

			//TODO
			// Legend
			rectangle = new System.Drawing.RectangleF(rectangle.Left, rectangle.Top, rectangle.Width - padding - padding, rectangle.Height);



			string xAxisTitle = table.Columns[0].ToString();
			decimal x_min = 0;
			decimal x_max = table.Rows.Count - 1;
			Range xRange = new Range(x_min, x_max);
			bool isXRangeNumeric = true;
			int xAxisScale = 0;
			int xAxisPrecision = 0;


			string yAxisTitle = table.Columns[1].ToString();
			decimal y0 = System.Convert.ToDecimal(table.Rows[0][1]);
			decimal y_min = y0;
			decimal y_max = y0;
			for (int j = 1; j < table.Rows.Count; j++)
			{
				for (int i = 1; i < table.Columns.Count; i++)
				{
					decimal y = System.Convert.ToDecimal(table.Rows[j][i]);
					if (y > y_max)
					{
						y_max = y;
					}
					else if (y < y_min)
					{
						y_min = y;
					}
				}
			}
			y_min = Math.Min(0, y_min);
			Range yRange = new Range(y_min, y_max);
			bool isYRangeNumeric = true;
			int yAxisScale = CalculateScale(yRange);
			int yAxisPrecision = Math.Max(0, 0 - yAxisScale);
			float maxYTickLabelWidth = Math.Max(
				MeasureString(graphics, y_min.ToString("N" + yAxisPrecision), axisTickFont).Width,
				MeasureString(graphics, y_max.ToString("N" + yAxisPrecision), axisTickFont).Width);



			System.Drawing.SizeF yAxisTitleSize = MeasureString(graphics, yAxisTitle, axisTitleFont);
			System.Drawing.SizeF xAxisTitleSize = MeasureString(graphics, xAxisTitle, axisTitleFont);
			float maxXTickLabelHeight = MeasureString(graphics, "A", axisTickFont).Height;

			// Y-Axis Title
			rectangle = DrawYAxisTitle(graphics, rectangle, yAxisTitle, axisTitleFont, yAxisTitleSize, xAxisTitleSize.Height + maxXTickLabelHeight + tickSize, axisForeColorBrush);

			// X-Axis Title
			rectangle = DrawXAxisTitle(graphics, rectangle, xAxisTitle, axisTitleFont, xAxisTitleSize, yAxisTitleSize.Height + maxYTickLabelWidth + tickSize, axisForeColorBrush);

			rectangle = new System.Drawing.RectangleF(rectangle.Left + maxYTickLabelWidth + tickSize, rectangle.Top, rectangle.Width - (maxYTickLabelWidth + tickSize), rectangle.Height - (maxXTickLabelHeight + tickSize));

			DrawYAxis(graphics, rectangle, YAxisTicks(rectangle, graphics, axisTickFont, yRange, isYRangeNumeric, yAxisScale, yAxisPrecision, table), tickSize, axisTickFont, gridLineColorPen, axisLineColorPen, axisForeColorBrush, true);
			DrawXAxis(graphics, rectangle, XAxisTicks(rectangle, graphics, axisTickFont, xRange, isXRangeNumeric, xAxisScale, xAxisPrecision, table), tickSize, axisTickFont, gridLineColorPen, axisLineColorPen, axisForeColorBrush, true);

			rectangle = new System.Drawing.RectangleF(rectangle.Left + 1, rectangle.Top, rectangle.Width, rectangle.Height - 1);



			// Plot
			float barWidth = rectangle.Width / table.Rows.Count;
			float bottom = (float)Math.Ceiling(rectangle.Top) + (float)Math.Ceiling(rectangle.Height);
			float x = rectangle.Left;
			Bar[] bars = new Bar[table.Rows.Count];
			for (int i = 0; i < table.Rows.Count; i++)
			{
				Bamboo.DataStructures.Tuple row = table.Rows[i];

				System.Drawing.PointF point = Coordinate(i, System.Convert.ToSingle(xRange.Min), System.Convert.ToSingle(xRange.Max), rectangle.Left, rectangle.Width, System.Convert.ToSingle(row[1]), System.Convert.ToSingle(yRange.Min), System.Convert.ToSingle(yRange.Max), rectangle.Top, rectangle.Height);

				bars[i].X = x;
				bars[i].Y = (float)Math.Ceiling(point.Y);
				bars[i].Width = barWidth;
				bars[i].Height = (float)Math.Ceiling(bottom - point.Y);
				bars[i].Brush = palette.Brush(colors[i % colors.Length]);

				x += barWidth; //TODO this isn't pretty.
			}
			DrawBars(graphics, bars);



			styleStack.PopTag();
		}

		protected override Tick[] XAxisTicks(System.Drawing.RectangleF rectangle, System.Drawing.Graphics graphics, System.Drawing.Font font, Range range, bool isRangeNumeric, int xAxisScale, int xAxisPrecision, Bamboo.DataStructures.Table table)
		{
			Tick[] xAxisTicks = new Tick[table.Rows.Count];
			float barWidth = rectangle.Width / table.Rows.Count;
			float x = rectangle.Left;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				Bamboo.DataStructures.Tuple row = table.Rows[i];
				System.Drawing.PointF point = Coordinate(i, System.Convert.ToSingle(range.Min), System.Convert.ToSingle(range.Max), rectangle.Left, rectangle.Width, 0, 0, 1, rectangle.Top, rectangle.Height);
				xAxisTicks[i].Point = new System.Drawing.PointF(x + (barWidth / 2), point.Y);
				xAxisTicks[i].Label = row[0].ToString();
				x += barWidth; //TODO this isn't pretty.
			}
			return xAxisTicks;
		}

		private static void DrawBars(System.Drawing.Graphics graphics, Bar[] bars)
		{
			for (int i = 0; i < bars.Length; i++)
			{
				graphics.FillRectangle(bars[i].Brush, bars[i].X, bars[i].Y, bars[i].Width, bars[i].Height);
			}
		}

	}
}
