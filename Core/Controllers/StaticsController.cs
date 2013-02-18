using System;
using System.Collections.Generic;
using Core;

namespace Core.Controllers
{
	public class StaticsController
	{
		private List<RigidBodyController> _controllers; 
		private Dictionary<DrawingObject, RigidBodyController> _bodyToController;
		private Dictionary<PointDouble, DrawingObject> _pointToObject;
		private SpatialTree _spatialTree;
		
		public StaticsController (SpatialTree tree, Dictionary<PointDouble, DrawingObject> pointTobj)
		{
			
			_controllers = new List<RigidBodyController>();
			_bodyToController = new Dictionary<DrawingObject, RigidBodyController>();
			_pointToObject = pointTobj;
			_spatialTree = tree;
			
		}
		
		public void AddBody(DrawingObject body)
		{
			var newController = new RigidBodyController(body);
			_bodyToController.Add(body, newController);
			
			_controllers.Add(newController);
		}
		
		public void AddForce(Tuple<PointDouble, PointDouble> force)
		{
			DrawingObject body;
			if (!_pointToObject.TryGetValue(force.Item1, out body))
				return;
			
			RigidBodyController controller;
			if (_bodyToController.TryGetValue(body, out controller))
			{
				controller.addForce(force);
			}
			//need to check if this force will cause an interaction with any thing... like a joint. 
		}
		public void AddMoment(Tuple<PointDouble, double> moment)
		{
			DrawingObject body;
			if (!_pointToObject.TryGetValue(moment.Item1, out body))
				return;
			
			RigidBodyController controller;
			if (_bodyToController.TryGetValue(body, out controller))
			{
				controller.addMoment(moment);
			}
		}
		public void AddJoint(PointDouble pt1, PointDouble pt2)
		{
			DrawingObject body1, body2;
			if (!_pointToObject.TryGetValue(pt1, out body1) ||
			    !_pointToObject.TryGetValue(pt2, out body2))
				return;
			
		//	_spatialTree.remove(pt2);
			_pointToObject.Remove(pt2);
			
			var newLines = new List<Tuple<PointDouble, PointDouble>>();
			var oldLines = new List<Tuple<PointDouble, PointDouble>>();
			foreach(var line in body2.lines)
			{
				
				if (line.Item1 == pt2)
				{
					newLines.Add(new Tuple<PointDouble, PointDouble>(pt1, line.Item2));
					oldLines.Add(line);
				}
				else if (line.Item2 == pt2)
				{
					newLines.Add(new Tuple<PointDouble, PointDouble>(line.Item1, pt1));
					oldLines.Add(line);
				}
			}
			
			foreach (var line in oldLines)
			{
				body2.lines.Remove(line);
			}
			
			body2.lines.AddRange(newLines);
			body2.points.Remove(pt2);
			body2.points.Add(pt1);
			
		}
		public void AddAnchor(PointDouble pt)
		{
			DrawingObject body;
			if (!_pointToObject.TryGetValue(pt, out body))
				return;
			
			RigidBodyController controller;
			if (_bodyToController.TryGetValue(body, out controller))
			{
				//controller.addAnchor(pt);
			}
		}
	}
}

