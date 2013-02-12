using System;

namespace Core
{
	public class ConnectedObject : DrawingObject
	{
		public ConnectedObject ()
		{
		}

		public void Connect()
		{
			lines.Add(new Tuple<PointDouble, PointDouble>(firstPoint, lastPoint));
		}
		
	}
}

