using System;

namespace Statics_Simulator_Linux
{
	public class Moment
	{
		public double magnitude;
		public SPoint cPoint;	
		
		public Moment(double mag, SPoint pt)
		{
			cPoint = pt;
			magnitude = mag;
		}
	}
}

