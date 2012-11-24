using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public class Point
	{
		public enum Type{FIXED, VARIABLE, NA};
		
		public double x;
		public double y;
		public String name; 
		public Type type;
		public Boolean known; 
		
		public Point(double x, double y, String name)
		{
			this.x = x;
			this.y = y;
			this.name = name;
			this.type = Type.NA;
			known = true;
		}
	
		
		public double distance(Point pt)
		{
			return Math.Sqrt((x-pt.x)*(x-pt.x) + (y-pt.y)*(y-pt.y));
		}
		
		public double rCrossF(FPoint f)
		{
			//simple two dimentional cross product. 
			double rx = f.x - x;
			double ry = f.y - y;
			if (rx == 0.0 && ry == 0.0)
				return 0.0;
			
			double Fx = f.i();
			double Fy = f.j();
			
			//Is this right?
			return rx*Fy - ry*Fx;
		}
	}
}

