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
using System.Threading;

namespace Gtktester
{
    public static class ZipArchiveExtensions
    {
        public static void ExtractToDirectory(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }
            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.Combine(destinationDirectoryName, file.FullName);
                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }
    
    public partial class Downloader : Gtk.Window
    {
        private string _URL, _location, _extractTo;
        private bool _extractNeeded, _extractingLunaLua;

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

        public Downloader(string URL, string location, bool extracingLunaLua, string extractTo) : base(Gtk.WindowType.Toplevel)
        {
            _URL = URL;
            _location = location;
            _extractingLunaLua = extracingLunaLua;
            _extractTo = extractTo;
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
                            try
                            {
                                using(ZipArchive archive = ZipFile.OpenRead(_location))
                                {
                                    foreach(ZipArchiveEntry entry in archive.Entries)
                                    {
                                        //bc apparently it's 2015 w/ .net 4.5 and this still isn't handled automatically or at least as an option
                                        if(File.Exists(hereIsWhereYouExtractToOhMyGod + System.IO.Path.DirectorySeparatorChar + entry.FullName))
                                            File.Delete(hereIsWhereYouExtractToOhMyGod + System.IO.Path.DirectorySeparatorChar + entry.FullName);
                                        entry.ExtractToFile(System.IO.Path.Combine(hereIsWhereYouExtractToOhMyGod, entry.FullName));
                                    }
                                }
                            }
                            catch(Exception ex)
                            {
                                MessageDialog md = 
                                    new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                                        "Error trying to extract the zip\n\n{0}", ex.Message);
                                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                                md.WindowPosition = WindowPosition.Center;
                                md.Run();
                                md.Destroy();
                            }
                            File.Delete(_location);
                        }
                        if(_extractingLunaLua)
                        {
                            progressbar1.Text = "Extracting...";
                            speedLabel.Text = "";

                            if (!Directory.Exists(_extractTo))
                                Directory.CreateDirectory(_extractTo);
                            try
                            {
                                using(Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "temp" + "\\lunalua.zip"))
                                {
                                    zip.ExtractAll(_extractTo, Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("ERROR: " + ex.Message);
                                BugReporter br = new BugReporter(String.Format("LunaLua Module Manager crashed while download LunaLua\n\nMessage: {0}\n\nStack Trace: {1}", ex.Message, ex.StackTrace));
                                br.Show();
                            }
                        }
                        //MessageDialog mdd = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Download complete!");
                        //mdd.Run();
                        //mdd.Destroy();
                        this.Destroy();

                    };
                webClient.DownloadProgressChanged += (object sender, DownloadProgressChangedEventArgs e) => 
                    {
                        DateTime lastUpdateTime = DateTime.Now;
                        var sc = SynchronizationContext.Current;
                        Gtk.Application.Invoke(
                            delegate 
                            {
                                //DateTime now = DateTime.Now;
                                //double sizeDiff = e.TotalBytesToReceive - e.BytesReceived;
                                //double timeDiff = stopWatch.ElapsedMilliseconds;
                                //TimeSpan interval = now - lastUpdateTime;

                                //decimal curSpeed = decimal.Round((decimal)(sizeDiff / timeDiff) / 1000, 2, MidpointRounding.AwayFromZero);
                                //speedLabel.Text = String.Format("{0} mb/s", curSpeed);
                                //Console.WriteLine(Convert.ToDouble(Convert.ToDouble(e.ProgressPercentage + ".0") / Convert.ToDouble(100.0)));
                                progressbar1.Fraction = Convert.ToDouble(Convert.ToDouble(e.ProgressPercentage + ".0") / Convert.ToDouble(100.0));
                                progressbar1.Text = String.Format("{0} kb / {1} kb", (e.BytesReceived / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d).ToString("0.00"));

                                //lastUpdateTime = now;
                            });
                       

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
                        "Error Downloading File\n\n{0}\nPlease contact miketheripper1@gmail.com with this information!", ex.Message);
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.WindowPosition = WindowPosition.Center;
                    md.Run();
                    md.Destroy();
                }

            }
        }
    }
}

