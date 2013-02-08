using System;
using System.Collections;
using System.Collections.Generic;
using Cairo;
using Core;
namespace ViewModel
{
	public class DrawingWidgetModel
	{

		public List<DrawingObject> Objects{get; private set;}
		public DrawingObject TemporaryObject{ get; set; }

		public DrawingObject ActiveObject{ get; set; }
		public PointD ActivePoint{ get; set; }
		public SpatialTree _spatialTree;

		public DrawingWidgetModel ()

		{
			Objects = new List<DrawingObject>();
			_spatialTree = new SpatialTree();
			TemporaryObject = new DrawingObject();
		}
		
		public void commitTemporaryObject()
		{
			Objects.Add(TemporaryObject);
			_spatialTree.AddObject(TemporaryObject);
			ActiveObject = TemporaryObject;

			TemporaryObject = new DrawingObject();
		}
		
		
	}
}

