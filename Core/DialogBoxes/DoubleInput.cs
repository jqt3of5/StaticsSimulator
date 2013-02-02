using System;

namespace Core.UI
{
	public partial class DoubleInput : Gtk.Dialog
	{
		double [] outputs;
		public DoubleInput (int inputCounts)
		{
			//this.Build ();

			outputs = new double[inputCounts];

			for (int i = 0; i < inputCounts; ++i) 
			{
				var input = new Gtk.Entry();
				input.EditingDone += 
				(object sender, EventArgs e) => 
				{
					Double.TryParse((sender as Gtk.Entry).Text, out outputs[i]);
				};
					
				MainView.Put(input, 20, 10 + i*10);
			}

		}
	}
}

