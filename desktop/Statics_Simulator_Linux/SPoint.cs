using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public class SPoint
	{
		public double x;
		public double y;
		public String name; 
		
		public SPoint(double x, double y, String name)
		{
			this.x = x;
			this.y = y;
			this.name = name;
		}
	
		public double distance(SPoint pt)
		{
			return Math.Sqrt((x-pt.x)*(x-pt.x) + (y-pt.y)*(y-pt.y));
		}
	}
}

