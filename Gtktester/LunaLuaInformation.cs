// /**
// * Author: Mike Santiago
// */
using System;
using System.IO;
using System.Diagnostics;
using Gtk;
using System.Threading;

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
            if (Internals.CurrentOS != InternalOperatingSystem.Windows)
                button37.Visible = false;
        }

        public void SetWohlID(int id)
        {
            if (id == -1)
            {
                if (MainWindow.wohl.ReturnLatestVersion() > CurLunaDllVer)
                {
                    button17.Label = "Update";
                    button17.Sensitive = true;
                }
                else
                {
                    button17.Label = "Update not needed!";
                    button17.Sensitive = false;
                }
            }
            else
            {
                button17.Label = "Install this version";
                button17.Sensitive = true;
            }
            wohlId = id;
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

        /// <summary>
        /// Updates LunaLua ONLY.
        /// </summary>
        public void UpdateLunaLua()
        {
            if (!Directory.Exists(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "temp"))
                Directory.CreateDirectory(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "temp");

            string url = String.Format("http://engine.wohlnet.ru/LunaLua/get.php?luaver={0}&installationType=Update&base=smbx13&fbase=1", wohlId);
            Downloader d = new Downloader(url, Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "temp" + System.IO.Path.DirectorySeparatorChar + "lunalua.zip", true, Program.ProgramSettings.LunaLuaDirectory);
            d.BeginDownload();
            d.Destroyed += (object sender, EventArgs e) => {RefreshView();};
        }

        private void ActualUpdate()
        {
            
        }

        private void RefreshView()
        {
            if (File.Exists(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LunaDll.dll"))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LunaDll.dll");

                if (fvi.FileVersion != null)
                {
                    CurLunaDllVer = new Version(fvi.FileVersion.ToString());
                    curLunaLuaVersion.Text = "Your Current LunaLua Version is: " + CurLunaDllVer.ToString();
                    if (MainWindow.wohl.ReturnLatestVersion() > CurLunaDllVer)
                    {
                        button17.Label = "Update";
                        button17.Sensitive = true;
                    }
                    else
                    {
                        button17.Label = "Update not needed!";
                        button17.Sensitive = false;
                    }
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
                        UpdateLunaLua();
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

        protected void OnButton17Clicked (object sender, EventArgs e)
        {
            UpdateLunaLua();
        }

        protected void OnButton37Clicked (object sender, EventArgs e)
        {
            if (File.Exists(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "smbx.exe"))
                Process.Start(Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "smbx.exe");
            else
            {
                MessageDialog md = new MessageDialog(null, 
                    DialogFlags.Modal, 
                    MessageType.Question, 
                    ButtonsType.YesNo, "SMBX doesn't exist in {0}!", Program.ProgramSettings.LunaLuaDirectory);
                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                md.WindowPosition = WindowPosition.Center;
            }
        }
    }
}