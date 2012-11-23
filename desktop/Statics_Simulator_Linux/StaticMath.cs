using System;

namespace Statics_Simulator_Linux
{
	public class StaticMath
	{
		public StaticMath ()
		{
		}
		
		//==============================================================================
		// Takes any point, and a force point. Calculates the r vector from the point to 
		// the force, then takes the cross product of the r vector and the force vector
		//
		//==============================================================================
		public static double rCrossF(Point r, FPoint fpt)
		{
			//simple two dimentional cross product. 
			double rx = fpt.x - r.x;
			double ry = fpt.y - r.y;
			if (rx == 0.0 && ry == 0.0)
				return 0.0;
			
			double Fx = fpt.i();
			double Fy = fpt.j();
			
			//Is this right?
			return rx*Fy - ry*Fx;
		}
		
		//==============================================================================
		// Wrapper function for the main calcMoment function. Takes a string name, finds
		// 	the actual point on thebeam, and passes it to calcMoment
		//==============================================================================
		public static MPoint calcMoment(String name, Structure beam)
		{
			Point point = beam.getPoint(name);
			return calcMoment(point, beam);
		}
		//==============================================================================
		// Calcuates the moment and this particular point. Runs though all of the force 
		// points on the beam, and adds up the cross product of the point and the force. 
		// Then determines whether is should update the MPoint coming in, or return a 
		// new MPoint. 
		//==============================================================================
		public static MPoint calcMoment(Point point, Structure beam)
		{
			double total = 0;
			foreach(Point fpt in beam.points)
			{
				if (fpt.GetType() == typeof(FPoint)){
					total += StaticMath.rCrossF(point,(FPoint)fpt);
			
				}
			}
			if (point.GetType() == typeof(MPoint))
			{
				((MPoint)point).magnitude = total;
				return null;
			}	
			else
				return new MPoint(total, Point.Type.VARIABLE, point.x, point.y, point.name + "m");

		}
		
		//==============================================================================
		// Used to add a point to the beam. This way the calculations on the beam stay
		// in this class, and do not become tangled into the beam class. 
		//==============================================================================
		public static void addPointToStruct(Structure beam, Point point)
		{
			//if we are adding a force, then we need to clear all of the variable moments, because they are wrong
			if (point.GetType() == typeof(FPoint))
				beam.clearMoments();
			
			//add the point
			beam.addPoint(point);
			
			//if we are adding a force, then we need to re calc the moments
			if (point.GetType() == typeof(FPoint)){
				foreach (Point mpt in beam.points)
				{
					if (mpt.GetType() == typeof(MPoint) && mpt.type == Point.Type.VARIABLE)
						calcMoment(mpt, beam);
				}
			}
		}
		//==============================================================================
		// Given a known, fixed moment, and an unknown force. Calulate the unknown force. 
		// There must only be one unknown force on the beam, other than those that may be
		// on the moment point. 
		//==============================================================================
		public static FPoint calcForceUsingMoment(Structure beam, MPoint mpt, Point at)
		{
			
			return null;
		}
	}
}

