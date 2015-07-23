using System;
using Gtk;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using thing2;
using System.Collections.Generic;
using System.Linq;

public partial class MainWindow: Gtk.Window
{
    private List<LuaModule> example = new List<LuaModule>();

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

        OnWindowLoad();
	}

    private void OnWindowLoad()
    {
        using (var client = new WebClient())
        {
            string jsonDatabase = client.DownloadString("http://mrmiketheripper.x10.mx/luamodulemanager/test.json");

            if (jsonDatabase != null)
                example = JsonConvert.DeserializeObject<List<LuaModule>>(jsonDatabase);
        }
        LoadDatabaseIntoTreeview();
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
}
