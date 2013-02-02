using System;
using Cairo;
using System.Collections.Generic;
namespace Core.UI
{
	public class DrawingObject
	{
		public List<Tuple<PointD, double>> _moments;
		public List<Tuple<PointD, double, double>> _forces;
		public List<PointD> points;
		public List<Tuple<PointD,PointD>> lines;
		private PointD firstPoint;
		private PointD lastPoint;
		
		public DrawingObject ()
		{
			points = new List<PointD>();
			lines = new List<Tuple<PointD, PointD>>();
			_moments = new List<Tuple<PointD, double>>();
			_forces = new List<Tuple<PointD, double, double>>();
		}
		public void AddMoment(Tuple<PointD, double> moment)
		{
			_moments.Add(moment);
		}
		public void AddForce(Tuple<PointD, double, double> force)
		{
			_forces.Add(force);
		}
		public void AddPoint(PointD point)
		{
			if (points.Count == 0)
				firstPoint = point;
			
			points.Add(point);
			
			if (points.Count > 1)
				lines.Add(new Tuple<PointD, PointD>(lastPoint, point));
			
			lastPoint = point;
			
		}
		public void Connect()
		{
			lines.Add(new Tuple<PointD, PointD>(firstPoint, lastPoint));
		}
		
	}
}

