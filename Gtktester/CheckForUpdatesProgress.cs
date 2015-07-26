// /**
// * Author: Mike Santiago
// */
using System;
using System.Net;
using System.Diagnostics;

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
                        wc.DownloadFile(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/Updater.exe"), Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "Updater.exe");
                        Process.Start(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "Updater.exe", "-force");
                        Environment.Exit(1);
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }

    }
}

