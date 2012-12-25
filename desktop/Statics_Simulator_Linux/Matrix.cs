using System;

namespace Statics_Simulator_Linux
{
	public class Matrix
	{
		double [][] max;
		int height;
		int width;
		public Matrix(int a, int b)
		{
			height = a;
			width = b;
			max = new double[a][];
			for (int i = 0; i < a; ++i)
				max[i] = new double [b];
			
		}
		public int getHeight()
		{
			return height;
		}
		public int getWidth()
		{
			return width;
		}
		
		public double [] this[int i]
		{
			get{
				return max[i];
			}
			
			set{
				max[i] = value;
			}
		}
		
		public static Matrix operator+(Matrix a, Matrix b)
		{
			if(a.getHeight() != b.getHeight() || a.getWidth() != b.getWidth())
			{
				throw new Exception("Dimention mismatch");
			}
			
			Matrix result = new Matrix(a.getHeight(), a.getWidth());
			for (int i = 0; i < a.getHeight(); ++i)	
				for (int j = 0; j < a.getWidth(); ++j)
					result.max[i][j] = a[i][j] + b[i][j];
			
			return result;
		}
		
	}
}

