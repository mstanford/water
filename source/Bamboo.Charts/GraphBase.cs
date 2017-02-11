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
	public abstract class GraphBase : ChartBase
	{

		public GraphBase()
        {
        }

		protected System.Drawing.RectangleF DrawYAxisTitle(System.Drawing.Graphics graphics, System.Drawing.RectangleF rectangle, string yAxisTitle, System.Drawing.Font axisFont, System.Drawing.SizeF yAxisTitleSize, float y, System.Drawing.Brush axisForeColorBrush)
		{
			graphics.TranslateTransform(rectangle.Left, rectangle.Top + ((rectangle.Height - y) / 2) + (yAxisTitleSize.Width / 2));
			graphics.RotateTransform(-90.0f);
			DrawString(graphics, yAxisTitle, axisFont, axisForeColorBrush, 0.0f, 0.0f);
			graphics.ResetTransform();
			return new System.Drawing.RectangleF(rectangle.Left + yAxisTitleSize.Height, rectangle.Top, rectangle.Width - yAxisTitleSize.Height, rectangle.Height);
		}

		protected System.Drawing.RectangleF DrawXAxisTitle(System.Drawing.Graphics graphics, System.Drawing.RectangleF rectangle, string xAxisTitle, System.Drawing.Font axisFont, System.Drawing.SizeF xAxisTitleSize, float x, System.Drawing.Brush axisForeColorBrush)
		{
			DrawString(graphics, xAxisTitle, axisFont, axisForeColorBrush, rectangle.Left + x + ((rectangle.Width - x) / 2) - (xAxisTitleSize.Width / 2), rectangle.Top + rectangle.Height - xAxisTitleSize.Height);
			return new System.Drawing.RectangleF(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height - xAxisTitleSize.Height);
		}

		protected void DrawYAxis(System.Drawing.Graphics graphics, System.Drawing.RectangleF rectangle, Tick[] yAxisTicks, float tickSize, System.Drawing.Font axisTickFont, System.Drawing.Pen gridLineColorPen, System.Drawing.Pen axisLineColorPen, System.Drawing.Brush axisForeColorBrush, bool drawGridline)
		{
			for (int i = 0; i < yAxisTicks.Length; i++)
			{
				if (drawGridline && Math.Ceiling(yAxisTicks[i].Point.Y) < Math.Ceiling(rectangle.Top + rectangle.Height))
				{
					// Draw gridline.
					graphics.DrawLine(gridLineColorPen, yAxisTicks[i].Point.X, yAxisTicks[i].Point.Y, yAxisTicks[i].Point.X + rectangle.Width, yAxisTicks[i].Point.Y);
				}

				//TODO
				//If zero is on the y-axis draw a darkened grid line.

				graphics.DrawLine(axisLineColorPen, yAxisTicks[i].Point.X, yAxisTicks[i].Point.Y, yAxisTicks[i].Point.X - tickSize, yAxisTicks[i].Point.Y);
				System.Drawing.SizeF sizef = MeasureString(graphics, yAxisTicks[i].Label, axisTickFont);
				DrawString(graphics, yAxisTicks[i].Label, axisTickFont, axisForeColorBrush, yAxisTicks[i].Point.X - (sizef.Width + tickSize), yAxisTicks[i].Point.Y - (sizef.Height / 2f));
			}
			graphics.DrawLine(axisLineColorPen, rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Top + rectangle.Height);
		}

		protected void DrawXAxis(System.Drawing.Graphics graphics, System.Drawing.RectangleF rectangle, Tick[] xAxisTicks, float tickSize, System.Drawing.Font axisTickFont, System.Drawing.Pen gridLineColorPen, System.Drawing.Pen axisLineColorPen, System.Drawing.Brush axisForeColorBrush, bool drawGridline)
		{
			for (int i = 0; i < xAxisTicks.Length; i++)
			{
				if (drawGridline && xAxisTicks[i].Point.X > rectangle.Left)
				{
					// Draw gridline.
					graphics.DrawLine(gridLineColorPen, xAxisTicks[i].Point.X, xAxisTicks[i].Point.Y, xAxisTicks[i].Point.X, xAxisTicks[i].Point.Y - rectangle.Height);
				}

				graphics.DrawLine(axisLineColorPen, xAxisTicks[i].Point.X, xAxisTicks[i].Point.Y, xAxisTicks[i].Point.X, xAxisTicks[i].Point.Y + tickSize);
				System.Drawing.SizeF sizef = MeasureString(graphics, xAxisTicks[i].Label, axisTickFont);
				DrawString(graphics, xAxisTicks[i].Label, axisTickFont, axisForeColorBrush, xAxisTicks[i].Point.X - (sizef.Width / 2f), xAxisTicks[i].Point.Y + tickSize);
			}
			graphics.DrawLine(axisLineColorPen, rectangle.Left, rectangle.Top + rectangle.Height, rectangle.Left + rectangle.Width, rectangle.Top + rectangle.Height);
		}

		protected virtual Tick[] XAxisTicks(
			System.Drawing.RectangleF rectangle, 
			System.Drawing.Graphics graphics, 
			System.Drawing.Font font,
			Range range,
			bool isRangeNumeric,
			int xAxisScale, 
			int xAxisPrecision,
			Bamboo.DataStructures.Table table)
		{
			float labelWidth = Math.Max(MeasureString(graphics, range.Min.ToString("N" + xAxisPrecision), font).Width, MeasureString(graphics, range.Max.ToString("N" + xAxisPrecision), font).Width);

			int availableSpaces = (int)Math.Floor(rectangle.Width / labelWidth);
			if (availableSpaces <= 1)
			{
				return new Tick[0];
			}
			else
			{
				decimal min = Math.Floor(range.Min);
				decimal max = Math.Ceiling(range.Max);
				decimal precision = Power(10, xAxisPrecision);
				decimal neededSpaces = (max - min) * precision;

				decimal interval = 1 / precision;
				int multiplier = 2;
				while (neededSpaces > availableSpaces)
				{
					if ((neededSpaces / multiplier) <= availableSpaces)
					{
						interval *= multiplier;
						break;
					}
					multiplier++;
				}



				List<Tick> ticks = new List<Tick>();
				Tick tick;
				decimal value = min;
				while (value <= max)
				{
					tick = new Tick();
					tick.Point = Coordinate(System.Convert.ToSingle(value), System.Convert.ToSingle(min), System.Convert.ToSingle(max), rectangle.Left, rectangle.Width, 0, 0, 1, rectangle.Top, rectangle.Height);
					if (isRangeNumeric)
					{
						tick.Label = value.ToString("N" + xAxisPrecision);
					}
					else
					{
						tick.Label = table.Rows[(int)value][0].ToString();
					}
					ticks.Add(tick);
					value += interval;
				}
				return ticks.ToArray();
			}
		}

		protected virtual Tick[] YAxisTicks(
			System.Drawing.RectangleF rectangle,
			System.Drawing.Graphics graphics,
			System.Drawing.Font font,
			Range range,
			bool isRangeNumeric,
			int yAxisScale,
			int yAxisPrecision,
			Bamboo.DataStructures.Table table)
		{
			float labelHeight = Math.Max(MeasureString(graphics, range.Min.ToString("N" + yAxisPrecision), font).Height, MeasureString(graphics, range.Max.ToString("N" + yAxisPrecision), font).Height);

			int availableSpaces = (int)Math.Floor(rectangle.Height / labelHeight);
			if (availableSpaces <= 1)
			{
				return new Tick[0];
			}
			else
			{
				decimal min = Math.Floor(range.Min);
				decimal max = Math.Ceiling(range.Max);
				decimal precision = Power(10, yAxisPrecision);
				decimal neededSpaces = (max - min) * precision;

				decimal interval = 1 / precision;
				int multiplier = 2;
				if (neededSpaces > 1000)
				{
					multiplier *= (int)neededSpaces / 20;
				}
				while (neededSpaces > availableSpaces)
				{
					if ((neededSpaces / multiplier) <= availableSpaces)
					{
						interval *= multiplier;
						break;
					}
					multiplier++;
				}



				List<Tick> ticks = new List<Tick>();
				Tick tick;
				decimal value = min;
				while (value <= max)
				{
					tick = new Tick();
					tick.Point = Coordinate(0, 0, 1, rectangle.Left, 0, System.Convert.ToSingle(value), System.Convert.ToSingle(min), System.Convert.ToSingle(max), rectangle.Top, rectangle.Height);
					if (isRangeNumeric)
					{
						tick.Label = value.ToString("N" + yAxisPrecision);
					}
					else
					{
						throw new System.Exception("Unhandled.");
					}
					ticks.Add(tick);
					value += interval;
				}
				return ticks.ToArray();
			}
		}

		//TODO this needs work.
		protected static int CalculateScale(Range range)
		{
			int scale = 0;

			if ((range.Max - range.Min) <= 10)
			{
				scale = -1;
			}
			else if ((range.Max - range.Min) <= 100)
			{
				scale = 0;
			}
			else
			{
				scale = 1;
			}

			return scale;
		}

		protected static bool IsNumeric(object value)
		{
			return (
				value is int || 
				value is long || 
				value is float || 
				value is double || 
				value is decimal);
		}

		//TODO DELETE
		//protected static decimal Floor2(decimal min, int precision)
		//{
		//    decimal p = Power(10, precision);
		//    decimal d = min * p;
		//    decimal d2 = decimal.Truncate(d);
		//    return d2 / p;
		//}

		//protected static decimal Ceiling2(decimal max, int precision)
		//{
		//    decimal p = Power(10, precision);
		//    decimal d = max * p;
		//    decimal d2 = decimal.Truncate(d);
		//    if (d2 < d)
		//    {
		//        d2 += 1;
		//    }
		//    return d2 / p;
		//}

		protected static System.Drawing.PointF Coordinate(float x, float x_min, float x_max, float left, float width, float y, float y_min, float y_max, float top, float height)
		{
			return new System.Drawing.PointF(left + (((x - x_min) / (x_max - x_min)) * width), (top + height) - (((y - y_min) / (y_max - y_min)) * height));
		}

		private static decimal Power(decimal x, int y)
		{
			decimal z = 1;
			for (int i = 0; i < y; i++)
			{
				z *= x;
			}
			return z;
		}

		/*
		private static void Render(System.Drawing.Graphics graphics, int left, int top, int width, int height, System.Drawing.Pen axisPen, System.Drawing.Brush axisBrush, System.Drawing.Font axisFont, Orientation orientation, double min, double max, int tickSize)
		{
			if (orientation == Orientation.Bottom)
			{
				int ntick = Math.Max(1, (int)(width / Math.Max(MeasureString(graphics, min.ToString(), axisFont).Width * 2, MeasureString(graphics, max.ToString(), axisFont).Width * 2)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				double graphmin = Math.Floor(min / d) * d;
				double graphmax = Math.Ceiling(max / d) * d;
				double graphrange = graphmax - graphmin;
				int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

				graphics.DrawLine(axisPen, left, top, left + width, top);
				for (int i = 0; i <= ntick; i++)
				{
					double f = ((double)i / (double)ntick);

					int tickleft = left + (int)(width * f);
					graphics.DrawLine(axisPen, tickleft, top, tickleft, top + tickSize);

					string label = (graphmin + (f * graphrange)).ToString("N" + nfrac);
					graphics.DrawLine(axisPen, tickleft, top, tickleft, top + tickSize);
					System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
					DrawString(graphics, label, axisFont, axisBrush, tickleft - (sizef.Width / 2f), top + tickSize);
				}
			}
			else if (orientation == Orientation.Left)
			{
				int ntick = Math.Max(1, (int)(height / Math.Max(MeasureString(graphics, min.ToString(), axisFont).Height * 2, MeasureString(graphics, max.ToString(), axisFont).Height * 2)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				double graphmin = Math.Floor(min / d) * d;
				double graphmax = Math.Ceiling(max / d) * d;
				double graphrange = graphmax - graphmin;
				int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

				graphics.DrawLine(axisPen, left, top, left, top + height);
				for (int i = 0; i <= ntick; i++)
				{
					double f = ((double)i / (double)ntick);

					int ticktop = top + (int)(height * f);
					graphics.DrawLine(axisPen, left, ticktop, left - tickSize, ticktop);

					string label = (graphmin + (f * graphrange)).ToString("N" + nfrac);
					graphics.DrawLine(axisPen, left, ticktop, left - tickSize, ticktop);
					System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
					DrawString(graphics, label, axisFont, axisBrush, left - tickSize - sizef.Width - 3, ticktop - (sizef.Height / 2f));
				}
			}
			else if (orientation == Orientation.Right)
			{
				int ntick = Math.Max(1, (int)(height / Math.Max(MeasureString(graphics, min.ToString(), axisFont).Height * 2, MeasureString(graphics, max.ToString(), axisFont).Height * 2)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				double graphmin = Math.Floor(min / d) * d;
				double graphmax = Math.Ceiling(max / d) * d;
				double graphrange = graphmax - graphmin;
				int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

				graphics.DrawLine(axisPen, left, top, left, top + height);
				for (int i = 0; i <= ntick; i++)
				{
					double f = ((double)i / (double)ntick);

					int ticktop = top + (int)(height * f);
					graphics.DrawLine(axisPen, left, ticktop, left + tickSize, ticktop);

					string label = (graphmin + (f * graphrange)).ToString("N" + nfrac);
					graphics.DrawLine(axisPen, left, ticktop, left + tickSize, ticktop);
					System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
					DrawString(graphics, label, axisFont, axisBrush, left + tickSize, ticktop - (sizef.Height / 2f));
				}
			}
			else if (orientation == Orientation.Top)
			{
				int ntick = Math.Max(1, (int)(width / Math.Max(MeasureString(graphics, min.ToString(), axisFont).Width * 2, MeasureString(graphics, max.ToString(), axisFont).Width * 2)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				double graphmin = Math.Floor(min / d) * d;
				double graphmax = Math.Ceiling(max / d) * d;
				double graphrange = graphmax - graphmin;
				int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

				graphics.DrawLine(axisPen, left, top, left + width, top);
				for (int i = 0; i <= ntick; i++)
				{
					double f = ((double)i / (double)ntick);

					int tickleft = left + (int)(width * f);
					graphics.DrawLine(axisPen, tickleft, top, tickleft, top - tickSize);

					string label = (graphmin + (f * graphrange)).ToString("N" + nfrac);
					graphics.DrawLine(axisPen, tickleft, top, tickleft, top - tickSize);
					System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
					DrawString(graphics, label, axisFont, axisBrush, tickleft - (sizef.Width / 2f), top - tickSize - sizef.Height);
				}
			}
		}

		// From Paul S. Heckbert, 'Nice Numbers For Graph Labels'
		private static double nicenum(double x, bool round)
		{
			double exp = Math.Floor(Math.Log10(x));
			double f = x / Math.Pow(10, exp);
			double nf;
			if (round)
			{
				if (f < 1.5)
				{
					nf = 1;
				}
				else if (f < 3)
				{
					nf = 2;
				}
				else if (f < 7)
				{
					nf = 5;
				}
				else
				{
					nf = 10;
				}
			}
			else
			{
				if (f <= 1)
				{
					nf = 1;
				}
				else if (f <= 2)
				{
					nf = 2;
				}
				else if (f <= 5)
				{
					nf = 5;
				}
				else
				{
					nf = 10;
				}
			}
			return nf * Math.Pow(10, exp);
		}
		*/

		/*
		private static void Render(System.Drawing.Graphics graphics, int left, int top, int width, int height, System.Drawing.Pen axisPen, System.Drawing.Brush axisBrush, System.Drawing.Font axisFont, Orientation orientation, double min, double max, int tickSize, int labelPadding)
		{
			if (orientation == Orientation.Bottom)
			{
				int ntick = Math.Max(1, (int)(width / Math.Max(MeasureString(graphics, ToDateTime(min).ToString(), axisFont).Width + labelPadding, MeasureString(graphics, ToDateTime(max).ToString(), axisFont).Width + labelPadding)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				if (d != double.NaN && !double.IsInfinity(d))
				{
					double graphmin = Math.Floor(min / d) * d;
					double graphmax = Math.Ceiling(max / d) * d;
					double graphrange = graphmax - graphmin;
					int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

					graphics.DrawLine(axisPen, left, top, left + width, top);
					for (int i = 0; i <= ntick; i++)
					{
						double f = ((double)i / (double)ntick);

						int tickleft = left + (int)(width * f);
						graphics.DrawLine(axisPen, tickleft, top, tickleft, top + tickSize);

						string label = ToDateTime(graphmin + (f * graphrange)).ToString();
						graphics.DrawLine(axisPen, tickleft, top, tickleft, top + tickSize);
						System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
						DrawString(graphics, label, axisFont, axisBrush, tickleft - (sizef.Width / 2f), top + tickSize);
					}
				}
			}
			else if (orientation == Orientation.Left)
			{
				int ntick = Math.Max(1, (int)(height / Math.Max(MeasureString(graphics, ToDateTime(min).ToString(), axisFont).Height + labelPadding, MeasureString(graphics, ToDateTime(max).ToString(), axisFont).Height + labelPadding)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				if (d != double.NaN && !double.IsInfinity(d))
				{
					double graphmin = Math.Floor(min / d) * d;
					double graphmax = Math.Ceiling(max / d) * d;
					double graphrange = graphmax - graphmin;
					int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

					graphics.DrawLine(axisPen, left, top, left, top + height);
					for (int i = 0; i <= ntick; i++)
					{
						double f = ((double)i / (double)ntick);

						int ticktop = top + (int)(height * f);
						graphics.DrawLine(axisPen, left, ticktop, left - tickSize, ticktop);

						string label = ToDateTime(graphmin + (f * graphrange)).ToString();
						graphics.DrawLine(axisPen, left, ticktop, left - tickSize, ticktop);
						System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
						DrawString(graphics, label, axisFont, axisBrush, left - tickSize - sizef.Width - 3, ticktop - (sizef.Height / 2f));
					}
				}
			}
			else if (orientation == Orientation.Right)
			{
				int ntick = Math.Max(1, (int)(height / Math.Max(MeasureString(graphics, ToDateTime(min).ToString(), axisFont).Height + labelPadding, MeasureString(graphics, ToDateTime(max).ToString(), axisFont).Height + labelPadding)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				if (d != double.NaN && !double.IsInfinity(d))
				{
					double graphmin = Math.Floor(min / d) * d;
					double graphmax = Math.Ceiling(max / d) * d;
					double graphrange = graphmax - graphmin;
					int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

					graphics.DrawLine(axisPen, left, top, left, top + height);
					for (int i = 0; i <= ntick; i++)
					{
						double f = ((double)i / (double)ntick);

						int ticktop = top + (int)(height * f);
						graphics.DrawLine(axisPen, left, ticktop, left + tickSize, ticktop);

						string label = ToDateTime(graphmin + (f * graphrange)).ToString();
						graphics.DrawLine(axisPen, left, ticktop, left + tickSize, ticktop);
						System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
						DrawString(graphics, label, axisFont, axisBrush, left + tickSize, ticktop - (sizef.Height / 2f));
					}
				}
			}
			else if (orientation == Orientation.Top)
			{
				int ntick = Math.Max(1, (int)(width / Math.Max(MeasureString(graphics, ToDateTime(min).ToString(), axisFont).Width + labelPadding, MeasureString(graphics, ToDateTime(max).ToString(), axisFont).Width + labelPadding)));
				double range = nicenum(max - min, false);
				double d = nicenum(range / (ntick - 1), true);
				if (d != double.NaN && !double.IsInfinity(d))
				{
					double graphmin = Math.Floor(min / d) * d;
					double graphmax = Math.Ceiling(max / d) * d;
					double graphrange = graphmax - graphmin;
					int nfrac = (int)Math.Max(0 - Math.Floor(Math.Log10(d)), 0);

					graphics.DrawLine(axisPen, left, top, left + width, top);
					for (int i = 0; i <= ntick; i++)
					{
						double f = ((double)i / (double)ntick);

						int tickleft = left + (int)(width * f);
						graphics.DrawLine(axisPen, tickleft, top, tickleft, top - tickSize);

						string label = ToDateTime(graphmin + (f * graphrange)).ToString();
						graphics.DrawLine(axisPen, tickleft, top, tickleft, top - tickSize);
						System.Drawing.SizeF sizef = MeasureString(graphics, label, axisFont);
						DrawString(graphics, label, axisFont, axisBrush, tickleft - (sizef.Width / 2f), top - tickSize - sizef.Height);
					}
				}
			}
		}

		private static DateTime ToDateTime(double d)
		{
			return new DateTime((long)d);
		}
		*/

	}
}
