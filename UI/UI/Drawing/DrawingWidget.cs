using System;
using Gtk;

namespace UI
{
	public class DrawingWidget : Gtk.DrawingArea
	{
		public DrawingWidget ()
		{
			AddEvents(-1); //add events
			ButtonPressEvent += OnMouseClick;
			
		}
		
		protected void OnMouseClick(object sender, ButtonPressEventArgs args)
		{
			if (args.Event.Button != 1)
				return;
			
			Console.WriteLine("test");
		}
	}
}

