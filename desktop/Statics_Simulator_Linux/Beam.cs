using System;
using System.Collections;

namespace Statics_Simulator_Linux
{
	public class Beam : Structure
	{
		
		public Point leftPoint;
		public Point rightPoint;
		public double length;
		
		public Beam(Point A, Point B)
		{
			if (A.x < B.x){
				leftPoint = A;
				rightPoint = B;
			}else{
				
				leftPoint = B;
				rightPoint = A;
			}
			cmPoint = new Point((rightPoint.x - leftPoint.x)/2.0 + leftPoint.x, 
			                    (rightPoint.y - leftPoint.y)/2.0 + leftPoint.x,"CM");
			
			points.Add(leftPoint);
			points.Add(rightPoint);
			points.Add(cmPoint);
			
			length = leftPoint.distance(rightPoint);
		}
		
		
		public override Point getPoint(String name)
		{
			foreach(Point p in points)
			{
				if (p.name == name)
					return p;
			}
			return null;
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
			points.Add(pt);
			
			
		}
	}
}

 