using System;
using Cairo;

using System.Collections.Generic;
namespace ViewModel
{
	public class DrawingObject
	{
		public List<PointD> points;
		public List<Tuple<PointD,PointD>> lines;
		private PointD firstPoint;
		private PointD lastPoint;
		
		public DrawingObject ()
		{
			points = new List<PointD>();
			lines = new List<Tuple<PointD, PointD>>();
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

