using System;
using Data;
namespace Controllers
{
	public class Joint
	{
		public enum Type{BALL, HINGE, WELD};
		
		RigidBodyController controllerA;
		RigidBodyController controllerB;
		Type type;
		Point connectingPoint;
		
		public Joint (RigidBodyController A, RigidBodyController B, Type t, Point pt)
		{
			controllerA = A;
			controllerB = B;
			type = t;
			connectingPoint = pt;
		}
		public void addForce(Point force)
		{
			controllerA.addForce(force, connectingPoint);
			controllerB.addForce(force, connectingPoint);
		}
		/*public void addMoment(Point moment)
		{
			
			controllerA.addPivot(moment, connectingPoint);
			controllerB.addPivot(moment, connectingPoint);
		}*/
	}
}

