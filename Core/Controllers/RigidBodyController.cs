using System;
using Core;

namespace Core.Controllers
{
	public class RigidBodyController
	{
		DrawingObject rigidBody;
		
		
	
		public RigidBodyController (DrawingObject body)
		{
			rigidBody = body;
		}
		
		public void addForce(Tuple<PointDouble, PointDouble> force)
		{
			
		}
		
		public void addMoment(Tuple <PointDouble, double> moment)
		{
			
		}
		
	}
}

