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
	public class LineGraph : GraphBase
	{

		public LineGraph()
		{
			this.DefaultStyleSheet = LoadStyleSheet("LineGraph.css");
		}

		protected override void RenderChart(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table)
		{
			styleStack.PushTag("LineGraph");



			string axisForeColor = (string)styleStack["AxisForeColor"];
			string axisLineColor = (string)styleStack["AxisLineColor"];
			string backColor = (string)styleStack["BackColor"];
			Bamboo.Css.Font font = (Bamboo.Css.Font)styleStack["Font"];
			string foreColor = (string)styleStack["ForeColor"];
			string gridLineColor = (string)styleStack["GridLineColor"];
			float lineWidth = System.Convert.ToSingle(styleStack["LineWidth"]);
			int padding = (int)styleStack["Padding"];
			bool showPoints = (bool)styleStack["ShowPoints"];
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

			rectangle = new System.Drawing.RectangleF(rectangle.Left + 1, rectangle.Top, rectangle.Width - 1, rectangle.Height - 1);



			// Plot
			float pointRadius = lineWidth * 2f; //TODO put in stylesheet.
			Line[] lines = new Line[table.Columns.Count - 1];
			for (int i = 1; i < table.Columns.Count; i++)
			{
				lines[i - 1].Pen = palette.Pen(colors[(i - 1) % colors.Length], lineWidth);
				lines[i - 1].Brush = palette.Brush(colors[(i - 1) % colors.Length]);

				lines[i - 1].Points = new Point[table.Rows.Count];
				for (int j = 0; j < table.Rows.Count; j++)
				{
					float value = System.Convert.ToSingle(table.Rows[j][i]);
					System.Drawing.PointF point = Coordinate(j, System.Convert.ToSingle(xRange.Min), System.Convert.ToSingle(xRange.Max), rectangle.Left, rectangle.Width, value, System.Convert.ToSingle(yRange.Min), System.Convert.ToSingle(yRange.Max), rectangle.Top, rectangle.Height);
					lines[i - 1].Points[j].X = point.X;
					lines[i - 1].Points[j].Y = point.Y;
				}
			}
			DrawLines(graphics, lines);
			if (showPoints)
			{
				for (int i = 0; i < lines.Length; i++)
				{
					System.Drawing.PointF[] points = new System.Drawing.PointF[lines[i].Points.Length - 1];
					for (int j = 1; j < points.Length; j++)
					{
						points[j - 1] = new System.Drawing.PointF(lines[i].Points[j].X, lines[i].Points[j].Y);
					}
					DrawPoints(graphics, lines[i].Brush, points, pointRadius);
				}
			}



			styleStack.PopTag();
		}

		private static void DrawLines(System.Drawing.Graphics graphics, Line[] lines)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				System.Drawing.PointF[] points = new System.Drawing.PointF[lines[i].Points.Length];
				for (int j = 0; j < lines[i].Points.Length; j++)
				{
					points[j].X = lines[i].Points[j].X;
					points[j].Y = lines[i].Points[j].Y;
				}
				if (lines[i].Pen.Width >= 2 && graphics.SmoothingMode != System.Drawing.Drawing2D.SmoothingMode.HighQuality)
				{
					graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				}
				graphics.DrawLines(lines[i].Pen, points);
			}
			graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
		}

		//TODO add this to DrawLines.
		private static void DrawPoints(System.Drawing.Graphics graphics, System.Drawing.Brush pointBrush, System.Drawing.PointF[] points, float radius)
		{
			float diameter = radius + radius;
			for (int i = 0; i < points.Length; i++)
			{
				System.Drawing.PointF point = points[i];
				graphics.FillEllipse(pointBrush, point.X - radius, point.Y - radius, diameter, diameter);
			}
		}

		//TODO use this instead of DrawPoints
		//private static void DrawCircles(System.Drawing.Graphics graphics, System.Drawing.Pen pen, System.Drawing.PointF[] points, float radius)
		//{
		//    float diameter = radius + radius;
		//    for (int i = 0; i < points.Length; i++)
		//    {
		//        System.Drawing.PointF point = points[i];
		//        graphics.DrawEllipse(pen, point.X - radius, point.Y - radius, diameter, diameter);
		//    }
		//}

		//private static void DrawCircles(System.Drawing.Graphics graphics, System.Drawing.Brush pointBrush, System.Drawing.Brush backgroundBrush, System.Drawing.PointF[] points, float radius)
		//{
		//    float diameter = radius + radius;
		//    for (int i = 0; i < points.Length; i++)
		//    {
		//        System.Drawing.PointF point = points[i];
		//        graphics.FillEllipse(pointBrush, point.X - radius, point.Y - radius, diameter, diameter);
		//        graphics.FillEllipse(backgroundBrush, point.X - (radius - 1), point.Y - (radius - 1), diameter - 2, diameter - 2);
		//    }
		//}

	}
}
