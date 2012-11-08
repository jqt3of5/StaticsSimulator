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
		public ArrayList points;
		public ArrayList forces;
		public ArrayList moments;
		public ArrayList anchors;
		
		public Beam(SPoint A, SPoint B)
		{
			
			if (A.x < B.x){
				endPointA = A;
				endPointB = B;
			}else{
				endPointA = B;
				endPointB = A;
			}
			
			points = new ArrayList();
			forces = new ArrayList();
			moments = new ArrayList();
			anchors = new ArrayList();
			
			points.Add(endPointA);
			points.Add(endPointB);
			
			length = endPointA.distance(endPointB);
		}
		
		public SPoint getPoint(String name)
		{
			foreach(SPoint p in points)
			{
				if (p.name == name)
					return p;
			}
			return null;
		}
		
		public void addPoint(SPoint pt)
		{
			//need to verify that this point is along the beam
			double m = (endPointA.y-endPointB.y)/(endPointA.x-endPointB.x);
			
			//since this is floating point arithmetic, we will have errors. 
			// So compare within some amount of error
			if ((pt.y - endPointA.y) != m*(pt.x-endPointA.x) 
			    || pt.x < endPointA.x || pt.x > endPointB.x)
			{
				throw new Exception("Attempted to add a point not on the beam");
			}
			points.Add(pt);	
		}
	}
}

 