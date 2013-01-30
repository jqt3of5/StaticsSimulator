using System;
using Gtk;
using Gdk;
using ViewModel;
using Cairo;
using System.Collections;
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
			viewModel.redrawWindow = new System.Action(QueueDraw);
			
		}
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
		private void DrawMoment(Context ctx, PointD pt)
		{
			ctx.Save ();
		
			ctx.Color = new Cairo.Color(0,0,0);
			ctx.Arc(pt.X, pt.Y, 20, 4, 2.28);
			ctx.Stroke();
			
			ctx.Restore();
			
		}
		private void DrawForce(Context ctx, DrawingObject force)
		{
			ctx.Save ();
			ctx.Color = new Cairo.Color(0,0,0);
			Tuple<PointD, PointD> f = force.lines[0];
			ctx.MoveTo(f.Item1);
			ctx.LineTo(f.Item2);
			ctx.Rectangle(f.Item2.X-5, f.Item2.Y-5, 10,10);
			
			
			ctx.Stroke();
			ctx.Restore();
		}
		public void DrawObject(Context ctx, DrawingObject body)
		{
			if (body == null)
				return;
			ctx.Save();
			ctx.Color = new Cairo.Color(0,0,0);
			
			ctx.LineWidth = 1;
			foreach(Tuple<PointD, PointD> line in body.lines)
			{
				ctx.MoveTo(line.Item1);
				ctx.LineTo(line.Item2);
			}
			
			ctx.Stroke();
			
			foreach(PointD point in body.points)
			{
				ctx.Rectangle(point.X-5, point.Y-5, 10,10);
				ctx.Fill();
			}
				
			ctx.Restore();
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
				DrawObject(ctx,viewModel.tempObject);
				
				if (viewModel.IsDrawingObject){
					ctx.Save();
					ctx.Color = new Cairo.Color(1,0,0);
					ctx.MoveTo(viewModel.LastPos);
					ctx.LineTo(viewModel.MousePos);
					ctx.Stroke();
					ctx.Rectangle(viewModel.MousePos, 10,10);
					ctx.Fill();
					ctx.Restore();
				}
				
				
				foreach (DrawingObject f in viewModel.Forces)
				{
					DrawForce(ctx, f);
				}
				
				foreach (PointD pt in viewModel.Moments)
				{
					DrawMoment(ctx, pt);
				}
				
			}
			
		}
		#endregion
		
	}
}

