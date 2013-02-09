using System;

namespace ViewModel
{
	public class UpdateStatusMessage
	{
		public string Message{ get; private set;} 
		public UpdateStatusMessage (string message)
		{
			Message = message;
		}
	}
}

