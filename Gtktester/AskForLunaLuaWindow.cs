// /**
// * Author: Mike Santiago
// */
using System;

namespace Gtktester
{
    public partial class AskForLunaLuaWindow : Gtk.Window
    {
        public AskForLunaLuaWindow()
            : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        protected void OnButtonNextClick (object sender, EventArgs e)
        {
            if (this.askforlunaluawidget1.CheckValid())
            {
                Program.ProgramSettings.LunaLuaDirectory = askforlunaluawidget1.LunaLuaPath;
                Program.SaveSettings();

                MainWindow mw = new MainWindow();
                mw.Show();
                this.Destroy();
            }
        }
    }
}

