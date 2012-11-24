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
		public void unknownForce()
		{
			Point A = new Point(0,0, "A");
			Point B = new Point(1,0,"B");
			
			Beam beam = new Beam(A,B);
			
			beam.addPoint(new MPoint(0, 0,0,"Ma"));
			
			beam.addPoint(new FPoint(100, Math.PI/2.0,1,0,"Fb"));
			beam.addPoint(new FPoint(Math.PI/2.0,0,0,"Fa"));
			
			Assert.AreEqual(100, ((MPoint)beam.getPoint("Ma")).magnitude);
			
			Assert.AreEqual(false, beam.getPoint("Fa").known);
			
			try{
			 	beam.calcMoment(B);
			}catch(Exception e)
			{
				return;
			}	
			Assert.Fail();
		}
		
		[Test]
		public void addingForceTest()
		{
			Point A = new Point(0,0, "A");
			Point B = new Point(1,0,"B");
			
			Beam beam = new Beam(A,B);
			
			beam.addPoint(new MPoint(0,0,0,"Ma"));
			beam.addPoint(new MPoint(0,1,0,"Mb"));
			
			Assert.AreEqual(0, ((MPoint)beam.getPoint("Ma")).magnitude);
			Assert.AreEqual(0, ((MPoint)beam.getPoint("Mb")).magnitude);
			
			beam.addPoint(new FPoint(100, Math.PI/2.0, 0,0,"Fa"));
			beam.addPoint(new FPoint(100, Math.PI/2.0, 1,0,"Fb"));
			
			Assert.AreEqual(100, ((MPoint)beam.getPoint("Ma")).magnitude);
			Assert.AreEqual(-100, ((MPoint)beam.getPoint("Mb")).magnitude);
		}
		[Test]
		public void multiForceTest()
		{
			Point A = new Point(0,0, "A");
			Point B = new Point(1,0,"B");
			
			Beam beam = new Beam(A,B);
			
			beam.addPoint(new FPoint(100, Math.PI/2.0, 0,0,"Fa"));
			beam.addPoint(new FPoint(50, Math.PI/2.0, 1,0,"Fb"));
			beam.addPoint (new MPoint(.5,0,"M"));
			
			Assert.AreEqual(50, beam.calcMoment("A").magnitude);
			Assert.AreEqual(-100, beam.calcMoment("B").magnitude);
			
			Assert.AreEqual(null, beam.calcMoment("M"));
			Assert.AreEqual(-25,((MPoint)beam.getPoint("M")).magnitude);
		}
		
		[Test]
		public void crossProductTest()
		{
			Point A = new Point(0,0,"A");
			FPoint Fa = new FPoint(100, Math.PI/2.0, 1,0,"Fa");
			
			
			Assert.AreEqual(100, A.rCrossF(Fa));
			
		}
		
	}
}

