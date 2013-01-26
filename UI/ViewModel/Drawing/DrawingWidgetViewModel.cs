using System;
using Cairo;
using Gtk;
using Gdk;
using Messenger;
namespace ViewModel
{
	public class DrawingWidgetViewModel
	{ 
		#region private members
		private Surface _view;
		private bool isDrawingObject;
		private PointD lastPoint;
		#endregion
		
		#region Properties
		public ToolBarViewModel.Tools selectedTool{get; set;}
		public Surface View 
		{
			get{ return _view;} 
			set 
			{
				_view = value;
				using (Context ctx = new Context(value)){
					ctx.SetSourceRGB(1,1,1);
					ctx.Paint ();
				}
			}
		}	
		public System.Action redrawWindow{get; set;}
		#endregion
		
		#region Constructors
		public DrawingWidgetViewModel ()
		{
			isDrawingObject = false;
			selectedTool = ToolBarViewModel.Tools.NONE;
			VMMessenger.getMessenger().register<NewToolChosenMessage>(RequestToolChange);			
			VMMessenger.getMessenger().register<RequestRedrawMessage>(RequestRedraw);		                                              
		}
		#endregion
		
		#region Message Handlers
		private void RequestRedraw(RequestRedrawMessage msg)
		{
			redrawWindow();
		}
		
		private void RequestToolChange(NewToolChosenMessage tool)
		{
			Console.WriteLine("changed tool");
			selectedTool = tool.tool;
		}
		#endregion
		
		#region Event Handlers
		public  void DrawOnExpose (object o, ExposeEventArgs args)
		{
			using (Context ctx = Gdk.CairoHelper.Create(args.Event.Window)){
				ctx.SetSourceSurface(View, 0,0);
				ctx.Paint();
			}
			
		}
		
		public  void MouseMoved (object o, MotionNotifyEventArgs args)
		{
			if (isDrawingObject)
				using (Context ctx = new Context(View)){
					ctx.SetSourceRGB(1,1,1);
					ctx.Paint();
					
					ctx.SetSourceRGB(0,0,0);
					ctx.Rectangle(lastPoint.X-5, lastPoint.Y-5, 10,10);
					ctx.Fill ();
					ctx.LineWidth = 1;
					
					ctx.MoveTo(lastPoint);
					ctx.LineTo(new PointD(args.Event.X, args.Event.Y));
					ctx.Stroke();
					
				}
		    Console.WriteLine ("moved to: " + args.Event.X + " " + args.Event.Y);
			redrawWindow();
		}
		
		public  void ButtonPressed (object o, ButtonPressEventArgs args)
		{
			
			switch(args.Event.Button)
			{
			case 1:
				switch(selectedTool)
				{
				case ToolBarViewModel.Tools.POINT:
					isDrawingObject = false;
					using (Context ctx = new Context(View)){
						ctx.Rectangle (args.Event.X-5,args.Event.Y-5,10,10);
						ctx.Fill();					
					}
				break;
				case ToolBarViewModel.Tools.UNCONNECTED:
					lastPoint = new PointD(args.Event.X, args.Event.Y);
					using (Context ctx = new Context(View)){
						ctx.Rectangle (args.Event.X-5,args.Event.Y-5,10,10);
						ctx.Fill();					
					}
					isDrawingObject = true;
				break;
				
				default:
				break;
				}

			break;
				
			case 3:
				if (isDrawingObject)
				using (Context ctx = new Context(View)){
						ctx.Rectangle (args.Event.X-5,args.Event.Y-5,10,10);
						ctx.Fill();					
					}
				isDrawingObject = false;
				break;
			}
			if (redrawWindow != null)
				redrawWindow();
		}
		#endregion
	}
}

