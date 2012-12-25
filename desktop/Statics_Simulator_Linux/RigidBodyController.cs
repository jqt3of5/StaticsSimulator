using System;

namespace Statics_Simulator_Linux
{
	public class RigidBodyController
	{
		RigidBody rigidBody;
		
		public delegate void consistencyCallback(Tuple<Point, Point> moment, Point mag);
		
		private consistencyCallback consistent;
		private consistencyCallback inconsistent;
		
	
		public RigidBodyController (RigidBody body)
		{
			rigidBody = body;
		}
		
		public void addForce(Point force, Point point)
		{
			rigidBody.addForce(force,point);	
			checkConsistency();
		}
		
		public void addPivot(Point moment, Point point)
		{
			rigidBody.addPivot(moment, point);
			checkConsistency ();
		}
		public void setConsistentCallback(consistencyCallback callback)
		{
			consistent = callback;
		}
		public void setInconsistentCallback(consistencyCallback callback)
		{
			inconsistent = callback;
		}
		
		
		public void checkConsistency()
		{
		//every time we add a force, we need to check for consistency
			foreach (Tuple<Point, Point> moment in rigidBody.pivots)
			{
				//calc the coupled moment for each point
				Point mag = new Point();
				foreach (Tuple<Point, Point> fvector in rigidBody.forces)
				{
					//dont care about the forces acting on this moment
					if (moment.Item2 == fvector.Item2)
						continue;
					mag = mag + (fvector.Item2-moment.Item2)*fvector.Item1;
				}
				
				if (mag != moment.Item1)
					inconsistent(moment, mag);
				else
					consistent(moment, mag);
			}	
			
		}
		
		
	}
}

