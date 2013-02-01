using System;
using System.Collections;
using System.Collections.Generic;
using Cairo;
using Core.UI;
namespace ViewModel
{
	public class DrawingWidgetModel
	{

		public List<DrawingObject> Objects{get; private set;}

		public DrawingWidgetModel ()
		{
			Objects = new List<DrawingObject>();
		}
		
		public void commitTemporaryObject(DrawingObject body)
		{
			Objects.Add(body);
		}
		
		
	}
}

