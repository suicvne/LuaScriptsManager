// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using thing2;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InformationView : Gtk.Bin
    {
        //Metadata from the script
        private LuaScriptMetadata RemoteMetadata = new LuaScriptMetadata();
        private LuaScriptMetadata LocalMetadata = null;

        private string LuaScriptRemote;
        private string LuaScriptLocal;
        private LuaModule m;

        public InformationView()
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
            scriptTitleLabel.ModifyFont(desc);
            scriptTitleLabel.SetAlignment(0, 0);

            descriptionLabel.SetAlignment(0, 0);
            //Pango.FontDescription pd = Pango.FontDescription.FromString(scriptTitleLabel.Text);
            //MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, pd.Family.ToString());
            //md.Run();
            //md.Destroy();
        }

        public void SetAllData(LuaModule mm)
        {
            scriptTitleLabel.Text = mm.ScriptName;
            m = mm;

            CheckLocalScript();

            DownloadScript();
        }

        private void CheckLocalScript()
        {
            string fullFileName = Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LuaScriptsLib" + System.IO.Path.DirectorySeparatorChar + m.LuaURL.Substring(m.LuaURL.LastIndexOf("/")).Trim('/');
            if(File.Exists(fullFileName))
            {
                using (StreamReader sr = new StreamReader(fullFileName))
                {
                    LuaScriptLocal = sr.ReadToEnd();
                    LocalMetadata = new LuaScriptMetadata();
                    ParseMetadata(LocalMetadata, LuaScriptLocal);
                }
            }
        }

        private void DownloadScript()
        {
            using (var client = new WebClient())
            {
                try
                {
                    string temp = client.DownloadString(m.LuaURL);
                    if (temp != null)
                        this.LuaScriptRemote = temp;
                }
                catch(Exception ex)
                {
                    MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Error\n" + ex.Message + "\n\nPlease email miketheripper1@gmail.com with this information!");
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.Run();
                    md.Destroy();
                }
            }

            ParseMetadata(RemoteMetadata, LuaScriptRemote);

            LoadNeededInfo();
        }

        private void ParseMetadata(LuaScriptMetadata outMetadata, string script)
        {
            if (script == "")
                return;
            string[] asLines = script.Split(new string[]{ "\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 5; i++)
            {
                if (asLines[i].StartsWith("local __title"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Title = cleaned;
                }
                else if (asLines[i].StartsWith("local __author"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Author = cleaned;
                }
                else if (asLines[i].StartsWith("local __version"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    Console.WriteLine(cleaned);
                    outMetadata.ScriptVersion = new Version(cleaned);
                }
                else if (asLines[i].StartsWith("local __url"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.URL = cleaned;
                }
                else if (asLines[i].StartsWith("local __description"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Description = cleaned;
                }
            }
        }

        private void LoadNeededInfo()
        {
            this.scriptTitleLabel.Text = String.Format("{0} by {1}", RemoteMetadata.Title, RemoteMetadata.Author);
            if (LocalMetadata == null)
            {
                this.descriptionLabel.Text = RemoteMetadata.Description + String.Format("\n\nAvailable: {0}", RemoteMetadata.ScriptVersion.ToString());
            }
            else
            {
                this.descriptionLabel.Text = RemoteMetadata.Description + String.Format("\n\nInstalled: {0}\nRemote: {1}", LocalMetadata.ScriptVersion.ToString(), RemoteMetadata.ScriptVersion.ToString());
                this.installUpdateButton.Sensitive = false;
            }
            this.usagePreview.Buffer.Text = this.m.UsageExample;

            CheckForScriptUpdates();
        }

        private void CheckForScriptUpdates()
        {
            if (LocalMetadata == null)
                return;

            if (RemoteMetadata.ScriptVersion > LocalMetadata.ScriptVersion)
            {
                this.installUpdateButton.Label = "Update";
                this.installUpdateButton.Sensitive = true;
            }
        }

        protected void OnWebButtonClicked (object sender, EventArgs e)
        {
            if (RemoteMetadata.URL != null || RemoteMetadata.URL != "NULL")
                Process.Start(RemoteMetadata.URL);
        }

        protected void OnInstallUpdateButtonClicked (object sender, EventArgs e)
        {
            string pathToLuaScriptsLib = Program.ProgramSettings.LunaLuaDirectory + System.IO.Path.DirectorySeparatorChar + "LuaScriptsLib";
            if (m.LuaURL != null || m.LuaURL != "NULL")
            {
                string toSaveToFileName = m.LuaURL.Substring(m.LuaURL.LastIndexOf("/")).Trim('/');

                Downloader d = new Downloader(m.LuaURL, pathToLuaScriptsLib + System.IO.Path.DirectorySeparatorChar + toSaveToFileName);
                d.Show();
                d.BeginDownload();
                while (d.Downloading)
                    ;
            }
            if (m.ResURL != null || m.ResURL != "NULL")
            {
                string toSaveToFileName = m.ResURL.Substring(m.LuaURL.LastIndexOf("/")).Trim('/');

                Downloader d = new Downloader(m.ResURL, pathToLuaScriptsLib + System.IO.Path.DirectorySeparatorChar + toSaveToFileName, true);
                d.Show();
                d.BeginDownload();
                while (d.Downloading)
                    ;
            }
            Refresh();
        }

        private void Refresh()
        {
            CheckLocalScript();
            DownloadScript();

            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Successfully installed LunaLua Module!");
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.Run();
            md.Destroy();
        }
    }
}

