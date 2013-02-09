using System;

namespace Core
{
	public class Tuple<T1>
	{
		public T1 A { get; set; }
		public Tuple (T1 a)
		{
			A = a;
		}
	}

	public class Tuple<T1, T2> : Tuple<T1>
	{
		public T2 B{ get; set; }

		public Tuple(T1 a, T2 b) :base (a)
		{
			B = b;
		}
	}
	public class Tuple<T1, T2, T3> : Tuple<T1, T2>
	{
		public T3 C{ get; set; }
		
		public Tuple(T1 a, T2 b, T3 c) :base (a, b)
		{
			C = c;
		}
	}
}

