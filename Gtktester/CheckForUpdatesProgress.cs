// /**
// * Author: Mike Santiago
// */
using System;
using System.Net;
using System.Diagnostics;
using System.Security.Permissions;

namespace Gtktester
{
    public partial class CheckForUpdatesProgress : Gtk.Window
    {
        private Version CurrentVersion;

        public CheckForUpdatesProgress(Version _CurrentVersion)
            : base(Gtk.WindowType.Toplevel)
        {
            CurrentVersion = _CurrentVersion;
            this.Build();
        }

        public bool CheckForUpdates()
        {
            using (WebClient wc = new WebClient())
            {
                string newestVersion = wc.DownloadString("http://mrmiketheripper.x10.mx/luamodulemanager/version.txt");
                if (newestVersion != "" && newestVersion != "NULL" && newestVersion != null)
                {
                    Version LatestVersion = new Version(newestVersion);
                    if (LatestVersion > CurrentVersion)
                    {
                        label1.Text = "Downloading updater..";
                        DownloadUpdater(wc);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }


        private void DownloadUpdater(WebClient wc)
        {
            wc.DownloadFile(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/Updater.exe"), Program.ProgramSettings.ConfigDirectory + System.IO.Path.DirectorySeparatorChar + "Updater.exe");
            Process p = new Process();
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "\"" + Environment.CurrentDirectory + "\"";
            p.StartInfo.FileName = Program.ProgramSettings.ConfigDirectory + System.IO.Path.DirectorySeparatorChar + "Updater.exe";
            p.Start();
            Environment.Exit(1);
        }
    }
}

