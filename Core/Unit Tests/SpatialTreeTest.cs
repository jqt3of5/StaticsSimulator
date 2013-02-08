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
			List<PointD> points = new List<PointD>();
			points.Add (new PointD(0,0));
			points.Add (new PointD(50,50));
			points.Add (new PointD(0,50));
			points.Add (new PointD(50,0));
			points.Add (new PointD(25,25));
			points.Add (new PointD(25,0));
			points.Add (new PointD(0,25));

			foreach(PointD pt in points)
				tree.addPoint(pt);

			Assert.AreEqual(0, tree._root._bucket.Count);
			Assert.AreNotEqual(null,tree._root._left);
			Assert.AreNotEqual(null, tree._root._right);

		}
		[Test()]
		public void Lookup ()
		{
			SpatialTree tree = new SpatialTree();
			List<PointD> points = new List<PointD>();

			points.Add (new PointD(0,0));
			points.Add (new PointD(0,10));
			points.Add (new PointD(0,20));
			points.Add (new PointD(0,30));
			points.Add (new PointD(0,40));
			points.Add (new PointD(0,50));
			points.Add (new PointD(0,60));

			points.Add (new PointD(0,70));

			foreach(PointD pt in points)
				tree.addPoint(pt);
			

			var result = tree.GetPointsInRange(new PointD(1,1), 15);

			Assert.AreEqual(2, result.Count);
		}


		[Test()]
		public void StressTest ()
		{
			SpatialTree tree = new SpatialTree ();
			List<PointD> points = new List<PointD> ();
			Random rand = new Random ();

			//initialize the list
			for (int i = 0; i < 10000; ++i) 
			{
				points.Add (new PointD (rand.Next (1000), rand.Next (1000)));
			}

			DateTime start = DateTime.Now;
			//insert them all into the list
			foreach (PointD pt in points) 
			{
				tree.addPoint (pt);
			}
			DateTime stop = DateTime.Now;
			Console.WriteLine ("Took " + (stop - start) + " to add " + 10000 + " records");			


			start = DateTime.Now;
			for (int i = 0; i < 10000; ++i) 
			{
				tree.GetPointsInRange(new PointD(rand.Next(1000), rand.Next(1000)), 10);
			}
			stop  = DateTime.Now;

			var treeTime = stop-start;

			Console.WriteLine("SpatialTree: Took " + treeTime + " to lookup " + 10000 + " records");	

			
			start = DateTime.Now;

			List<PointD> found;
			for (int i = 0; i < 10000; ++i) 
			{
				found = new List<PointD>();
				var lookup = new PointD(rand.Next(1000), rand.Next(1000));
				foreach(PointD pt in points)
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
			List<PointD> points = new List<PointD> ();
			Random rand = new Random ();

			//initialize the list
			for (int i = 0; i < 10000; ++i) 
			{
				points.Add (new PointD (rand.Next (1000), rand.Next (1000)));
			}
			
			//insert them all into the list
			foreach (PointD pt in points) 
			{
				tree.addPoint (pt);
			}
			List<PointD> found1;
			List<PointD> found2;
			for (int i = 0; i < 10000; ++i) 
			{
				var point = new PointD(rand.Next(1000), rand.Next(1000));
				found1 = tree.GetPointsInRange( point, 10);

				found2 = new List<PointD>();
				foreach(PointD pt in points)
				{
					if ((pt.X-point.X)*(pt.X-point.X) + 
					    (pt.Y-point.Y)*(pt.Y-point.Y) < 100.0) 
					{
						found2.Add(pt);
					}
				}

				foreach(PointD pt in found1)
				{
					Assert.AreEqual(true, found2.Contains(pt));
				}
				foreach(PointD pt in found2)
				{
					Assert.AreEqual(true, found1.Contains(pt));
				}

				//found1.Clear();
				//found2.Clear ();
			}

		}
	}
}

