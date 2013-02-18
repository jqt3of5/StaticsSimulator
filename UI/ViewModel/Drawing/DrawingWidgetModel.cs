using System;
using System.Collections;
using System.Collections.Generic;
using Cairo;
using Core;
using Core.Controllers;
namespace ViewModel
{
	public class DrawingWidgetModel
	{

		public List<DrawingObject> Objects{get; private set;}

		
		public DrawingObject TemporaryObject{ get; set; }
		public DrawingObject ActiveObject{ get; set; }
		public PointDouble ActivePoint{ get; set; }
		
		public SpatialTree SpatialTree { get; set; }
		public StaticsController Controller{ get; set; }
		public Dictionary<PointDouble, DrawingObject> PointToParent{ get; set; }

		public DrawingWidgetModel ()
		{
			Objects = new List<DrawingObject>();
			SpatialTree = new SpatialTree();
			PointToParent = new Dictionary<PointDouble, DrawingObject>();
			
			//this coupling here makes me nervous... Is there a better way for the controller to have access to this information? 
			Controller = new StaticsController(SpatialTree, PointToParent);
		}
		
		public void commitTemporaryObject ()
		{
			Objects.Add (TemporaryObject);
			SpatialTree.AddObject (TemporaryObject);
			foreach (PointDouble pt in TemporaryObject.points) 
			{
				PointToParent.Add(pt, TemporaryObject);
			}

			ActiveObject = TemporaryObject;

		}
		
		
	}
}

