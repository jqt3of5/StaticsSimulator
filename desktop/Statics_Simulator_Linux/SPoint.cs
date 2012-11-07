using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public class SPoint
	{
		public double x;
		public double y;
		public String name; 
		
		public ArrayList forces;
		public Moment moment;
		
		public SPoint(double x, double y, String name)
		{
			this.x = x;
			this.y = y;
			this.name = name;
			
			forces = new ArrayList();
			
			moment = new Moment(0);
		}
		public bool hasUnknownForces()
		{
			foreach(Force f in forces)
			{
				if (!f.known)
					return true;
			}
			return false;
		}
		public void addForce(Force p)
		{
			forces.Add(p);
		}
	
		public double distance(SPoint pt)
		{
			return Math.Sqrt((x-pt.x)*(x-pt.x) + (y-pt.y)*(y-pt.y));
		}
	}
}

