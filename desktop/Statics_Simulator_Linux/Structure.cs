using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public abstract class Structure
	{
		public ArrayList points;
		public Point cmPoint;
		
		public Structure ()
		{
			points = new ArrayList();
		}
		
		public abstract void addPoint(Point point);
		public abstract Point getPoint(String name);
		public void clearMoments()
		{
			foreach (Point p in points)
			{
				if (p.GetType() == typeof(MPoint))
					p.known = false;
			}
		}
	}
}

