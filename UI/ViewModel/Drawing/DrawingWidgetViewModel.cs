using System;
using Cairo;
using Gtk;
using Gdk;
using Messenger;
using Core.UI;
using Core.VM;
using Core;
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

		public List<DrawingObject> ObjectsToDraw {
			get { return _model.Objects;}
		}

	
		public DrawingObject ActiveObject { get { return _model.ActiveObject; } }
		public PointD ActivePoint { get { return _model.ActivePoint; } }
		
		public ToolBarViewModel.Tools selectedTool{get; private set;}
		public DrawingObject TemporaryObject { get{ return _model.TemporaryObject;} }
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

			VMMessenger.getMessenger().sendMessage(new UpdateStatusMessage("Drawing Body. Right click to finish"));
		}
		private void EndDrawingUnconnected()
		{
			IsDrawingObject = false;
			_model.commitTemporaryObject();
			VMMessenger.getMessenger().sendMessage(new UpdateStatusMessage("Finished drawing body"));
		}
		private void EndDrawingConnected ()
		{
			IsDrawingObject = false;
			_model.TemporaryObject.Connect ();
			_model.commitTemporaryObject ();
			VMMessenger.getMessenger().sendMessage(new UpdateStatusMessage("Finished drawing body"));
		}
		
		#endregion
		
		#region Event Handlers (Main View interface)
		
		public  void MouseMoved (double x, double y)
		{
			
			_mouseX = x;
			_mouseY = y;
	
			//probably need something to implement a "snap to" feature of moments and forces
			var pointsList = _model._spatialTree.GetPointsInRange(MousePos, 10);
			if (pointsList.Count > 0)
				Console.WriteLine("X: " + pointsList[0].X + " Y: " + pointsList[0].Y);

			VMMessenger.getMessenger().sendMessage<RequestRedrawMessage>(new RequestRedrawMessage());
		}
		
		public  void ButtonPressed (uint button)
		{
			DoubleInputView dialogView;
		//	DoubleInputViewModel dialogViewModel;

			switch (button) {
			case 1:
				
				switch (selectedTool) {
				case ToolBarViewModel.Tools.SELECTION:
					
					break;
				case ToolBarViewModel.Tools.FORCE:
					//popup dialog that asks for mag/dir
					dialogView = new DoubleInputView(2);
					dialogView.ShowAll();
					if (ActiveObject != null && dialogView._inputs != null)
						ActiveObject.AddForce (new Core.Tuple<PointD,double, double> (MousePos,dialogView._inputs[0], dialogView._inputs[1]));
					break;
					
				case ToolBarViewModel.Tools.MOMENT:
					//popup dialog that asks for mag
					dialogView = new DoubleInputView(1);
					dialogView.ShowAll();
					if (ActiveObject != null && dialogView._inputs != null)
						ActiveObject.AddMoment(new Core.Tuple<PointD, double>(MousePos,dialogView._inputs[0]));
					break;
					
				case ToolBarViewModel.Tools.CONNECTED:
					if (!IsDrawingObject)
						BeginDrawingBody ();
					_model.TemporaryObject.AddPoint (MousePos);
                    break;
					
				case ToolBarViewModel.Tools.UNCONNECTED:
					if (!IsDrawingObject)
						BeginDrawingBody ();
					_model.TemporaryObject.AddPoint (MousePos);
					break;
				
				default:
					break;
				}
				_lastX = _mouseX;
				_lastY = _mouseY;
				break;
				
			case 3:
				if (IsDrawingObject) {
					
					_model.TemporaryObject.AddPoint (MousePos);
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

