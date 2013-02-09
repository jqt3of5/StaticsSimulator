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
		public PointDouble ActivePoint{ get; set; }
		public SpatialTree _spatialTree;

		public Dictionary<PointDouble, DrawingObject> PointToParent{ get; set; }


		public DrawingWidgetModel ()

		{
			Objects = new List<DrawingObject>();
			_spatialTree = new SpatialTree();
			TemporaryObject = new DrawingObject();
			PointToParent = new Dictionary<PointDouble, DrawingObject>();
		}
		
		public void commitTemporaryObject ()
		{
			Objects.Add (TemporaryObject);
			_spatialTree.AddObject (TemporaryObject);
			foreach (PointDouble pt in TemporaryObject.points) 
			{
				PointToParent.Add(pt, TemporaryObject);
			}

			ActiveObject = TemporaryObject;

			TemporaryObject = new DrawingObject();
		}
		
		
	}
}

