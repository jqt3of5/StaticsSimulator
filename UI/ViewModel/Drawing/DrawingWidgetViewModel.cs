using System;
using Cairo;
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
		
		public PointDouble MousePos { get{ return new PointDouble(_mouseX, _mouseY);} }
		public PointDouble LastPos { get{ return new PointDouble(_lastX, _lastY); } }

		public bool IsDrawingObject{get; private set;}

		public bool IsClicked {get; private set;}

		public List<DrawingObject> ObjectsToDraw {
			get { return _model.Objects;}
		}
		
		public bool IsJoiningPoints {get; private set;}
	
		public DrawingObject ActiveObject { get { return _model.ActiveObject; } }
		public PointDouble ActivePoint { get { return _model.ActivePoint; } private set { _model.ActivePoint = value; } }
		
		public ToolBarViewModel.Tools selectedTool{get; private set;}
		public DrawingObject TemporaryObject {
			get{ return _model.TemporaryObject;}
			private set{_model.TemporaryObject = value;}}
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
			VMMessenger.getMessenger().sendMessage(new UpdateStatusMessage("Tool changed to " + tool.tool.ToString()));
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
			(_model.TemporaryObject as ConnectedObject).Connect();
			_model.commitTemporaryObject ();
			VMMessenger.getMessenger().sendMessage(new UpdateStatusMessage("Finished drawing body"));
		}
		private void GrabActiveObject()
		{
			_model.ActivePoint = _model.SpatialTree.GetClosestPoint (MousePos);
			if (_model.ActivePoint != null && _model.PointToParent.ContainsKey (_model.ActivePoint))
				_model.ActiveObject = _model.PointToParent [_model.ActivePoint];
			
		}
		#endregion
		
		#region Event Handlers (Main View interface)
		
		public  void MouseMoved (double x, double y)
		{
			
			_mouseX = x;
			_mouseY = y;

			
			if (IsClicked && selectedTool == ToolBarViewModel.Tools.SELECTION) 
			{
				_model.ActivePoint.X = _mouseX;
				_model.ActivePoint.Y = _mouseY;
				_model.ActiveObject.CalcCenterOfMass();
			} 
			
			
			VMMessenger.getMessenger().sendMessage<RequestRedrawMessage>(new RequestRedrawMessage());
			VMMessenger.getMessenger().sendMessage<UpdatePositionStatusMessage>(new UpdatePositionStatusMessage(_mouseX, _mouseY));

		}
	
		public void ButtonReleased (uint button)
		{
			switch (button) 
			{
			case 1:
				switch (selectedTool)
				{
				case ToolBarViewModel.Tools.SELECTION:
					IsClicked = false;
					break;
				}
				break;
			}


		}
		public  void ButtonPressed (uint button)
		{
			DoubleInputView dialogView;
			DoubleInputModel dialogModel;

			switch (button) {
			case 1:
				
				switch (selectedTool) {
				case ToolBarViewModel.Tools.SELECTION:
					IsClicked = true;
					
					GrabActiveObject();
					break;
					
				case ToolBarViewModel.Tools.JOINT:
					if (!IsJoiningPoints)	
					{
						IsJoiningPoints = true;
						GrabActiveObject();
						
					}
					else
					{
						IsJoiningPoints = false;
						var point1 = ActivePoint;
						var body1 = ActiveObject;
						
						GrabActiveObject();
						
						_model.Controller.AddJoint(point1, ActivePoint);
					}
					break;
					
				case ToolBarViewModel.Tools.FORCE:
					//popup dialog that asks for mag/dir
					dialogModel = new DoubleInputModel(2, new string[]{"Angle", "Magnitude"}); 
					dialogView = new DoubleInputView(dialogModel);
					dialogView.ShowAll();
					dialogView.Run();
					
					GrabActiveObject();
					if (ActiveObject != null && dialogModel._inputs != null)
						ActiveObject.AddForce (new Tuple<PointDouble,double, double> (ActivePoint,dialogModel._inputs[0],dialogModel._inputs[1]));
					//_model.Controller.AddForce(new Tuple<PointDouble,double, double> (ActivePoint,dialogModel._inputs[0],dialogModel._inputs[1]));
					
					break;
					
				case ToolBarViewModel.Tools.MOMENT:
					//popup dialog that asks for mag
					dialogModel = new DoubleInputModel(1, new string[]{"Magnitude"}); 
					dialogView = new DoubleInputView(dialogModel);
					dialogView.ShowAll();
					dialogView.Run();
					
					GrabActiveObject();
					
					if (ActiveObject != null && dialogModel._inputs != null)
						ActiveObject.AddMoment (new Tuple<PointDouble,double> (ActivePoint,dialogModel._inputs[0]));
					//_model.Controller.AddForce(new Tuple<PointDouble,double> (ActivePoint,dialogModel._inputs[0]));
					
					break;
					
				case ToolBarViewModel.Tools.CONNECTED:
					if (!IsDrawingObject)
					{
						BeginDrawingBody ();
						TemporaryObject = new ConnectedObject();
					}
					ActivePoint = MousePos;
					_model.TemporaryObject.AddPoint (ActivePoint);
                    break;
					
				case ToolBarViewModel.Tools.UNCONNECTED:
					if (!IsDrawingObject)
					{
						BeginDrawingBody ();
						TemporaryObject = new UnconnectedObject();
					}
					ActivePoint = MousePos;
					_model.TemporaryObject.AddPoint (ActivePoint);
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

