using System;
using Gtk;
using Gdk;
using ViewModel;
using Cairo;

namespace UI	
{
	public class DrawingWidget : Gtk.DrawingArea
	{
		
		public DrawingWidgetViewModel viewModel{get; private set;}
		
		public DrawingWidget(DrawingWidgetViewModel vm)
		{
			AddEvents (-1);
			viewModel = vm;
			//wire up the events to the view model
			ConfigureEvent += Initialize;
			ExposeEvent += viewModel.DrawOnExpose;
			MotionNotifyEvent += viewModel.MouseMoved;
			ButtonPressEvent += viewModel.ButtonPressed;
			viewModel.redrawWindow = new System.Action(QueueDraw);
			
		}
		
		protected void Initialize (object sender, ConfigureEventArgs args)
		{
			viewModel.View = new ImageSurface(Format.Argb32, args.Event.Width, args.Event.Height);
			
		}
		
	}
}

