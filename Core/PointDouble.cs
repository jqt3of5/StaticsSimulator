using System;
using Cairo;
namespace Core
{
	public class PointDouble
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public PointDouble ()
		{
		}
		public PointDouble (double x)
		{
			X = x;
		}
		public PointDouble (double x, double y) : this(x)
		{
			Y = y;
		}
		public PointDouble (double x, double y, double z) :this (x,y)
		{
			Z = z;
		}

		public PointD toPointD()
		{
			return new PointD(X,Y);
		}
	}
}

