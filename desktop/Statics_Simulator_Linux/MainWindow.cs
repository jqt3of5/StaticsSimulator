using System;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		ListStore store = new ListStore(typeof(string), typeof(Gdk.Pixbuf));
		store.AppendValues("col A", Gtk.IconTheme.Default.LoadIcon("gnome-fs-regular",48,0));
		store.AppendValues("1",  Gtk.IconTheme.Default.LoadIcon("gnome-fs-regular",48,0));
		store.AppendValues("2",  Gtk.IconTheme.Default.LoadIcon("gnome-fs-regular",48,0));
		store.AppendValues("3",  Gtk.IconTheme.Default.LoadIcon("gnome-fs-regular",48,0));
		
		this.iconView.TextColumn = 0;
		this.iconView.PixbufColumn = 1;
		
		this.iconView.Model = store;
		
		ShowAll ();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
