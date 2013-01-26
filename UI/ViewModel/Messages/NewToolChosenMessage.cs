using System;

namespace ViewModel
{
	public class NewToolChosenMessage
	{
		public ToolBarViewModel.Tools tool;
		public NewToolChosenMessage (ToolBarViewModel.Tools t)
		{
			tool = t;
		}
	}
}

