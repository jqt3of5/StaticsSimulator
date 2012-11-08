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
		public void unknownForceInsufficientTest1()
		{
		/*	SPoint A = new SPoint(0,0, "A");
			SPoint D = new SPoint(1,0,"D");
			SPoint C = new SPoint(.5,0,"C");
			
			Beam beam = new Beam(A,D);
			beam.addConnectingPoint(C);
			
			
			beam.getConnectingPoint("A").addForce(new Force());
			beam.getConnectingPoint("D").addForce(new Force(100, Math.PI/2.0));
			
			beam.getConnectingPoint("C").addForce(new Force(3.0*Math.PI/2.0));
			beam.getConnectingPoint("A").moment= new Moment(0);
			
			try{
				StaticMath.calcForce(beam, "A");
				Assert.Fail();
			}catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			
			try{
				StaticMath.calcMoment(beam, "D");
				Assert.Fail();
			}catch (Exception e)
			{
				
				Console.WriteLine(e.Message);
			}
		
			Console.WriteLine("the end");
			*/
		}
		
		[Test]
		public void unknownMomentTest()
		{
			SPoint A = new SPoint(0,0, "A");
			SPoint D = new SPoint(1,0,"D");
			SPoint C = new SPoint(.5,0,"C");
			
			Beam beam = new Beam(A,D);
			beam.addPoint(C);
			
			beam.forces.Add(new Force(100, Math.PI/2.0,A));
			beam.forces.Add(new Force(50, Math.PI/2.0,C));
			
			Assert.AreEqual(200, StaticMath.calcMoment(D,beam));
		
		}
		[Test]
		public void unknownForceTest()
		{
			SPoint A = new SPoint(0,0, "A");
			SPoint D = new SPoint(1,0,"D");
			SPoint B = new SPoint(.25,0,"B");
			SPoint C = new SPoint(.5,0,"C");
			
			Beam beam = new Beam(A,D);
			beam.addPoint(C);
			beam.addPoint(B);
			
			
			
			beam.forces.Add(new Force(100, Math.PI/2.0,A));
			
			beam.moments.Add (new Moment(0,D));
			
			
			Assert.AreEqual(200, StaticMath.calcForce(beam, "C").magnitude);
			Assert.AreEqual(3.0*Math.PI/2.0, StaticMath.calcForce(beam, "C").direction);
		
		//	Assert.AreEqual(StaticMath.calcMoment(beam, "A"), 0);
		
			
		}
		[Test]
		public void multiForceTest()
		{
			SPoint A = new SPoint(0,0, "A");
			SPoint B = new SPoint(1,0,"B");
			SPoint C = new SPoint(.5,0,"C");
			
			
			Beam beam = new Beam(A,B);
			beam.addPoint(C);
			
			beam.forces.Add (new Force(200, 3.0*Math.PI/2.0,C));
			beam.forces.Add (new Force(100, Math.PI/2.0,B));
			
			
			//beam.getConnectingPoint("A").addForce(new Force(100, Math.PI/2.0));
			//beam.getConnectingPoint("B").addForce(new Force(100, Math.PI/2.0));
			
			//beam.getConnectingPoint("C").addForce(new Force(250, 3.0*Math.PI/2.0));
			//beam.getConnectingPoint("C").addForce(new Force(50, Math.PI/2.0));
			
			Assert.AreEqual(0,StaticMath.calcMoment("A",beam));
		}
		[Test]
		public void singleForceTest()
		{
			SPoint A = new SPoint(0,0, "A");
			SPoint B = new SPoint(1,0,"B");
			
			Beam beam = new Beam(A,B);
			
			beam.forces.Add(new Force(100, Math.PI/2.0, A));
			beam.forces.Add(new Force(50, Math.PI/2.0, B));
			
			Assert.AreEqual(50, StaticMath.calcMoment("A", beam));
			Assert.AreEqual(-100, StaticMath.calcMoment("B", beam));
			
		}
		
		[Test]
		public void crossProductTest()
		{
			SPoint A = new SPoint(0,0,"A");
			SPoint B = new SPoint(1,0,"B");
			
			
			Beam beam = new Beam(A,B);
			
			beam.forces.Add(new Force(100, Math.PI/2.0, B));
			
			Assert.AreEqual(100, StaticMath.rCrossF(A, B, beam));
			
		}
		
	}
}

