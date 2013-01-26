using System;
using Gdk;
using Gtk;
using Messenger;
namespace ViewModel
{
	public class ToolBarViewModel
	{
		//unconnected body - a body, like a beam, whos end points don't necessarily connect
		//unconnected body - the opposite, like a box
		//force - a force point
		//moment - a moment point
		//joint - a joint bettween two bodies
		//anchor - an anchor (rigid or hinged) that anchors a single body
		public enum Tools{CONNECTED, UNCONNECTED, FORCE, MOMENT, JOINT, ANCHOR, POINT, NONE};
		
		private Tools _selectedTool;
		public Tools selectedTool 
		{
			get { return _selectedTool; } 
			set
			{
				_selectedTool = value;
				VMMessenger.getMessenger().sendMessage<NewToolChosenMessage>(new NewToolChosenMessage(value));
			
			}
		} // this property needs to send a message to DrawingWidgetviewmodel! 
		  //I want to use mvvm light. Seems the way to go. 
		
		public ToolBarViewModel ()
		{
			selectedTool = Tools.NONE;
		}
		
		
		
		
		
	}
}

