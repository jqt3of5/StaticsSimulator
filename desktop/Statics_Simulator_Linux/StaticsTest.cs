using System;
using NUnit.Framework;


namespace Statics_Simulator_Linux
{
	[TestFixture]
	public class StaticsTest
	{
		public StaticsTest ()
		{
		}
		
		[Test]
		public void resolveForce()
		{
		}
		
		[Test]
		public void multiForceTest()
		{
			Point A = new Point(0,0);
			Point B = new Point (1,0);
			Beam beam = new Beam (A, B);
			BeamController controller = new BeamController(beam);
					
			controller.setInconsistentCallback(delegate(Tuple<Point, Point> moment, Point mag) {
				Console.WriteLine("Inconsistency detected. Have " +
				                  + moment.Item1.x + "," + moment.Item1.y + "," + moment.Item1.z	
				                  + " needed " + mag.x + "," + mag.y + "," + mag.z);	
			});
			controller.setConsistentCallback( delegate(Tuple<Point, Point> moment, Point mag) {
				Console.WriteLine("Consistent!");
			});
			
			controller.addMoment(new Point(0,0,-25), new Point(.5,0));
			controller.addMoment(new Point(0,0,-100), new Point(1,0));
			controller.addMoment(new Point(0,0,50), new Point(0,0));
			
			controller.addForce(new Point(0, 100), new Point(0,0));
			controller.addForce(new Point(0, 50), new Point(1,0));
			
		}
		
		[Test]
		public void PointTest()
		{
			Point A = new Point(3,10);
			Point Fa = new Point(10, 4);
			
			Assert.AreEqual(-88, A*Fa);
			Assert.AreEqual(Math.Sqrt(13*13+14*14), (A+Fa).mag ());
			Assert.AreEqual(Math.Sqrt(7*7+6*6), (A-Fa).mag ());
	
		}
		
	}
}

