// /**
// * Author: Mike Santiago
// */
using System;
using System.Net;
using System.Diagnostics;
using Gtk;
using Internals;
using System.IO;
using System.IO.Compression;

namespace Gtktester
{
    public partial class Downloader : Gtk.Window
    {
        private string _URL, _location;
        private bool _extractNeeded;

        public bool Downloading {get;set;}

        WebClient webClient;
        Stopwatch stopWatch = new Stopwatch();

        public Downloader()
            : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        public Downloader(string URL, string location) : base(Gtk.WindowType.Toplevel)
        {
            _URL = URL;
            _location = location;
            _extractNeeded = false;
            this.Build();
        }

        public Downloader(string URL, string location, bool extractNeeded) : base(Gtk.WindowType.Toplevel)
        {
            _URL = URL;
            _location = location;
            _extractNeeded = extractNeeded;
            this.Build();
        }

        public void BeginDownload()
        {
            using (webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += (object sender, System.ComponentModel.AsyncCompletedEventArgs e) => 
                    {
                        Downloading = false;
                        stopWatch.Reset();

                        if(_extractNeeded)
                        {
                            progressbar1.Text = "Extracting..";
                            string dirToExtract = _location.Substring(_location.LastIndexOf(System.IO.Path.DirectorySeparatorChar)).Trim(System.IO.Path.DirectorySeparatorChar);
                            string dirName = _URL.Substring(_URL.LastIndexOf("/")).Trim('/');
                            string[] trimExtension = dirName.Split(new char[] {'.'}, 2);

                            string hereIsWhereYouExtractToOhMyGod = Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LuaScriptsLib" + System.IO.Path.DirectorySeparatorChar + trimExtension[0];

                            if(!Directory.Exists(hereIsWhereYouExtractToOhMyGod))
                                Directory.CreateDirectory(hereIsWhereYouExtractToOhMyGod);
                            using(ZipArchive archive = ZipFile.OpenRead(_location))
                            {
                                foreach(ZipArchiveEntry entry in archive.Entries)
                                {
                                    entry.ExtractToFile(System.IO.Path.Combine(hereIsWhereYouExtractToOhMyGod, entry.FullName));
                                }
                            }
                            File.Delete(_location);
                        }
                        //MessageDialog mdd = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Download complete!");
                        //mdd.Run();
                        //mdd.Destroy();
                        this.Destroy();
                    };
                webClient.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) => 
                    {
                        speedLabel.Text = String.Format("{0} mb/s", (e.BytesReceived / 1024d / 1024d / stopWatch.Elapsed.TotalSeconds));
                        progressbar1.Fraction = e.ProgressPercentage;
                        progressbar1.Text = String.Format("{0} kb / {1} kb", (e.BytesReceived / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d).ToString("0.00"));

                    };

                Uri iUrl = _URL.StartsWith("http", StringComparison.OrdinalIgnoreCase) ? new Uri(_URL) : new Uri("http://" + _URL);

                stopWatch.Start();
                try
                {
                    webClient.DownloadFileAsync(iUrl, _location);
                    Downloading = true;
                }
                catch(Exception ex)
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Error Downloading File\n\n{0}\nPlease email miketheripper1@gmail.com with this information!", ex.Message);
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.Run();
                    md.Destroy();
                }

            }
        }
    }
}

