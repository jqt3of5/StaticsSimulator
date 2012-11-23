using System;

namespace Statics_Simulator_Linux
{
	public class FPoint : Point
	{
		public double direction;
		public double magnitude;
		
		public FPoint(double dir, double x, double y, String n) : base(x,y, Type.VARIABLE ,n)
		{
			direction = dir;
			magnitude = 0;	
			known = false;
		}
		public FPoint(double mag, double dir,  Type t, double x, double y,String n) : base(x,y,t,n)
		{
			direction = dir;
			magnitude = mag;
		}
		
		public double i()
		{
			if (known == false)
				throw new Exception(name + " Force is unknown");
			return magnitude*Math.Cos(direction);
		}
		public double j()
		{
			if (known == false)
				throw new Exception(name + " Force is unknown");
			return magnitude*Math.Sin(direction);
		}
		
	}
}

