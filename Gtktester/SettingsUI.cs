using System;
using Gtk;
using System.IO;
using System.Diagnostics;

namespace Gtktester
{
    public partial class SettingsUI : Gtk.Window
    {
        private bool safeToSave = false;

        public SettingsUI()
            : base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            LoadSettingsIn();
        }

        private void LoadSettingsIn()
        {
            lunaLuaDirEntry.Text = Program.ProgramSettings.LunaLuaDirectory;
            databaseLocationEntry.Text = Program.ProgramSettings.DatabaseURL;
            startAppMaximizedCheck.Active = Program.ProgramSettings.StartMaximized;
        }

        private int SaveSettings()
        {
            MessageDialog md;
            if (databaseLocationEntry.Text.Trim() != Program.ProgramSettings.DatabaseURL.Trim())
            {
                Program.ProgramSettings.DatabaseURL = databaseLocationEntry.Text.Trim();

                md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, 
                    ButtonsType.Ok, "The follow changes won't take effect until the program is restarted:\n\nDatabase URL->{0}", databaseLocationEntry.Text);
                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                md.WindowPosition = WindowPosition.Center;
                md.Run();
                md.Destroy();
            }
            Program.ProgramSettings.LunaLuaDirectory = lunaLuaDirEntry.Text.Trim();
            Program.ProgramSettings.StartMaximized = startAppMaximizedCheck.Active;
            if (!System.IO.Directory.Exists(lunaLuaDirEntry.Text + System.IO.Path.DirectorySeparatorChar + "LuaScriptsLib"))
            {
                return -1;
            }

                md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Settings saved successfully!");
                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                md.WindowPosition = WindowPosition.Center;
                md.Run();
                md.Destroy();
            safeToSave = true;
            return 0;
        }

        protected void OnButton21Clicked (object sender, EventArgs e)
        {
            int result = SaveSettings();
            if (result == -1)
            {
                MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                                 "The selected directory does not contain a valid LunaLua installation\n\nNo LuaScriptsLib folder found!");
                md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                md.WindowPosition = WindowPosition.Center;
                md.Run();
                md.Destroy();
            }
            else if (result == 0)
            {
                if (safeToSave)
                {
                    Program.SaveSettings();
                    this.Destroy();
                }
            }
        }

        protected void OnResetDatabaseBtnClicked (object sender, EventArgs e)
        {
            Program.ProgramSettings.DatabaseURL = "http://mrmiketheripper.x10.mx/luamodulemanager/test.json";
            databaseLocationEntry.Text = Program.ProgramSettings.DatabaseURL;
        }

        protected void OnButton41Clicked (object sender, EventArgs e)
        {
            FileChooserDialog fcd = new FileChooserDialog("Select LunaLua Directory", null, FileChooserAction.SelectFolder);
            fcd.AddButton(Stock.Cancel, ResponseType.Cancel);
            fcd.AddButton(Stock.Ok, ResponseType.Ok);
            int ret = fcd.Run();
            if (ret == (int)ResponseType.Ok)
            {
                lunaLuaDirEntry.Text = fcd.Filename;
            }
            fcd.Destroy();
        }

        protected void OnLunaLuaDirEntryChanged (object sender, EventArgs e)
        {
            string lunaDllPath = this.lunaLuaDirEntry.Text + System.IO.Path.DirectorySeparatorChar + "LunaDll.dll";
            if (File.Exists(lunaDllPath))
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(lunaDllPath);
                if (fvi.FileVersion == null || fvi.FileVersion == "")
                    lblLunaLuaVersion.Text = "LunaLua outdated!";
                else
                    lblLunaLuaVersion.Text = "LunaLua Version: " + fvi.FileVersion;
            }
            else
            {
                lblLunaLuaVersion.Text = "";
            }
        }
    }
}

