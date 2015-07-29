// /**
// * Author: Mike Santiago
// */
using System;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class LunaLuaInformation : Gtk.Bin
    {
        private string DownloadURL {get;set;}

        public LunaLuaInformation()
        {
            this.Build();
        }

        public void SetDownloadButtonEnabled(bool enabled)
        {
            button17.Sensitive = enabled;
        }
    }
}

