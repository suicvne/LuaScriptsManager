using System;
using Gtk;

namespace Gtktester
{
	public partial class FirstRunWindow : Gtk.Window
	{
        private int screenIndex = 0;

		public FirstRunWindow () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}

        /*static void ReplaceWidget (Gtk.Widget old, Gtk.Widget neww, Gtk.Box box) 
        { 
            int pos = System.Array.IndexOf (box.Children, old); 
            if (pos < 0) 
                return; 

            bool expand, fill; 

            uint packing; 
            Gtk.PackType packType; 
            box.QueryChildPacking (old, out expand, out fill, out packing, out 
                packType); 

            box.Remove (old); 
            box.PackEnd (neww); 
            box.SetChildPacking (neww, expand, fill, packing, packType); 
            box.ReorderChild (neww, pos); 

            neww.Show (); 
            old.Destroy (); 
        }*/

        protected void OnNextButtonClicked (object sender, EventArgs e)
        {
            AskForLunaLuaWindow afllw = new AskForLunaLuaWindow();
            afllw.Show();
            this.Destroy();
        }

        public void SetNextEnabled(bool state)
        {
            this.nextButton.Sensitive = state;
        }
	}
}

