using System;

public class UpdatePositionStatusMessage
{
	public double X{get; set;}
	public double Y{get; set;}

	public UpdatePositionStatusMessage (double x, double y)
	{
		X = x;
		Y = y;
	}
}

