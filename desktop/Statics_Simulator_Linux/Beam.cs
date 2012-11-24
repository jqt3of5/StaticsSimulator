using System;
using System.Collections;

namespace Statics_Simulator_Linux
{
	public class Beam : RigidBody
	{
		
		public Point leftPoint;
		public Point rightPoint;
		public double length;
		
		public Beam(Point A, Point B)
		{
			if (A.x <= B.x){
				leftPoint = A;
				rightPoint = B;
			}else{
				
				leftPoint = B;
				rightPoint = A;
			}
			cmPoint = new Point((rightPoint.x - leftPoint.x)/2.0 + leftPoint.x, 
			                    (rightPoint.y - leftPoint.y)/2.0 + leftPoint.x,"CM");
			
			
			base.addPoint(leftPoint);
			base.addPoint(rightPoint);
			base.addPoint(cmPoint);
			
			length = leftPoint.distance(rightPoint);
		}
		
		
	
		public override void addPoint(Point pt)
		{
			//need to verify that this point is along the beam
			//calcualtes the slope of the beam
			double m = (leftPoint.y-rightPoint.y)/(leftPoint.x-rightPoint.x);
			
			//makes sure it is on the beam
			if ((pt.y - leftPoint.y) != m*(pt.x-leftPoint.x) 
			    || pt.x < leftPoint.x || pt.x > rightPoint.x)
			{
				throw new Exception("Attempted to add a point not on the beam");
			}
			
			//if this point is a force, the base class will clear and recalc all the moments
			//is this what I want? I don't know..
			base.addPoint(pt);
			
		}
	}
}

 