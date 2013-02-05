using System;

namespace Core.VM
{
	public class DoubleInputViewModel
	{
		public int Count { get; set; }
		public Double [] _inputs;

		public DoubleInputViewModel (int numberOfInputs)
		{
			_inputs = new Double[numberOfInputs];
			Count = numberOfInputs;
		}

	}
}

