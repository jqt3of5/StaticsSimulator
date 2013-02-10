using System;
using Cairo;
using System.Collections.Generic;
namespace Core.VM
{
	public class DoubleInputModel
	{
		public int Count {get; set; }
		public double [] _inputs;
		public string [] _labels;
		public DrawingObject ActiveObject { get; set; }
		public PointD ActivePoint { get; set; }

		public DoubleInputModel (int numberOfInputs, string[] labels)
		{
			_inputs = new double[numberOfInputs];
			Count = numberOfInputs;
			_labels = labels;
		}



	}
}

