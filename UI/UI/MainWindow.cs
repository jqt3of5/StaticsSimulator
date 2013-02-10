using System;
using Gtk;
using UI;
using ViewModel;
using Messenger;
public partial class MainWindow: Gtk.Window
{	
	public DrawingWidgetView drawingView{get; private set;}
	public DrawingWidgetViewModel drawingViewModel{get; private set;}
	
	public ToolBarView toolBarView{get; private set;}
	public ToolBarViewModel toolBarViewModel{get; private set;}
	
	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//add the special drawing window here
		drawingViewModel = new DrawingWidgetViewModel();
		drawingView = new DrawingWidgetView(drawingViewModel);
		
		toolBarViewModel = new ToolBarViewModel();
		toolBarView = new ToolBarView(toolBarViewModel);
		
		
		toolbarAlignment.Add (toolBarView);
		DrawingBoxAlignment.Add(drawingView);

		VMMessenger.getMessenger().register<UpdateStatusMessage>(HandleStatusUpdateMessage);
		VMMessenger.getMessenger().register<UpdatePositionStatusMessage>(HandlePositionStatusUpdateMessage);

		ShowAll();
	}

	private void HandlePositionStatusUpdateMessage (UpdatePositionStatusMessage msg)
	{
		posLabel.Text = "x: " + msg.X + " y: " + msg.Y;
	}

	private void HandleStatusUpdateMessage(UpdateStatusMessage msg)
	{
		uint id = 1;
		_statusBar.Push(id, msg.Message);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
}
