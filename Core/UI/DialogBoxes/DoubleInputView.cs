using System;
using Core.VM;
namespace Core.UI
{
	public partial class DoubleInputView : Gtk.Dialog
	{
		public DoubleInputView (DoubleInputModel model)
		{

			this.Build ();


			for (int i = 0; i < model.Count; ++i) 
			{
				var input = new Gtk.Entry();
				var label = new Gtk.Label(model._labels[i] + ": ");
				
				//The index i was being "captured" by referance in the anonymous function.\
				//This makes a copy
				int j = i;
				input.Changed += 
				(object sender, EventArgs e) => 
				{
					Double.TryParse((sender as Gtk.Entry).Text, out model._inputs[j]);
					Console.WriteLine("we wrote: " + model._inputs[j] + " j: " + j);
				};
				MainView.Put(label, 10, 10 + i*40);					
				MainView.Put(input, 120, 10 + i*40);
			}


			buttonCancel.Clicked += (o, args) => {model._inputs = null; this.Destroy();};
			buttonOk.Clicked += (o, args) => {this.Destroy();};

		}
	
	}
}

