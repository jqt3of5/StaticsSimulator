using System;
using System.Collections;

namespace Statics_Simulator_Linux
{
	public class Beam
	{
		public SPoint endPointA;
		public SPoint endPointB;
		
		public double  length;
		
		//this should probably be a hash table....
		public ArrayList connectPoints;
		
		public Beam(SPoint A, SPoint B)
		{
			
			if (A.x < B.x){
				endPointA = A;
				endPointB = B;
			}else{
				endPointA = B;
				endPointB = A;
			}
			
			connectPoints = new ArrayList();
			
			connectPoints.Add(endPointA);
			connectPoints.Add(endPointB);
			
			length = endPointA.distance(endPointB);
		}
		
		public SPoint getConnectingPoint(String name)
		{
			foreach(SPoint p in connectPoints)
			{
				if (p.name == name)
					return p;
			}
			return null;
		}
		public void addConnectingPoint(SPoint pt)
		{
			//need to verify that this point is along the beam
			double m = (endPointA.y-endPointB.y)/(endPointA.x-endPointB.x);
			
			//since this is floating point arithmetic, we will have errors. 
			// So compare within some amount of error
			if (Math.Abs((pt.y - endPointA.y) - m*(pt.x-endPointA.x)) < .01 
			    && pt.x >= endPointA.x && pt.x <= endPointB.x)
			{
				connectPoints.Add(pt);	
			}
			else
				throw new Exception("Attempted to add a point not on the beam");
		}
	}
}

 