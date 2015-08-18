using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace LuaScriptsManager_WinForms
{
    public partial class LuaScriptInformationView : UserControl
    {
        private LuaScriptMetadata RemoteMetadata = new LuaScriptMetadata();
        private LuaScriptMetadata LocalMetadata = null;

        private string LuaScriptRemote;
        private string LuaScriptLocal;
        private LuaModule m;

        public LuaScriptInformationView()
        {
            InitializeComponent();
            installButton.Enabled = false;
            websiteButton.Enabled = false;
        }

        public void SetAllData(LuaModule module)
        {
            titleLabel.Text = module.ScriptName;
            m = module;
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

        private void LoadNeededInfo()
        {
            titleLabel.Text = String.Format("{0} by {1}", RemoteMetadata.Title, RemoteMetadata.Author);
            if(LocalMetadata == null)
            {
                descriptionLabel.Text = RemoteMetadata.Description + String.Format("\n\nAvailable: {0}", RemoteMetadata.ScriptVersion.ToString());
                installButton.Enabled = true;
                websiteButton.Enabled = true;
            }
            else
            {
                descriptionLabel.Text = RemoteMetadata.Description + String.Format("\n\nInstalled: {0}\nRemote: {1}", LocalMetadata.ScriptVersion.ToString(), RemoteMetadata.ScriptVersion.ToString());
                installButton.Enabled = false;
            }
            usageExample.Text = m.UsageExample;
            CheckForScriptUpdates();
        }

        private void CheckForScriptUpdates()
        {
            if (LocalMetadata == null)
                return;
            if(RemoteMetadata.ScriptVersion > LocalMetadata.ScriptVersion)
            {
                installButton.Text = "Update";
                installButton.Enabled = true;
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
                    {
                        this.LuaScriptRemote = temp;
                        ParseMetadata(RemoteMetadata, LuaScriptRemote);
                        LoadNeededInfo();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error Downloading Lua Script", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //TODO
                }
            }
        }

        private void ParseMetadata(LuaScriptMetadata outMetadata, string script)
        {
            if (script == "")
                return;
            string[] asLines = script.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 5; i++)
            {
                if (asLines[i].StartsWith("local __title"))
                {
                    string[] split = asLines[i].Split(new char[] { '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Title = cleaned;
                }
                else if (asLines[i].StartsWith("local __author"))
                {
                    string[] split = asLines[i].Split(new char[] { '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Author = cleaned;
                }
                else if (asLines[i].StartsWith("local __version"))
                {
                    string[] split = asLines[i].Split(new char[] { '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    Console.WriteLine(cleaned);
                    outMetadata.ScriptVersion = new Version(cleaned);
                }
                else if (asLines[i].StartsWith("local __url"))
                {
                    string[] split = asLines[i].Split(new char[] { '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.URL = cleaned;
                }
                else if (asLines[i].StartsWith("local __description"))
                {
                    string[] split = asLines[i].Split(new char[] { '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    outMetadata.Description = cleaned;
                }
            }
        }
        
        private void websiteButton_Click(object sender, EventArgs e)
        {
            if (RemoteMetadata.URL != null || RemoteMetadata.URL != "NULL")
                Process.Start(RemoteMetadata.URL);
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            string luaScriptsLibPath = Program.ProgramSettings.LunaLuaDirectory + Path.DirectorySeparatorChar + "LuaScriptsLib";
            //TODO
        }

        private void RefreshInfo()
        {
            CheckLocalScript();
            DownloadScript();
            MessageBox.Show("Successfully installed module!", "Module Installed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
