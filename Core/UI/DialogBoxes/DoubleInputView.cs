using System;
using Core.VM;
namespace Core.UI
{
	public partial class DoubleInputView : Gtk.Dialog
	{
		public double []_inputs;
		public DoubleInputView (int count)
		{
			this.Build ();
			_inputs = new double[count];

			for (int i = 0; i < count; ++i) 
			{
				var input = new Gtk.Entry();
				var label = new Gtk.Label("Input " + i);

				var into = _inputs[i];

				input.Changed += 
				(object sender, EventArgs e) => 
				{
					Double.TryParse((sender as Gtk.Entry).Text, out into);
					Console.WriteLine("we wrote: " + into);
				};
				MainView.Put(label, 10, 10 + i*40);					
				MainView.Put(input, 80, 10 + i*40);
			}
			buttonCancel.Clicked += (o, args) => {_inputs = null; this.Destroy();};
			buttonOk.Clicked += (o, args) => {this.Destroy();};
		}
	}
}

