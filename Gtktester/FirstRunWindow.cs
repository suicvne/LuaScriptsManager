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

        static void ReplaceWidget (Gtk.Widget old, Gtk.Widget neww, Gtk.Box box) 
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
        } 

        protected void OnNextButtonClicked (object sender, EventArgs e)
        {
            if (screenIndex == 1)
            {
                AskForLunaLuaWidget l = (AskForLunaLuaWidget)this.vbox2.Children[0];
                Program.ProgramSettings.LunaLuaDirectory = l.LunaLuaPath;

                Program.SaveSettings();
                this.Destroy();

                MainWindow mw = new MainWindow();
                mw.Show();
            }

            screenIndex++;
            Gtk.Widget nextWidget = null;
            switch (screenIndex)
            {
                case(0):
                    nextWidget = new FirstRunWidget();
                    break;
                case(1):
                    nextWidget = new AskForLunaLuaWidget(this);
                    break;
            }
            if (nextWidget != null)
            {
                ReplaceWidget(this.vbox2.Children[0], nextWidget, vbox2);
            }
        }

        public void SetNextEnabled(bool state)
        {
            this.nextButton.Sensitive = state;
        }
	}
}

