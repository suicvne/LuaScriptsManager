// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using System.Diagnostics;
using System.IO;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class AskForLunaLuaWidget : Gtk.Bin
    {
        public string LunaLuaPath = "";

        public AskForLunaLuaWidget()
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

            label2.SetAlignment(0, 0);
        }

        protected void OnButton2Clicked (object sender, EventArgs e)
        {
            if (Internals.CurrentOS == InternalOperatingSystem.Linux)
            {
                MessageDialog md = new MessageDialog(null, 
                    DialogFlags.Modal, 
                    MessageType.Question, 
                    ButtonsType.YesNo, 
                    "It is highly recommended that you use my PlayOnLinux script for direct support from me\nregarding running LunaLua under Linux.\n\nWould you like to get my PlayOnLinux script? \n(Hitting no will bring you to the manual LunaLua download)");
                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                int returnVal = md.Run();
                if (returnVal == -8)
                {
                    Process.Start("https://www.playonlinux.com/en/topic-13294-Script_Super_Mario_Bros_X.html");
                }
                else
                    Process.Start("http://engine.wohlnet.ru/LunaLua");
                Console.WriteLine(returnVal);
                md.Destroy();
            }
        }

        public bool CheckValid()
        {
            string path = entry1.Text;
            if (Directory.Exists(String.Format("{0}{1}LuaScriptsLib", path, System.IO.Path.DirectorySeparatorChar)))
            {
                label3.Text = "Valid!";
                //_parent.SetNextEnabled(true);
                LunaLuaPath = path;
                return true;
            }
            else
            {
                label3.Text = "Invalid (No LuaScriptsLib Folder)";
                return false;
            }
        }

        protected void OnButton1Activated (object sender, EventArgs e)
        {
            FileChooserDialog fcd = new FileChooserDialog("Select LunaLua Directory", null, FileChooserAction.SelectFolder);
            fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
            fcd.AddButton(Stock.Ok, ResponseType.Ok);
            int ret = fcd.Run();
            if (ret == (int)ResponseType.Ok)
            {
                entry1.Text = fcd.Filename;

                CheckValid();
            }
            fcd.Destroy();
        }

        protected void OnEntry1Changed (object sender, EventArgs e)
        {
            CheckValid();
        }
    }
}

