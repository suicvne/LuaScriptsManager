using System;
using Gtk;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Gtktester;

public partial class MainWindow: Gtk.Window
{
    private List<LuaModuleManager.LuaModule> example = new List<LuaModuleManager.LuaModule>();
    public static WohlJsonObj wohl = new WohlJsonObj(); //risky..idc
    private string[] SplashMessages = new string[]{"Finally out of beta!", 
        "Better coded than SMBX!", 
        "Also on Linux!", 
        "Gtk Sucks!",
    "Not written in C++!",
    "Marina x Joey",
        "<@Joey> also please don't say stuff like \"daddy touch my princess parts\"", "CS:STOP", "Should work most of the time!",
        "Luigibot does what Reta don't ©",
    "Json is fun!"};
    private static Random r = new Random((int)DateTime.Now.Ticks);


	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
        try
        {
        CheckForUpdatesProgress cfup = new CheckForUpdatesProgress(Assembly.GetExecutingAssembly().GetName().Version);
        cfup.Show();
        if (cfup.CheckForUpdates() == false)
            cfup.Destroy();
        }
        catch
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                "Communication could not be established to our servers: you must be online to use the LunaLua Module Manager.\n\nPress ok so we can self destruct.");
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.WindowPosition = WindowPosition.Center;
            md.Run();
            md.Destroy();
            this.Destroy();
            Environment.Exit(-5);
        }

		Build ();

        if (Program.ProgramSettings.StartMaximized)
            this.Maximize();

        this.hpaned2.Position = 170;
        this.hpaned1.Position = 170;


        OnWindowLoad();

        this.notebook1.CurrentPage = 0;
	}

    private void OnWindowLoad()
    {
        string title = String.Format("LunaLua Module Manager - v{0} - {1}", Assembly.GetExecutingAssembly().GetName().Version.ToString(),
            SplashMessages[r.Next(SplashMessages.Length)]);
        this.Title = title;

        try
        {
            using (var client = new WebClient())
            {
                string jsonDatabase = client.DownloadString("http://mrmiketheripper.x10.mx/luamodulemanager/test.json");

                //if (jsonDatabase != null)
                    //example = JsonConvert.DeserializeObject<List<LuaModuleManager.LuaModule>>(jsonDatabase);
                LoadDatabaseIntoTreeview();
            }
        }
        catch(Exception ex)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                "Error loading database from: {0}\n\n{1}", Program.ProgramSettings.DatabaseURL, ex.Message);
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.WindowPosition = WindowPosition.Center;
            md.Run();
            md.Destroy();

            if (Program.ProgramSettings.EnableSilentBugReporting)
            {
                BugReporter br = new BugReporter();
                br.SubmitSilentBugReport(String.Format("An error ocurred while loading in the database from: {0}\nUsername: {3}\nMessage: {1}\n\nStack Trace: {2}", Program.ProgramSettings.DatabaseURL, ex.Message, ex.StackTrace, Program.ProgramSettings.OptionalUsername));
                br.Destroy();
            }

            Environment.Exit(-3);
        }

        try
        {            
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("http://engine.wohlnet.ru/LunaLua/get.php?showversions");
                if (json != null)
                {
                    wohl = JsonConvert.DeserializeObject<WohlJsonObj>(json);
                    LoadWohlDatabase();

                    this.lunaluainformation1.CheckForLunaDllUpdates();
                }
            }
        }
        catch(Exception ex)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                "Error loading database from: {0}\n\n{1}", Program.ProgramSettings.WohlstandJSON, ex.Message);
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.WindowPosition = WindowPosition.Center;
            md.Run();
            md.Destroy();

            if (Program.ProgramSettings.EnableSilentBugReporting)
            {
                BugReporter br = new BugReporter();
                br.SubmitSilentBugReport(String.Format("An error ocurred while loading in the database from: {0}\nUsername: {3}\nMessage: {1}\nStack Trace: {2}", Program.ProgramSettings.WohlstandJSON, ex.Message, ex.StackTrace, Program.ProgramSettings.OptionalUsername));
                br.Destroy();
            }

            Environment.Exit(-4);
        }
    }

    private void InitLunaLuaTreeView()
    {
        Gtk.TreeViewColumn c = new TreeViewColumn();
        c.Title = "LunaLua Version";
        lunaLuaVersionsTree.AppendColumn(c);

        Gtk.CellRendererText scriptTitleCell = new CellRendererText();
        c.PackStart(scriptTitleCell, true);
        c.AddAttribute(scriptTitleCell, "text", 0);
    }
    private void LoadWohlDatabase()
    {
        Gtk.TreeStore model = new Gtk.TreeStore(typeof(string));
        Gtk.TreeIter iter = model.AppendValues("Latest");
        model.AppendValues(iter, wohl.latest);

        iter = model.AppendValues("Older Versions");
        foreach (var ver in wohl.versions)
        {
            model.AppendValues(iter, ver.version.ToString());
        }
        this.lunaLuaVersionsTree.Model = model;
        InitLunaLuaTreeView();
    }

    private void InitTreeView()
    {
        Gtk.TreeViewColumn c = new Gtk.TreeViewColumn();
        c.Title = "Script Name";
        this.treeview1.AppendColumn(c);

        /*Gtk.ListStore model = new Gtk.ListStore(typeof(string));
        model.AppendValues("Hey");
        model.AppendValues("You work!");*/

        Gtk.CellRendererText scriptTitleCell = new CellRendererText();
        c.PackStart(scriptTitleCell, true);
        c.AddAttribute(scriptTitleCell, "text", 0);
    }

    private void LoadDatabaseIntoTreeview()
    {
        Gtk.ListStore model = new Gtk.ListStore(typeof(string));
        foreach (var module in example)
        {
            model.AppendValues(module.ScriptName);
        }
        this.treeview1.Model = model;

        InitTreeView();
    }

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
        Program.SaveSettings();

		Application.Quit ();
		a.RetVal = true;
	}

	protected void button_clicked (object sender, EventArgs e)
	{
		Console.WriteLine ("Button clicked!");
	}

    protected void treeview_SelectionChanged (object sender, EventArgs e)
    {
        TreeSelection selection = (sender as TreeView).Selection;
        TreeModel model;
        TreeIter iter;
        if (selection.GetSelected(out model, out iter))
        {
            var match = example.FirstOrDefault(x => x.ScriptName.Equals(model.GetValue(iter, 0).ToString()));
            this.informationview1.SetAllData(match);
        }
    }
    protected void OnAbout (object sender, EventArgs e)
    {
        AboutDialog ad = new AboutDialog();

        ad.WindowPosition = WindowPosition.CenterAlways;
        ad.Title = "LunaLua Module Manager";
        ad.ProgramName = "LunaLua Module Manager";
        ad.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        ad.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
        ad.Logo = ad.Icon;
        ad.Authors = new string[]
            {
                "Mike Santiago", "\n--Special Thanks to the Beta Testers :)--", "Xerx/Blank - Windows 8.1\nMarinite - Windows 7\nEnjl - Windows 7\nglitch4\nbossedit8",
                "\n\nSpecial Thank you to Wohlstand for hosting LunaLua" +
                "\nSpecial Thank you to Kevsoft for developing LunaLua", 
                "\n\n--3rd Party Libraries Used--",
                "Json.NET by Newtonsoft - http://www.newtonsoft.com/json\n  Licensed under the MIT License (MIT)",
                "MonoDevelop.Core by Xamarin/Mono - http://www.github.com/mono/monodevelop"
            };
        ad.Website = "http://www.github.com/Luigifan/LuaScriptsManager";
        ad.WebsiteLabel = "GitHub Repository";
        ad.License = @"Copyright (C) 2015 Mike Santiago        
Released under the MIT Public License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the ""Software""), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.";
        ad.WrapLicense = true;
        
        ad.Run();
        ad.Destroy();
    }
    protected void OnClosed (object sender, EventArgs e)
    {
        this.OnDeleteEvent(this, new DeleteEventArgs());
        //this.Destroy();
    }

    protected void OnPreferencesActionActivated (object sender, EventArgs e)
    {
        SettingsUI s = new SettingsUI();
        s.Show();
    }
    protected void OnConvertActionActivated (object sender, EventArgs e)
    {
        BugReporter br = new BugReporter();
        br.Show();
    }

    protected void OnLunaLuaVersionsTreeCursorChanged (object sender, EventArgs e)
    {
        TreeSelection selection = (sender as TreeView).Selection;
        TreeModel model;
        TreeIter iter;
        if (selection.GetSelected(out model, out iter))
        {
            string curSelected = model.GetValue(iter, 0).ToString();
            if (curSelected != "Latest" && curSelected != "Older Versions")
            {
                try
                {
                    var match = wohl.versions.FirstOrDefault(x => x.version.Equals(curSelected));
                    //this.lunaluainformation1
                    if (match == null)
                    {
                        lunaluainformation1.SetWohlID(-1);
                    }
                    else
                        lunaluainformation1.SetWohlID(match.id);
                }
                catch
                {
                    lunaluainformation1.SetDownloadButtonEnabled(false);
                }
            }
            else
                lunaluainformation1.SetDownloadButtonEnabled(false);

            //var match = wohl.versions.FirstOrDefault(x => x.ScriptName.Equals(model.GetValue(iter, 0).ToString()));
            //this.informationview1.SetAllData(match);

        }
    }
}
