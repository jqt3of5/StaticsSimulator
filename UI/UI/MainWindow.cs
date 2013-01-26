using System;
using Gtk;
using UI;
using ViewModel;
public partial class MainWindow: Gtk.Window
{	
	public DrawingWidget drawingView{get; private set;}
	public DrawingWidgetViewModel drawingViewModel{get; private set;}
	
	public ToolBarView toolBarView{get; private set;}
	public ToolBarViewModel toolBarViewModel{get; private set;}
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//add the special drawing window here
		drawingViewModel = new DrawingWidgetViewModel();
		drawingView = new DrawingWidget(drawingViewModel);
		
		toolBarViewModel = new ToolBarViewModel();
		toolBarView = new ToolBarView(toolBarViewModel);
		
		
		toolbarAlignment.Add (toolBarView);
		DrawingBoxAlignment.Add(drawingView);
		
		ShowAll();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
}
