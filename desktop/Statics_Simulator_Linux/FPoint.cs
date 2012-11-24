using System;

namespace Statics_Simulator_Linux
{
	public class FPoint : Point
	{
		public double direction;
		public double magnitude;
		
		public FPoint() : base(0,0,"N")
		{
			direction = 0;
			magnitude = 0;
			known = false;
		}
		public FPoint(double dir, double x, double y, String n) : base(x,y,n)
		{
			direction = dir;
			magnitude = 0;	
			known = false;
		}
		public FPoint(double mag, double dir, double x, double y,String n) : base(x,y,n)
		{
			direction = dir;
			magnitude = mag;
		}
		public FPoint addForce(FPoint f)
		{
			double newi = i() + f.i();
			double newj = j() + f.j();
			
			return new FPoint(Math.Sqrt(newi*newi + newj*newj), Math.Atan2(newj, newi), x,y,name + "+" +f.name);
		}
		public double i()
		{
			if (known == false)
				throw new Exception(name + " Force is unknown");
			return magnitude*Math.Cos(direction);
		}
		public double j()
		{
			if (known == false)
				throw new Exception(name + " Force is unknown");
			return magnitude*Math.Sin(direction);
		}
		
	}
}

