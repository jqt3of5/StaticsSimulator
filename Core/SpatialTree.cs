using System;
using Cairo;
using System.Collections.Generic;
using Core.UI;

namespace Core
{
	public class Node
	{
		//Which axis it is split on
		public enum Type {X, Y};

		//Leaf data
		public List<PointD> _bucket;

		//Internal node data
		public double _val;
		public Node _left, _right;
		public Type split;

		public Node()
		{
			_bucket = new List<PointD>();
			_left = null;
			_right = null;
		}

	}
	public class SpatialTree
	{
		public Node _root;
		const int _bucketSize = 3;

		public SpatialTree ()
		{
			_root = new Node();

		}

		#region Search
		public PointD GetClosestPoint (PointD point)
		{
			return GetClosestPoint(point, _root);
		}
		public PointD GetClosestPoint (PointD point, Node node)
		{
			if (node == null)
				return new PointD();

			double distanceSqA = double.MaxValue;
			PointD closestPointA = new PointD();
			PointD closestPointB = new PointD();

			//this is a leaf node
			if (node._left == null && node._right == null) 
			{
				foreach (PointD pt in node._bucket)
				{
					
					if( (pt.X - point.X)*(pt.X - point.X) + 
					   (pt.Y - point.Y)*(pt.Y - point.Y) < distanceSqA)
					{
						closestPointA = pt;
						distanceSqA = (pt.X - point.X)*(pt.X - point.X) + (pt.Y - point.Y)*(pt.Y - point.Y);
					}
				}
				return closestPointA;
			}

			switch (node.split)
			{
			case Node.Type.X:
				if (point.Y > node._val){
					closestPointA = GetClosestPoint(point, node._left);
				}else{
					closestPointA = GetClosestPoint(point, node._right);
				}
				break;
			case Node.Type.Y:
				if (point.X < node._val){
					closestPointA = GetClosestPoint(point, node._left);
				}else{
					closestPointA = GetClosestPoint(point, node._right);
				}	
				break;
			}

			distanceSqA = (closestPointA.X - point.X)*(closestPointA.X - point.X) + (closestPointA.Y - point.Y)*(closestPointA.Y - point.Y);

			switch (node.split)
			{
			case Node.Type.X:
				if (point.Y > node._val){
					if ((point.Y - node._val)*(point.Y - node._val) <= distanceSqA)
						closestPointB = GetClosestPoint(point, node._right);
				}else{
					if ((point.Y - node._val)*(point.Y - node._val) < distanceSqA)
						closestPointB = GetClosestPoint(point, node._left);
				}
				break;
			case Node.Type.Y:
				if (point.X < node._val){
					if ((point.X - node._val)*(point.X - node._val) <= distanceSqA)
						closestPointB = GetClosestPoint(point, node._right);
				}else{
					if ((point.X - node._val)*(point.X - node._val) < distanceSqA)
						closestPointB = GetClosestPoint(point, node._left);
				}	
				break;
			}

			var distanceSqB = (closestPointB.X - point.X)*(closestPointB.X - point.X) + (closestPointB.Y - point.Y)*(closestPointB.Y - point.Y);
			return distanceSqA < distanceSqB ? closestPointA : closestPointB;

		}
		public List<PointD> GetPointsInRange (PointD point, double radius)
		{
			var points = new List<PointD>();
			GetPointsInRange(point, radius*radius, _root, points);

			return points;
		}
		public void GetPointsInRange (PointD point, double radsquare, Node node, List<PointD> listOfPoints)
		{
			if (node == null)
				return;

			//this is a leaf node
			if (node._left == null && node._right == null) 
			{
				foreach (PointD pt in node._bucket)
				{

					if( (pt.X - point.X)*(pt.X - point.X) + 
					    (pt.Y - point.Y)*(pt.Y - point.Y) < radsquare)
					{
						listOfPoints.Add(pt);
					}
				}
				return;
			}

			switch (node.split)
			{
			case Node.Type.X:
				if (point.Y > node._val){
					GetPointsInRange(point, radsquare, node._left, listOfPoints);
					if ((point.Y - node._val)*(point.Y - node._val) <= radsquare)
						GetPointsInRange(point, radsquare, node._right, listOfPoints);
				}else{
					GetPointsInRange(point, radsquare,  node._right, listOfPoints);
					if ((point.Y - node._val)*(point.Y - node._val) < radsquare)
						GetPointsInRange(point, radsquare, node._left, listOfPoints);

				}


				break;
			case Node.Type.Y:
				if (point.X < node._val){
					GetPointsInRange(point, radsquare, node._left, listOfPoints);
					if ((point.X - node._val)*(point.X - node._val) <= radsquare)
						GetPointsInRange(point, radsquare, node._right, listOfPoints);

				}else{
					GetPointsInRange(point, radsquare, node._right, listOfPoints);
					if ((point.X - node._val)*(point.X - node._val) < radsquare)
						GetPointsInRange(point, radsquare, node._left, listOfPoints);

				}	
				break;
			}


		}
		#endregion
		#region Insertion
		public void AddObject (DrawingObject obj)
		{
			//should do something smart here... So that we get a more balanced tree.
			//things todo
			foreach (PointD pt in obj.points) 
			{
				addPoint(pt);
			}
		}
		public void addPoint(PointD point)
		{
			addPoint(point, _root);
		}
		public void addPoint (PointD point, Node node)
		{
			//if we have reached a leaf node
			if (node == null)
				node = new Node ();
			if (node._left == null && node._right == null) {
				node._bucket.Add (point);

				if (node._bucket.Count > _bucketSize) {

					node._val = 0.0;
					foreach (PointD pt in node._bucket)
					{
						if (node.split == Node.Type.X)
							node._val += pt.Y;
						else
							node._val += pt.X;
					}
					node._val /= node._bucket.Count;


					node._left = new Node ();
					node._right = new Node ();

					foreach(PointD pt in node._bucket)
					{
						if (node.split == Node.Type.X)
							if (pt.Y > node._val)
								node._left._bucket.Add(pt);
							else
								node._right._bucket.Add (pt);
						else
							if (pt.X < node._val)
								node._left._bucket.Add(pt);
							else
								node._right._bucket.Add (pt);
					}

					node._left.split = node._right.split = node.split == Node.Type.X ? Node.Type.Y : Node.Type.X;

					//is this risky?
					node._bucket.Clear ();

				
				}
				return;
			}
			switch (node.split) 
			{
			
			case Node.Type.X:
				if (point.Y > node._val){
					addPoint(point, node._left);
				}else{
					addPoint(point, node._right);
				}
				break;
			case Node.Type.Y:
				if (point.X < node._val){
					addPoint(point, node._left);
				}else{
					addPoint(point, node._right);
				}
				break;

			}

		}
#endregion
	}
	
}

