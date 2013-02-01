using System;
using Cairo;
using Gtk;
using Gdk;
using Messenger;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
namespace ViewModel
{
	public class DrawingWidgetViewModel
	{ 
		#region private members
		private double _mouseX;
		private double _mouseY;
		private double _lastX;
		private double _lastY;
		private DrawingWidgetModel _model;
		#endregion
		
		#region Properties
		
		public PointD MousePos { get{ return new PointD(_mouseX, _mouseY);} }
		public PointD LastPos { get{ return new PointD(_lastX, _lastY); } }

		public bool IsDrawingObject{get; private set;}
		
		public DrawingObject tempObject{ get; private set; }
		
		public List<DrawingObject> ObjectsToDraw {
			get { return _model.Objects;}
		}
		public DrawingObject ActiveObject { get; private set; }
		public PointD ActivePoint { get; private set; }
		
		public ToolBarViewModel.Tools selectedTool{get; private set;}

		#endregion
		
		#region Constructors
		public DrawingWidgetViewModel ()
		{
			IsDrawingObject = false;
			selectedTool = ToolBarViewModel.Tools.NONE;
			_model = new DrawingWidgetModel();
		
			VMMessenger.getMessenger().register<NewToolChosenMessage>(RequestToolChange);			
		}
		#endregion
		
		#region Message Handlers
		
		private void RequestToolChange(NewToolChosenMessage tool)
		{
			Console.WriteLine("changed tool");
			selectedTool = tool.tool;
		}
		#endregion
		
		#region Private Methods
		private void BeginDrawingBody()
		{
			IsDrawingObject = true;
			tempObject = new DrawingObject();
		}
		private void EndDrawingUnconnected()
		{
			IsDrawingObject = false;
			_model.commitTemporaryObject(tempObject);
		}
		private void EndDrawingConnected ()
		{
			IsDrawingObject = false;
			tempObject.Connect ();
			_model.commitTemporaryObject (tempObject);
			
		}
		
		#endregion
		
		#region Event Handlers (Main View interface)
		
		public  void MouseMoved (double x, double y)
		{
			
			_mouseX = x;
			_mouseY = y;
	
			//probably need something to implement a "snap to" feature of moments and forces
			
			VMMessenger.getMessenger().sendMessage<RequestRedrawMessage>(new RequestRedrawMessage());
		}
		
		public  void ButtonPressed (uint button)
		{
			switch (button) {
			case 1:
				
				switch (selectedTool) {
				case ToolBarViewModel.Tools.SELECTION:
					
					break;
				case ToolBarViewModel.Tools.FORCE:
					//popup dialog that asks for mag/dir
					//ActiveObject.AddForce (new Tuple<PointD,double, double> ());
					var dialog = new DoubleInput(2);
					break;
					
				case ToolBarViewModel.Tools.MOMENT:
					//popup dialog that asks for mag
					//ActiveObject.AddMoment(new Tuple<PointD, double>());
					break;
					
				case ToolBarViewModel.Tools.CONNECTED:
					if (!IsDrawingObject)
						BeginDrawingBody ();
					tempObject.AddPoint (MousePos);
					break;
					
				case ToolBarViewModel.Tools.UNCONNECTED:
					if (!IsDrawingObject)
						BeginDrawingBody ();
					tempObject.AddPoint (MousePos);
					break;
				
				default:
					break;
				}
				_lastX = _mouseX;
				_lastY = _mouseY;
				break;
				
			case 3:
				if (IsDrawingObject) {
					
					tempObject.AddPoint (MousePos);
					switch (selectedTool) {
					case ToolBarViewModel.Tools.CONNECTED:
						EndDrawingConnected ();
						break;
						
					case ToolBarViewModel.Tools.UNCONNECTED:
						EndDrawingUnconnected ();
						break;
					
					}
						
				}
				break;
			}
			VMMessenger.getMessenger ().sendMessage<RequestRedrawMessage> (new RequestRedrawMessage ());
		}
		#endregion
	}
}

