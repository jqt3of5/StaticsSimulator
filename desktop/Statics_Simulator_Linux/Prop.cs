using System;

namespace Statics_Simulator_Linux
{
	public class Prop
	{
		public String name;
		public bool known;
		
		public Prop()
		{
			name = "";
			known = false;
		}
		public Prop(String n, bool k)
		{
			name = n;
			known = k;
		}
	}
}

