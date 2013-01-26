using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Messenger
{
	public class VMMessenger
	{
		#region Private members
		private static VMMessenger messenger = null;
		
		
		private Dictionary<System.Type, Delegate> recievers;
		#endregion
		
		#region Singleton Get
		public static VMMessenger getMessenger()
		{
		 	
			if (messenger == null){
				messenger = new VMMessenger();
				return messenger;
			} else
				return messenger;
		}
		#endregion
		
		#region Constructors
		private VMMessenger ()
		{
			recievers = new Dictionary<System.Type, Delegate>();
		}
		#endregion
		
		#region Public Methods
		public void register<T>(Action<T> action)
		{
			if (!recievers.ContainsKey(typeof(T))){
				recievers[typeof(T)] = action;
				return;
			}
			recievers[typeof(T)] = Action.Combine(recievers[typeof(T)],action);
		}
		public void unregister<T>(Action<T> action)
		{
			if (!recievers.ContainsKey(typeof(T)))
				return;
			
			recievers[typeof(T)] = Action.Remove(recievers[typeof(T)],action);
		
		}
		
		public void sendMessage<T>(T msg)
		{
			
			if (recievers != null &&
			    recievers.ContainsKey(typeof(T)) &&
			    recievers[typeof(T)] != null)
			{
				Action<T> foo = (Action<T>)recievers[typeof(T)];
				foo(msg);
			}
		}
		#endregion
		
	}
}

