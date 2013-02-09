using System;
using Gtk;
using Gdk;
using ViewModel;
using Cairo;
using Core.UI;
using System.Collections;
using Messenger;
using Core;
namespace UI	
{
	public class DrawingWidgetView : Gtk.DrawingArea
	{
		
		public DrawingWidgetViewModel viewModel{get; private set;}
		
		public DrawingWidgetView(DrawingWidgetViewModel vm)
		{
			AddEvents (-1);
			viewModel = vm;
			
			//View Model will be in charge of redrawing the screen. It knows better
			VMMessenger.getMessenger().register<RequestRedrawMessage>(HandleRedrawMessage);

			
		}
		#region Message Handler
		private void HandleRedrawMessage(RequestRedrawMessage msg)
		{
			QueueDraw();
		}
		

		#endregion
	
		#region Event Handlers
		protected override bool OnExposeEvent (EventExpose evnt)
		{
			DrawOnExpose(evnt.Window);
			return base.OnExposeEvent (evnt);
		}
		
		protected override bool OnMotionNotifyEvent (EventMotion evnt)
		{
			viewModel.MouseMoved(evnt.X, evnt.Y);
			return base.OnMotionNotifyEvent (evnt);
		}
		protected override bool OnButtonPressEvent (EventButton evnt)
		{
			viewModel.ButtonPressed(evnt.Button);
			return base.OnButtonPressEvent (evnt);
		}
		
		#endregion
		
		#region Draw Functions
		private void DrawMoment(Context ctx, Tuple<PointDouble, double> moment)
		{
			ctx.Save ();
		
			ctx.Color = new Cairo.Color(0,0,0);
			ctx.Arc(moment.Item1.X, moment.Item1.Y, 20, 4, 2.28);
			ctx.Stroke();
			
			ctx.Restore();
			
		}
		private void DrawForce(Context ctx, Tuple<PointDouble, double, double> force)
		{
			ctx.Save ();
			ctx.Color = new Cairo.Color(0,0,0);
			
			ctx.MoveTo(force.Item1.toPointD());
			ctx.LineTo(force.Item1.X + force.Item3 * Math.Cos(force.Item2), 
			           force.Item1.Y + force.Item3 * Math.Sin (force.Item2));
			ctx.Rectangle(force.Item1.X-5, force.Item1.Y-5, 10,10);
			
			
			ctx.Stroke();
			ctx.Restore();
		}
		public void DrawObject (Context ctx, DrawingObject body)
		{
			if (body == null)
				return;
			ctx.Save ();
			ctx.Color = new Cairo.Color (0, 0, 0);
			
			ctx.LineWidth = 1;
			foreach (Tuple<PointDouble, PointDouble> line in body.lines) {
				ctx.MoveTo (line.Item1.toPointD());
				ctx.LineTo (line.Item2.toPointD());
			}
			
			ctx.Stroke ();
			
			foreach (PointDouble point in body.points) {
				ctx.Rectangle (point.X - 5, point.Y - 5, 10, 10);
				ctx.Fill ();
			}
			foreach (Tuple<PointDouble, double, double> force in body._forces) {
				DrawForce (ctx, force);
			}
			
			foreach (Tuple<PointDouble, double> moment in body._moments) {
				DrawMoment (ctx, moment);
			}
			
			ctx.Restore ();
		}
		public  void DrawOnExpose (Gdk.Window window)
		{
			using (Context ctx = Gdk.CairoHelper.Create(window)){
				ctx.SetSourceRGB(1,1,1);
				ctx.Paint ();
				
				foreach (DrawingObject body in viewModel.ObjectsToDraw)
				{
					DrawObject(ctx, body);
				}
				//This should probably draw differently depending on type, using inheritance..
				DrawObject(ctx,viewModel.TemporaryObject);
				
				if (viewModel.IsDrawingObject){
					ctx.Save();
					ctx.Color = new Cairo.Color(1,0,0);
					ctx.MoveTo(viewModel.LastPos.toPointD());
					ctx.LineTo(viewModel.MousePos.toPointD());
					ctx.Stroke();
					ctx.Rectangle(viewModel.MousePos.toPointD(), 10,10);
					ctx.Fill();
					ctx.Restore();
				}
				
				
			}
			
		}
		#endregion
		
	}
}

