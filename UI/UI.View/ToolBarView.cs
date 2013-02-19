using System;
using ViewModel;
using Gdk;
using Gtk;
namespace UI
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ToolBarView : Gtk.Bin
	{
		
		public ToolBarViewModel viewModel{get; private set;}
		
		public ToolBarView (ToolBarViewModel vm)
		{
			this.Build ();
			viewModel = vm;
			
			connectedTool.Changed += ToolSelected;
			unconnectedTool.Changed += ToolSelected;
			forceTool.Changed += ToolSelected;
			momentTool.Changed += ToolSelected; 
			selectionTool.Changed += ToolSelected; 
			jointTool.Changed += ToolSelected;
		}	
		
		private void ToolSelected(object sender, ChangedArgs args)
		{
			
			var tbButton = sender as Gtk.RadioAction;
			if (tbButton == connectedTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.CONNECTED;
			else if (tbButton == unconnectedTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.UNCONNECTED;
			else if (tbButton == forceTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.FORCE;
			else if (tbButton == momentTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.MOMENT;
			else if (tbButton == selectionTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.SELECTION;
			else if (tbButton == jointTool && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.JOINT;
			 
		}
		
		
	}
}

