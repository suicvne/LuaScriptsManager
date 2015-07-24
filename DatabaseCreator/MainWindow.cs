// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public partial class MainWindow: Gtk.Window
{
    private string CurrentOpenFile = "";
    private List<LuaModuleManager.LuaModule> db = new List<LuaModuleManager.LuaModule>();

    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
        SetupTableview();
    }

    private void SetupTableview()
    {
        Gtk.TreeViewColumn c = new Gtk.TreeViewColumn();
        c.Title = "Module";
        this.treeview2.AppendColumn(c);

        /*Gtk.ListStore model = new Gtk.ListStore(typeof(string));
        model.AppendValues("Hey");
        model.AppendValues("You work!");*/

        Gtk.CellRendererText scriptTitleCell = new CellRendererText();
        c.PackStart(scriptTitleCell, true);
        c.AddAttribute(scriptTitleCell, "text", 0);
    }

    private void LoadDatabaseIntoView()
    {
        Gtk.ListStore model = new Gtk.ListStore(typeof(string));
        foreach (var module in this.db)
        {
            model.AppendValues(module.ScriptName);
        }
        this.treeview2.Model = model;
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    } 

    protected void OnAboutActionActivated (object sender, EventArgs e)
    {
        AboutDialog ad = new AboutDialog();
        ad.Title = "I know this is crappy but it hopefully works";
        ad.Authors = new String[]{ "Mike Santiago" };
        ad.Version = "1.0.0.0";


        ad.Run();
        ad.Destroy();
    }

    private void OpenFile()
    {
        Gtk.FileChooserDialog fcd = new Gtk.FileChooserDialog("Open JSON File", this, FileChooserAction.Open);
        fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
        fcd.AddButton(Stock.Ok, ResponseType.Ok);
        fcd.Filter = new FileFilter();
        fcd.Filter.AddPattern("*json");
        if (fcd.Run() == (int)ResponseType.Ok)
        {
            //TODO: open the file lmao
            JsonSerializer jsr = new JsonSerializer();
            using (StreamReader sr = new StreamReader(fcd.Filename))
            {
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    this.db = jsr.Deserialize<List<LuaModule>>(jr);
                }
            }
            LoadDatabaseIntoView();
        }

        fcd.Destroy();
    }

    protected void OnOpenActionActivated (object sender, EventArgs e)
    {
        OpenFile();
    }

    protected void OnTreeview2CursorChanged (object sender, EventArgs e)
    {
        //LuaModule toChange = this.db[this.edit1.Index];
        LuaModule changed = this.edit1.GetCurrentModule();
        this.db[this.edit1.Index] = changed;

    }
}
