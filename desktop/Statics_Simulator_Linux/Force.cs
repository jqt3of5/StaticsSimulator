using System;

namespace Statics_Simulator_Linux
{
	public class Force
	{
		public double magnitude;
		public double direction;
		public SPoint cPoint;
		
		public Force(double dir, SPoint pt) 
		{
			cPoint = pt;
			magnitude = 1;
			direction = dir;
		}
		public Force(double mag, double dir, SPoint pt)
		{
			cPoint = pt;
			magnitude = mag;
			direction = dir;
		}
		
		public double x()
		{
			return magnitude*Math.Cos(direction);
		}
		public double y()
		{
			return magnitude*Math.Sin(direction);
		}
		
	}
}

