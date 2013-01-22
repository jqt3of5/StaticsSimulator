using System;
using Data;
namespace Controllers
{
	public class RigidBodyController
	{
		RigidBody rigidBody;
		
		public delegate void consistencyCallback(Pivot have, Pivot need);
		
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
		
		public void addPivot(Pivot pivot)
		{
			rigidBody.addPivot(pivot);
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
		
		public Point calcForceAbout(Pivot pivot)
		{	
#warning  "not implemented"
			return null;
		}
		
		public Point calcMomentAbout(Pivot pivot)
		{
#warning "not implemented"
			return null;
		}
		
		public void checkConsistency()
		{
		//every time we add a force, we need to check for consistency
			foreach (Pivot pivot in rigidBody.pivots)
			{
				Point force = calcForceAbout(pivot);
				Point moment = calcMomentAbout(pivot);
			
				if (force != pivot.force || moment != pivot.moment)
				{
					inconsistent(pivot, new Pivot(force, moment, pivot.point, pivot.type));
				}
				else{
					consistent(pivot, null);
				}	
			}	
			Console.WriteLine("");
		}
	}
}

