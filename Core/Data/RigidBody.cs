using System;
using System.Collections;
using Core;
namespace Data
{
	public abstract class RigidBody
	{
		public Matrix geometry; //describes the geometry of the body, in the local coordinates 3d
		public ArrayList forces; // Tuple<Point,Point>
		public ArrayList pivots; //Tuple<Point, Point> 
		public Pivot cmPoint; //simply the center of mass, is a pivot
		
		public Matrix deformations; //Matrix to define the deformations to the body, due to the forces.
		public Matrix transformations; //Matrix to define the transformaions of the body.
		
		
		public RigidBody ()
		{
			geometry = null;
			deformations = null;
			transformations = null;
			
			forces = new ArrayList();
			pivots = new ArrayList();
			
			cmPoint = new Pivot(new Point(), Pivot.Type.CM);
			pivots.Add(cmPoint);
			
		}
		
		public RigidBody(Matrix g)
		{
			forces = new ArrayList();
			pivots = new ArrayList();
			
			geometry = g;
			deformations = new Matrix(g.getHeight(),g.getWidth());
			transformations = new Matrix(g.getHeight(),g.getWidth());
			
			cmPoint = new Pivot(new Point(), Pivot.Type.CM);
			pivots.Add(cmPoint);
		}
		public Pivot getCenterOfMass()
		{
			return cmPoint;
		}
		public void setCenterOfMass(Pivot pivot)
		{
			pivots.Remove(cmPoint);
			cmPoint = pivot;
			pivots.Add(cmPoint);
		}
		public virtual void addForce(Point force, Point point)
		{
			forces.Add(new Tuple<Point, Point>(force,point));
		}
		public virtual void addPivot(Pivot pivot)
		{
			pivots.Add(pivot);
		}
		
	
		//================================================================================
		//Moment functions
		
		
		
	}
}

