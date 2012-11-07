using System;

namespace Statics_Simulator_Linux
{
	public class StaticMath
	{
		public StaticMath ()
		{
		}
		
		public static Force totalForceAtPoint(SPoint point)
		{
			double Fx = 0, Fy = 0;
			foreach (Force f in point.forces)
			{
				if (!f.known)
					throw new Exception("Unknown Force");
				Fx += f.x();
				Fy += f.y();
			}
			
			return new Force(Math.Sqrt (Fx*Fx + Fy*Fy), Math.Atan2(Fy, Fx));
		}
		
		public static double rCrossF(SPoint r, SPoint fpt)
		{
			//simple two dimentional cross product. 
			//will add up all of the force vectors on
			//the f point before doing the cross product
			//throws an exception if a certain force is unknown
			double rx = fpt.x - r.x;
			double ry = fpt.y - r.y;
			
			Force force = totalForceAtPoint(fpt);
			double Fx = force.x();
			double Fy = force.y();
			
			//Is this right?
			return rx*Fy - ry*Fx;
		}
		
		public static double calcMoment(Beam beam, String name)
		{
			SPoint pt = beam.getConnectingPoint(name);
			//To calculate the moment, we need to know all of the forces 
			//that are being applied to the beam other than the current point. 
			//If not all forces are known, then we have to calculate 	
			//each of those forces. If we cannot infer those forces then 
			//we have no solution yet.
			double total = 0;
			foreach(SPoint fpt in beam.connectPoints)
			{
				if (fpt == pt)
					continue;
				total += StaticMath.rCrossF(pt,fpt);
			}
			
			return total;
		}
		
		public static Force calcForce(Beam beam, String name)
		{
			
			Force result;
			
			SPoint fpt = beam.getConnectingPoint(name);
		  	//search for a moment not on the current point. 
			//Preferably one that shares a point with an unknown force
			SPoint bestMomentPoint = null;
			foreach(SPoint point in beam.connectPoints)
			{
				//knowning the moment for the current point would be useless
				if (point == fpt)
					continue;
					
				if (point.moment != null && point.moment.known && point.hasUnknownForces())
				{
					bestMomentPoint = point;
					break;
				}
				
				if (point.moment != null && point.moment.known && bestMomentPoint == null)
				{
					bestMomentPoint = point;
				}
			}
			
			if (bestMomentPoint == null)
				throw new Exception("You need a known moment to calculate the force");
			
			double totalx = 0;
			double totaly = 0;
			double sumMoment = 0;
			Force temp;
			
			
			foreach(SPoint pt in beam.connectPoints)
			{
				//exclude the point we are trying to calculate, and the moment point
				if (fpt == pt || pt == bestMomentPoint)
					continue;
				//add up the total moments
				sumMoment += StaticMath.rCrossF(bestMomentPoint,pt);
				temp = totalForceAtPoint(pt);
				//sum the total forces in the x and y direction, 
				//totalx += temp.x(); 
				//totaly += temp.y(); 
			}
			
			//find our missing moment
			double missingMoment = bestMomentPoint.moment.magnitude - sumMoment;
			
	
			//calculate the missing force ad direction at this point
			double missingForce = Math.Abs(missingMoment/fpt.distance(bestMomentPoint));
			double missingForceDirection = Math.Atan2(beam.endPointB.y-beam.endPointA.y, 
			                                          beam.endPointB.x-beam.endPointA.x);
			
			if (missingMoment < 0)
				missingForceDirection += 3.0*Math.PI/2;
			else
				missingForceDirection += Math.PI/2;
			
			//creates a vector in the direction perpendicular to the beam. If we are given a theta, 
			//we have to do something different, I vote we make that some one elses problem.
			//they will have to decompose this vector into whatever vectors they have undefined at the point
			result = new Force(missingForce, missingForceDirection);
			
			return result;
		}
	}
}

