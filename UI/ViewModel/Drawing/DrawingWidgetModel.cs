using System;
using System.Collections;
using System.Collections.Generic;
using Cairo;
namespace ViewModel
{
	public class DrawingWidgetModel
	{

		public List<DrawingObject> Forces{ get; private set;}
		public List<PointD> Moments{ get; private set;}
		public List<DrawingObject> Objects{get; private set;}
		public DrawingWidgetModel ()
		{
			Objects = new List<DrawingObject>();
			Forces = new List<DrawingObject>();
			Moments = new List<PointD>();
		}
		
		public void commitTemporaryAsForce(DrawingObject body)
		{
			Forces.Add(body);
		}
		public void commitTemporaryObject(DrawingObject body)
		{
			Objects.Add(body);
		}
		
		
	}
}

