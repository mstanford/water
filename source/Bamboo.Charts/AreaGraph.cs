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
	public class AreaGraph : GraphBase
	{

		public AreaGraph()
		{
			this.DefaultStyleSheet = LoadStyleSheet("AreaGraph.css");
		}

		protected override void RenderChart(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table)
		{
			styleStack.PushTag("AreaGraph");



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
			bool isXRangeNumeric = IsNumeric(table.Rows[0][0]);
			int xAxisScale = CalculateScale(xRange);
			int xAxisPrecision = Math.Max(0, 0 - xAxisScale);


			string yAxisTitle = table.Columns[1].ToString();
			decimal y0 = System.Convert.ToDecimal(table.Rows[0][1]);
			decimal y_min = y0;
			decimal y_max = y0;
			for (int j = 1; j < table.Rows.Count; j++)
			{
				decimal y = System.Convert.ToDecimal(table.Rows[j][1]);
				if (y > y_max)
				{
					y_max = y;
				}
				else if (y < y_min)
				{
					y_min = y;
				}
			}
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

//			rectangle = new System.Drawing.RectangleF(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);



			// Plot
			float[,] values = new float[table.Rows.Count, 2];
			for (int i = 0; i < table.Rows.Count; i++)
			{
				values[i, 0] = i;
				values[i, 1] = System.Convert.ToSingle(table.Rows[i][1]);
			}
			DrawArea(graphics, palette.Brush(colors[0]), palette.Pen(colors[0]), System.Convert.ToSingle(xRange.Min), System.Convert.ToSingle(xRange.Max), rectangle.Left, rectangle.Width, System.Convert.ToSingle(yRange.Min), System.Convert.ToSingle(yRange.Max), rectangle.Top, rectangle.Height, values);



			styleStack.PopTag();
		}

		private static void DrawArea(System.Drawing.Graphics graphics, System.Drawing.Brush brush, System.Drawing.Pen pen, float x_min, float x_max, float left, float width, float y_min, float y_max, float top, float height, float[,] values)
		{
			int length = values.GetLength(0);
			System.Drawing.PointF[] points = new System.Drawing.PointF[length];
			for (int i = 0; i < length; i++)
			{
				points[i] = Coordinate(values[i, 0], x_min, x_max, left, width, values[i, 1], y_min, y_max, top, height);
			}

			System.Drawing.PointF[] areaPoints = new System.Drawing.PointF[points.Length + 2];
			System.Array.Copy(points, 0, areaPoints, 0, points.Length);
			areaPoints[points.Length] = Coordinate(x_max, x_min, x_max, left, width, y_min, y_min, y_max, top, height);
			areaPoints[points.Length + 1] = Coordinate(x_min, x_min, x_max, left, width, y_min, y_min, y_max, top, height);
			graphics.FillPolygon(brush, areaPoints);
		}

	}
}
