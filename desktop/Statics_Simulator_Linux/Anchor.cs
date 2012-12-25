using System;

namespace Statics_Simulator_Linux
{
	public class Anchor
	{
		enum Type {HINGE, WELD, 
		RigidBodyController body;
		Point force, moment, point;
		
		public Anchor (RigidBodyController b, Point pt)
		{
			body = b;
			force = new Point();
			moment = new Point();
			point = pt;
			
			body.addForce(force, point);
			body.addMoment(moment, point);
			body.setPivot(point);
		}
	}
}

