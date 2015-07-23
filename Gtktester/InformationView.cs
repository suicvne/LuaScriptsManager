// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using thing2;
using System.Collections.Generic;
using System.Net;
using NetLua;

namespace Gtktester
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class InformationView : Gtk.Bin
    {
        //Metadata from the script
        private string __title = "Blank";
        private string __author = "Nobody";
        private Version __version = new Version("0.0.0.0");
        private string __description = "No description provided!";
        private string __url = "http://www.google.com/";

        private string LuaScript;
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
            desc.Size = 32;
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
            DownloadScript();
        }

        private void DownloadScript()
        {
            using (var client = new WebClient())
            {
                string temp = client.DownloadString(m.LuaURL);
                if (temp != null)
                    this.LuaScript = temp;
            }

            ParseMetadata();
        }

        private void ParseMetadata()
        {
            Lua la = new Lua();
            string[] asLines = LuaScript.Split(new string[]{ "\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < 5; i++)
            {
                if (asLines[i].StartsWith("local __title"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    __title = cleaned;
                }
                else if (asLines[i].StartsWith("local __author"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    __author = cleaned;
                }
                else if (asLines[i].StartsWith("local __version"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    Console.WriteLine(cleaned);
                    __version = new Version(cleaned);
                }
                else if (asLines[i].StartsWith("local __url"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    __url = cleaned;
                }
                else if (asLines[i].StartsWith("local __description"))
                {
                    string[] split = asLines[i].Split(new char[]{ '=' }, 2);
                    string cleaned = split[1].Replace("\"", String.Empty).Replace(";", String.Empty).Trim();
                    __description = cleaned;
                }
            }
            string formatted = String.Format(
                "Title: {0}\nAuthor: {1}\nDescription: {2}\nVersion: {3}\nURL: {4}", __title, __author,
                __description, __version.ToString(), __url.ToString());

            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, formatted);
            md.Run();
            md.Destroy();
        }

    }
}

