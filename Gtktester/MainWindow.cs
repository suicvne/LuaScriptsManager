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


	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
        CheckForUpdatesProgress cfup = new CheckForUpdatesProgress(Assembly.GetExecutingAssembly().GetName().Version);
        cfup.Show();

		Build ();

        if (Program.ProgramSettings.StartMaximized)
            this.Maximize();

        this.hpaned2.Position = 170;

        OnWindowLoad();
	}

    private void OnWindowLoad()
    {
        this.Title = "LunaLua Module Manager - v" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        try
        {
            using (var client = new WebClient())
            {
                string jsonDatabase = client.DownloadString("http://mrmiketheripper.x10.mx/luamodulemanager/test.json");

                if (jsonDatabase != null)
                    example = JsonConvert.DeserializeObject<List<LuaModuleManager.LuaModule>>(jsonDatabase);
                LoadDatabaseIntoTreeview();
            }
        }
        catch(Exception ex)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                "Error loading database from: {0}\n\n{1}\nPlease contact miketheripper1@gmail.com with this information!", Program.ProgramSettings.DatabaseURL, ex.Message);
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.WindowPosition = WindowPosition.Center;
            md.Run();
            md.Destroy();
            this.Destroy();
        }

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
                "\n\nSpecial Thank you to Kevsoft for developing LunaLua", 
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
        /*MessageDialog md = new MessageDialog(null, 
            DialogFlags.Modal, 
            MessageType.Question, 
            ButtonsType.Ok, 
            @"Work in progress! Stay patient please!");
        md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
        md.Run();
        md.Destroy();*/
        SettingsUI s = new SettingsUI();
        s.Parent = this;
        s.Show();
    }
}
