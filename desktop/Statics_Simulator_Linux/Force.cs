using System;

namespace Statics_Simulator_Linux
{
	public class Force : Prop
	{
		public double magnitude;
		public double direction;
		
		public Force() : base ("", false)
		{
			magnitude = 1;
			//this assumes directions are positive... :/
			direction = -1;
		}
		
		public Force(double dir) : base("", false)
		{
			magnitude = 1;
			direction = dir;
		}
		public Force(double mag, double dir) : base("", true)
		{
			magnitude = mag;
			direction = dir;
		}
		public Force (double mag, double dir, String name) : base(name,true)
		{
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

