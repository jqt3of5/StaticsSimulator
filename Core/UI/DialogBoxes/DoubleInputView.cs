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
				var label = new Gtk.Label("Input " + i);

				var into = model._inputs[i];

				input.Changed += 
				(object sender, EventArgs e) => 
				{
					Double.TryParse((sender as Gtk.Entry).Text, out into);
					Console.WriteLine("we wrote: " + into);
				};
				MainView.Put(label, 10, 10 + i*40);					
				MainView.Put(input, 80, 10 + i*40);
			}


			buttonCancel.Clicked += (o, args) => {model._inputs = null; this.Destroy();};
			buttonOk.Clicked += (o, args) => {this.Destroy();};

		}
	
	}
}

