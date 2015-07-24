// /**
// * Author: Mike Santiago
// */
using System;
using System.Net;

namespace Gtktester
{
    public partial class Changelog : Gtk.Dialog
    {
        public Changelog()
        {
            this.Build();
            DownloadChangelog();
        }

        private void DownloadChangelog()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string changelogFile = wc.DownloadString(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/changelog.txt"));
                    this.textview1.Buffer.Text = changelogFile;
                }
                catch
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Destroy();
                }

            }

        }

        protected void OnButtonOkClicked (object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Destroy();
        }
    }
}

