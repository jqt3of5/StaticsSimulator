using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public class Point
	{
		public double x;
		public double y;
		public String name; 
		public enum Type{FIXED, VARIABLE, NA};
		public Type type;
		public Boolean known; 
		
		public Point(double x, double y, String name) : this(x,y,Type.NA,name)
		{
			
		}
		public Point(double x, double y, Type t, String name)
		{
			this.x = x;
			this.y = y;
			this.name = name;
			this.type = t;
			known = true;
		}
	
		public double distance(Point pt)
		{
			return Math.Sqrt((x-pt.x)*(x-pt.x) + (y-pt.y)*(y-pt.y));
		}
	}
}

