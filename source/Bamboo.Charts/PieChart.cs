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
	public class PieChart : ChartBase
	{

		public PieChart()
		{
			this.DefaultStyleSheet = LoadStyleSheet("PieChart.css");
		}

		protected override void RenderChart(System.Drawing.Graphics graphics, Palette palette, Bamboo.Css.StyleStack styleStack, System.Drawing.RectangleF rectangle, string title, Bamboo.DataStructures.Table table)
		{
			styleStack.PushTag("PieChart");



			string backColor = (string)styleStack["BackColor"];
			Bamboo.Css.Font font = (Bamboo.Css.Font)styleStack["Font"];
			string foreColor = (string)styleStack["ForeColor"];
			int padding = (int)styleStack["Padding"];
			string[] colors = (string[])styleStack["Colors"];

			//TODO put in stylesheet.
			System.Drawing.Font titleFont = palette.Font(font.Name, font.Size * 1.2f, System.Drawing.FontStyle.Bold);

			//TODO put in stylesheet.
			System.Drawing.Font labelFont = palette.Font(font.Name, font.Size, System.Drawing.FontStyle.Bold);

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

			//TODO
			// Legend
//			rectangle = new System.Drawing.RectangleF(rectangle.Left, rectangle.Top, rectangle.Width - padding - padding, rectangle.Height);



			Slice[] slices = new Slice[table.Rows.Count];
			float sum = 0;
			for (int i = 0; i < table.Rows.Count; i++)
			{
				Bamboo.DataStructures.Tuple row = table.Rows[i];

				slices[i].Label = row[0].ToString();

				float value = System.Convert.ToSingle(row[1]);
				slices[i].Value = value;

				sum += value;
			}
			sum /= 360;



			float startAngle = -90;
			for (int i = 0; i < slices.Length; i++)
			{
				slices[i].StartAngle = startAngle;

				float sweepAngle = slices[i].Value / sum;
				slices[i].SweepAngle = sweepAngle;

				double startPlusHalfSweepRadian = DegreeToRadian(startAngle + (sweepAngle / 2));
				slices[i].LabelCos = (float)Math.Cos(startPlusHalfSweepRadian);
				slices[i].LabelSin = (float)Math.Sin(startPlusHalfSweepRadian);

				System.Drawing.SizeF sizef = MeasureString(graphics, slices[i].Label, labelFont);
				slices[i].LabelWidth = sizef.Width;
				slices[i].LabelHeight = sizef.Height;

				slices[i].Adjacent = slices[i].LabelCos * slices[i].LabelWidth;
				slices[i].Opposite = slices[i].LabelSin * slices[i].LabelHeight;
				slices[i].Hypotenuse = (float)Math.Sqrt((slices[i].Adjacent * slices[i].Adjacent) + (slices[i].Opposite * slices[i].Opposite));

				slices[i].Brush = palette.Brush(colors[i % colors.Length]);

				startAngle += sweepAngle;
			}



			float x_min = 0;
			float x_max = 0;
			float y_min = 0;
			float y_max = 0;
			for (int i = 0; i < slices.Length; i++)
			{
				x_min = Math.Min(x_min, slices[i].Adjacent);
				x_max = Math.Max(x_max, slices[i].Adjacent);
				y_min = Math.Min(y_min, slices[i].Opposite);
				y_max = Math.Max(y_max, slices[i].Opposite);
			}
			float x_offset = Math.Max(Math.Abs(x_min), x_max);
			float y_offset = Math.Max(Math.Abs(y_min), y_max);
			rectangle = new System.Drawing.RectangleF(rectangle.Left + x_offset, rectangle.Top + y_offset, rectangle.Width - (x_offset * 2), rectangle.Height - (y_offset * 2));



			float halfInnerWidth = rectangle.Width / 2;
			float halfInnerHeight = rectangle.Height / 2;
			float radius = Math.Min(halfInnerWidth, halfInnerHeight);
			float diameter = radius + radius;
			float x = rectangle.Left + halfInnerWidth - radius;
			float y = rectangle.Top + halfInnerHeight - radius;

			if (radius > 0)
			{
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
				for (int i = 0; i < slices.Length; i++)
				{
					graphics.FillPie(slices[i].Brush, x, y, diameter, diameter, slices[i].StartAngle, slices[i].SweepAngle);

					float radiusPlusHalfHypotenuse = radius + slices[i].Hypotenuse / 2;
					float labelLeft = x + radius - (slices[i].LabelWidth / 2) + (slices[i].LabelCos * radiusPlusHalfHypotenuse);
					float labelTop = y + radius - (slices[i].LabelHeight / 2) + (slices[i].LabelSin * radiusPlusHalfHypotenuse);

					//TODO use a style for Brush
					DrawString(graphics, slices[i].Label, labelFont, foreColorBrush, labelLeft, labelTop);
				}
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
			}



			styleStack.PopTag();
		}

		private double DegreeToRadian(double angle)
		{
			return angle * Math.PI / 180.0;
		}

	}
}
