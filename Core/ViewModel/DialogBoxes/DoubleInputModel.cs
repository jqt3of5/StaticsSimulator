using System;
using Cairo;

namespace Core.VM
{
	public class DoubleInputModel
	{
		public int Count {get; set; }
		public Double [] _inputs;

		public DrawingObject ActiveObject { get; set; }
		public PointD ActivePoint { get; set; }

		public DoubleInputModel (int numberOfInputs)
		{
			_inputs = new Double[numberOfInputs];
			Count = numberOfInputs;
		}



	}
}

