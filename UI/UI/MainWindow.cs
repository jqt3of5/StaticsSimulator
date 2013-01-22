using System;
using Gtk;
using UI;
public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		//add the special drawing window here
		DrawingBoxAlignment.Add(new DrawingWidget());
		//need to populate the toolbar with tools. 
		//
		//
		//
	}
	

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
}
