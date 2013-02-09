using NUnit.Framework;
using System;
using System.Collections.Generic;
using Cairo;

using Core.VM;

namespace Core
{
	[TestFixture()]
	public class SpatialTreeTest
	{
		[Test()]
		public void Insertion ()
		{
			SpatialTree tree = new SpatialTree();
			List<PointDouble> points = new List<PointDouble>();
			points.Add (new PointDouble(0,0));
			points.Add (new PointDouble(50,50));
			points.Add (new PointDouble(0,50));
			points.Add (new PointDouble(50,0));
			points.Add (new PointDouble(25,25));
			points.Add (new PointDouble(25,0));
			points.Add (new PointDouble(0,25));

			foreach(PointDouble pt in points)
				tree.addPoint(pt);

			Assert.AreEqual(0, tree._root._bucket.Count);
			Assert.AreNotEqual(null,tree._root._left);
			Assert.AreNotEqual(null, tree._root._right);

		}
		[Test()]
		public void Lookup ()
		{
			SpatialTree tree = new SpatialTree();
			List<PointDouble> points = new List<PointDouble>();

			points.Add (new PointDouble(0,0));
			points.Add (new PointDouble(0,10));
			points.Add (new PointDouble(0,20));
			points.Add (new PointDouble(0,30));
			points.Add (new PointDouble(0,40));
			points.Add (new PointDouble(0,50));
			points.Add (new PointDouble(0,60));

			points.Add (new PointDouble(0,70));

			foreach(PointDouble pt in points)
				tree.addPoint(pt);
			

			var result = tree.GetPointsInRange(new PointDouble(1,1), 15);

			Assert.AreEqual(2, result.Count);
		}


		[Test()]
		public void StressTest ()
		{
			SpatialTree tree = new SpatialTree ();
			List<PointDouble> points = new List<PointDouble> ();
			Random rand = new Random ();

			//initialize the list
			for (int i = 0; i < 10000; ++i) 
			{
				points.Add (new PointDouble (rand.Next (1000), rand.Next (1000)));
			}

			DateTime start = DateTime.Now;
			//insert them all into the list
			foreach (PointDouble pt in points) 
			{
				tree.addPoint (pt);
			}
			DateTime stop = DateTime.Now;
			Console.WriteLine ("Took " + (stop - start) + " to add " + 10000 + " records");			


			start = DateTime.Now;
			for (int i = 0; i < 10000; ++i) 
			{
				tree.GetPointsInRange(new PointDouble(rand.Next(1000), rand.Next(1000)), 10);
			}
			stop  = DateTime.Now;

			var treeTime = stop-start;

			Console.WriteLine("SpatialTree: Took " + treeTime + " to lookup " + 10000 + " records");	

			
			start = DateTime.Now;

			List<PointDouble> found;
			for (int i = 0; i < 10000; ++i) 
			{
				found = new List<PointDouble>();
				var lookup = new PointDouble(rand.Next(1000), rand.Next(1000));
				foreach(PointDouble pt in points)
				{
					if ((pt.X-lookup.X)*(pt.X-lookup.X) + 
					    (pt.Y-lookup.Y)*(pt.Y-lookup.Y) < 100) 
					{
						found.Add(pt);
					}
				}
			}
			stop  = DateTime.Now;
			
			var linearTime = stop-start;
			Console.WriteLine("Linear search: Took " + linearTime+ " to lookup " + 10000 + " records");	

			Assert.Greater(linearTime, treeTime);
		}
		
		[Test()]
		public void CorrectnessTest ()
		{
			SpatialTree tree = new SpatialTree ();
			List<PointDouble> points = new List<PointDouble> ();
			Random rand = new Random ();

			//initialize the list
			for (int i = 0; i < 10000; ++i) 
			{
				points.Add (new PointDouble (rand.Next (1000), rand.Next (1000)));
			}
			
			//insert them all into the list
			foreach (PointDouble pt in points) 
			{
				tree.addPoint (pt);
			}
			List<PointDouble> found1;
			List<PointDouble> found2;
			for (int i = 0; i < 10000; ++i) 
			{
				var point = new PointDouble(rand.Next(1000), rand.Next(1000));
				found1 = tree.GetPointsInRange( point, 10);

				found2 = new List<PointDouble>();
				foreach(PointDouble pt in points)
				{
					if ((pt.X-point.X)*(pt.X-point.X) + 
					    (pt.Y-point.Y)*(pt.Y-point.Y) < 100.0) 
					{
						found2.Add(pt);
					}
				}

				foreach(PointDouble pt in found1)
				{
					Assert.AreEqual(true, found2.Contains(pt));
				}
				foreach(PointDouble pt in found2)
				{
					Assert.AreEqual(true, found1.Contains(pt));
				}

				//found1.Clear();
				//found2.Clear ();
			}

		}
		[Test()]
		public void GetClosestTest ()
		{
			SpatialTree tree = new SpatialTree ();
			List<PointDouble> points = new List<PointDouble> ();
			Random rand = new Random ();
			
			//initialize the list
			for (int i = 0; i < 10000; ++i) 
			{
				points.Add (new PointDouble (rand.Next (1000), rand.Next (1000)));
			}
			
			//insert them all into the list
			foreach (PointDouble pt in points) 
			{
				tree.addPoint (pt);
			}
			for (int i = 0; i < 10000; ++i) 
			{
				var point = new PointDouble(rand.Next(1000), rand.Next(1000));
				var found = tree.GetClosestPoint(point);

				PointDouble closestPoint = new PointDouble();
				double distanceSq = double.MaxValue;

				foreach(PointDouble pt in points)
				{
					if ((pt.X-point.X)*(pt.X-point.X) + 
					    (pt.Y-point.Y)*(pt.Y-point.Y) < distanceSq) 
					{
						closestPoint = pt;
						distanceSq = (pt.X-point.X)*(pt.X-point.X) + (pt.Y-point.Y)*(pt.Y-point.Y);
					}
				}
				var dist = (found.X - point.X)*(found.X - point.X) + (found.Y - point.Y)*(found.Y - point.Y);
				Assert.AreEqual(distanceSq, dist);


			}
				

			
		}

	}
}

