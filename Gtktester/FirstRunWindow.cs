using System;

namespace Gtktester
{
	public partial class FirstRunWindow : Gtk.Window
	{
		public FirstRunWindow () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

