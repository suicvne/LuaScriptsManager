// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class FirstRunWidget : Gtk.Bin
    {
        public FirstRunWidget()
        {
            this.Build();
            SetFonts();
        }
        private void SetFonts()
        {
            Pango.FontDescription desc = new Pango.FontDescription();
            desc.Family = "Sans";
            desc.Size = (int)(20 * Pango.Scale.PangoScale);
            desc.Weight = Pango.Weight.Normal;
            label1.ModifyFont(desc);
        }
    }
}

