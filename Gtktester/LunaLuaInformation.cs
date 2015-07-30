// /**
// * Author: Mike Santiago
// */
using System;
using System.IO;
using System.Diagnostics;
using Gtk;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class LunaLuaInformation : Gtk.Bin
    {
        private string DownloadURL {get;set;}
        private Version CurLunaDllVer {get;set;}
        public int wohlId { get; set;}

        public LunaLuaInformation()
        {
            this.Build();
            CheckLunaDllVersion();
        }

        private void CheckLunaDllVersion()
        {
            if (File.Exists(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LunaDll.dll"))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LunaDll.dll");

                if (fvi.FileVersion != null)
                {
                    CurLunaDllVer = new Version(fvi.FileVersion.ToString());
                    curLunaLuaVersion.Text = "Your Current LunaLua Version is: " + CurLunaDllVer.ToString();

                }
                else
                {
                    CurLunaDllVer = new Version("0.0.0.0");
                    curLunaLuaVersion.Text = "Your Current LunaLua Version is: Outdated! No file metadata!";
                }
            }
        }

        public void CheckForLunaDllUpdates()
        {
            if (MainWindow.wohl != null)
            {
                Version actualLatest = MainWindow.wohl.ReturnLatestVersion();
                if (actualLatest > CurLunaDllVer)
                {
                    MessageDialog md = new MessageDialog(null, 
                        DialogFlags.Modal, 
                        MessageType.Question, 
                        ButtonsType.YesNo, "A new LunaLua version is available!\n\nYour Version: {0}\nLatest: {1}\n\nWould you like to update?", CurLunaDllVer, actualLatest);
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.WindowPosition = WindowPosition.Center;
                    ResponseType res = (ResponseType)md.Run();
                    if (res == ResponseType.Yes)
                    {
                        //TODO: start the update process

                    }
                    else
                    {
                    }
                    md.Destroy();
                }
            }
        }

        public void SetDownloadButtonEnabled(bool enabled)
        {
            button17.Sensitive = enabled;
        }
    }
}

