using System;
using System.Collections;
namespace Statics_Simulator_Linux
{
	public abstract class RigidBody
	{
		public Matrix geometry; //describes the geometry of the body, in the local coordinates 3d
		public ArrayList forces; // Tuple<Point,Point>
		public ArrayList pivots; //Tuple<Point, Point> 
		public Tuple<Point, Point> cmPoint; //simply the center of mass, is a pivot
		
		public Matrix deformations; //Matrix to define the deformations to the body, due to the forces.
		public Matrix transformations; //Matrix to define the transformaions of the body.
		
		
		public RigidBody ()
		{
			geometry = null;
			deformations = null;
			transformations = null;
			
			forces = new ArrayList();
			pivots = new ArrayList();
			
			pivot = new Tuple<Point, Point>(null,null);
			pivots.Add(pivot);
			
		}
		
		public RigidBody(Matrix g)
		{
			forces = new ArrayList();
			pivots = new ArrayList();
			
			geometry = g;
			deformations = new Matrix(g.getHeight(),g.getWidth());
			transformations = new Matrix(g.getHeight(),g.getWidth());
			
			cmPoint = new Tuple<Point, Point>(new Point(),null);
			pivots.Add(cmPoint);
		}
		public Tuple<Point, Point> getCenterOfMass()
		{
			return cmPoint;
		}
		public void setCenterOfMass(Point moment, Point point)
		{
			cmPoint.Item1 = moment;
			cmPoint.Item2 = point;
		}
		public virtual void addForce(Point force, Point point)
		{
			forces.Add(new Tuple<Point, Point>(force,point));
		}
		public virtual void addPivot(Point moment, Point point)
		{
			Tuple<Point, Point> m = new Tuple<Point, Point>(moment,point);
			pivots.Add(m);
		}
		
	
		//================================================================================
		//Moment functions
		
		
		
	}
}

