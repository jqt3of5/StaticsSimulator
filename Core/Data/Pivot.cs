using System;

namespace Data
{
	public class Pivot
	{	
		public Point force, moment, point;
		public enum Type {CM, HINGE};
		public Type type;
		public Pivot(Point force, Point moment, Point point, Type t)
		{
			this.force = force;
			this.moment = moment;
			this.point = point;
			this.type = t;
		}
		public Pivot (Point pt, Type t)
		{
			this.force = new Point();
			this.moment = new Point();
			this.point = pt;
			this.type = t;
		}
	}
}

