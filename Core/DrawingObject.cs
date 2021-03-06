using System;
using Cairo;
using System.Collections.Generic;
namespace Core
{
	public class DrawingObject
	{
		public List<Tuple<PointDouble, double>> _moments;
		public List<Tuple<PointDouble, double, double>> _forces;
		public List<PointDouble> points;
		public List<Tuple<PointDouble,PointDouble>> lines;
		public PointDouble _centerOfMass;



		protected PointDouble firstPoint;
		protected PointDouble lastPoint;



		public DrawingObject ()
		{
			points = new List<PointDouble>();
			lines = new List<Tuple<PointDouble, PointDouble>>();
			_moments = new List<Tuple<PointDouble, double>>();
			_forces = new List<Tuple<PointDouble, double, double>>();
			_centerOfMass = new PointDouble ();
		}
		public void AddMoment(Tuple<PointDouble, double> moment)
		{
			_moments.Add(moment);
		}
		public void AddForce(Tuple<PointDouble, double, double> force)
		{
			_forces.Add(force);
		}
		public void AddPoint(PointDouble point)
		{
			if (points.Count == 0)
				firstPoint = point;
			
			points.Add(point);
			
			if (points.Count > 1)
				lines.Add(new Tuple<PointDouble, PointDouble>(lastPoint, point));
			
			lastPoint = point;


			CalcCenterOfMass();
		}
		public void CalcCenterOfMass ()
		{
			_centerOfMass.X = 0;
			_centerOfMass.Y = 0;
			foreach (PointDouble pt in points) 
			{
				_centerOfMass.X += pt.X;
				_centerOfMass.Y += pt.Y;
			}
			
			_centerOfMass.X /= points.Count;
			_centerOfMass.Y /= points.Count;
		}

		
	}
}

