using System;

namespace Statics_Simulator_Linux
{
	public class MPoint : Point
	{
		public double magnitude;
		public MPoint(double x, double y, String n) : base(x,y,Point.Type.VARIABLE,n)
		{ 
			known = false;
		}
		public MPoint(double mag, Type t, double x, double y, string n) : base(x,y,t,n)
		{
			magnitude = mag;
			type = t;
		}
	}
}

