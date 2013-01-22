using System;
using System.Collections;

namespace Data
{
	public class Beam : RigidBody
	{
		
		public Beam(Point A, Point B)
		{
			Matrix max = new Matrix(2,2);
			if (A.x <= B.x){
				max[0][0] = A.x;
				max[0][1] = A.y;
				max[1][0] = B.x;
				max[1][1] = B.y;
			}else{
				max[0][0] = B.x;
				max[0][1] = B.y;
				max[1][0] = A.x;
				max[1][1] = A.y;
			}
			geometry = max;
		
			//the center of mass is half way bettween the two end points
			Pivot cm = new Pivot(new Point((A.x + B.y)/2.0,(A.x + B.y)/2.0), Pivot.Type.CM);
			setCenterOfMass(cm);
		}
		
		
		public bool onBeam(Point point)
		{
				//need to verify that this point is along the beam
			//calcualtes the slope of the beam
			double m = (geometry[0][1]-geometry[1][1])/(geometry[0][0]-geometry[1][0]);
			
			//makes sure it is on the beam
			if ((point.y - geometry[0][1]) != m*(point.x-geometry[0][0]) 
			    || point.x < geometry[0][0] || point.x > geometry[1][0])
			{
				return false;
			}
			
			return true;
		}
		public override void addPivot(Pivot pivot)
		{
			if (!onBeam(pivot.point))
				throw new Exception("Attempted to add a point not on the beam");
			
			base.addPivot(pivot);
		}
		public override void addForce(Point force, Point point)
		{
		
			if (!onBeam(point))
				throw new Exception("Attempted to add a point not on the beam");
			
			base.addForce(force, point);
			
		}
	}
}

 