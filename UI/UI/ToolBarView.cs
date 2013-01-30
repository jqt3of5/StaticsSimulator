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
			
			tool11.Changed += ToolSelected;
			tool21.Changed += ToolSelected;
			tool3.Changed += ToolSelected;
			tool4.Changed += ToolSelected; 
		}	
		
		private void ToolSelected(object sender, ChangedArgs args)
		{
			
			var tbButton = sender as Gtk.RadioAction;
			if (tbButton == tool11 && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.CONNECTED;
			else if (tbButton == tool21 && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.UNCONNECTED;
			else if (tbButton == tool3 && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.MOMENT;
			else if (tbButton == tool4 && tbButton.Active)
				viewModel.selectedTool = ToolBarViewModel.Tools.FORCE;
			 
			 
		}
		
		
	}
}

