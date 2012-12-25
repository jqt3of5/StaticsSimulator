using System;

namespace Statics_Simulator_Linux
{
	public class Point
	{
		public double x,y,z;
		public Point ()
		{
			x = 0;
			y = 0;
			z = 0;
		}
		public Point(double x)
		{
			this.x = x;
			this.y = 0;
			this.z = 0;
		}
		public Point(double x, double y)
		{
			this.x = x;
			this.y = y;
			this.z = 0;
		}
		public Point(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		
		public static Point operator*(Point pt1, Point pt2)
		{
			double a,b,c;
			
			a = pt1.y*pt2.z - pt2.y*pt1.z;
			b = pt1.x*pt2.z - pt2.x*pt1.z;
			c = pt1.x*pt2.y - pt2.x*pt1.y;
			
			return new Point(a,-b,c);
		}
		public static Point operator-(Point pt1, Point pt2)
		{
			return new Point(pt1.x-pt2.x,pt1.y-pt2.y,pt1.z-pt2.z);
		}
		public static Point operator+(Point pt1, Point pt2)
		{
			return new Point(pt1.x+pt2.x,pt1.y+pt2.y,pt1.z+pt2.z);
		}
		public double mag()
		{
			return Math.Sqrt(x*x + y*y + z*z);
		}
		public static bool operator==(Point pt1, Point pt2)
		{
			return pt1.x == pt2.x && pt1.y == pt2.y && pt1.z == pt2.z;
		}
		
		public static bool operator!=(Point pt1, Point pt2)
		{
			return !(pt1==pt2);
		}
		
	}
}

