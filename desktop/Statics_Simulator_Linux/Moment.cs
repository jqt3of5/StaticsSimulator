using System;

namespace Statics_Simulator_Linux
{
	public class Moment : Prop
	{
		public double magnitude;
		
		public Moment()
		{
			magnitude = 0;
		}
		public Moment(double mag) : base("", true)
		{
			magnitude = mag;
		}
		public Moment (double mag, String name) : base(name,true)
		{
			magnitude = mag;
		}
	}
}

