using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WohlhabendNetworks;

namespace LuaScriptsManager_WinForms
{
    public delegate void LoadingComplete(object sender);

    public partial class MainWindow : Form
    {
        private List<LuaModule> AvailableModulesList = new List<LuaModule>();
        public static WohlJsonObj LunaLuaVersions = new WohlJsonObj(); //risky..idc
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
        public event LoadingComplete 
            LoadingDone;

        public MainWindow()
        { 
            Font = SystemFonts.MessageBoxFont;
            InitializeComponent();
            OnWindowLoad();
        }

        public void OnWindowLoad()
        {
            try
            {
                //TODO check for updates
            }
            catch
            {
                MessageBox.Show("Communication could not be established to our servers: you must be online to use the LunaLua Module Manager.\n\nPress ok to let the program crash.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-5);
            }

            if (Program.ProgramSettings.StartMaximized)
                WindowState = FormWindowState.Maximized;

            string title = String.Format("LunaLua Module Manager - v{0} - {1}", Assembly.GetExecutingAssembly().GetName().Version.ToString(),
           SplashMessages[r.Next(SplashMessages.Length)]);
            Text = title;

            try
            {
                using (var client = new WebClient())
                {
                    string jsonDatabase = client.DownloadString(Program.ProgramSettings.DatabaseURL);
                    if (jsonDatabase != null)
                        AvailableModulesList = JsonConvert.DeserializeObject<List<LuaModule>>(jsonDatabase);
                    foreach(var module in AvailableModulesList)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = module.ScriptName;
                        availModulesListView.Items.Add(lvi);
                    }
                }
                LoadLunaLuaDatabase();
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("Error loading database from: {0}\n\n{1}", Program.ProgramSettings.DatabaseURL, ex.Message), 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                Environment.Exit(-3);
            }
        }

        private void availModulesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(availModulesListView.SelectedItems.Count > 0 && availModulesListView.SelectedItems[0] != null)
            {
                var match = AvailableModulesList.FirstOrDefault(x => x.ScriptName.Equals(availModulesListView.SelectedItems[0].Text));
                luaScriptInformationView1.SetAllData(match);
            }
        }

        private void LoadLunaLuaDatabase()
        {
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("http://engine.wohlnet.ru/LunaLua/get.php?showversions");
                if (json != null)
                {
                    LunaLuaVersions = JsonConvert.DeserializeObject<WohlJsonObj>(json);
                    foreach (var version in LunaLuaVersions.versions)
                    {
                        Regex matchVersion = new Regex(@"\d+(\.\d+)+");
                        Match m = matchVersion.Match(version.version);
                        if (new Version(m.Value) == LunaLuaVersions.ReturnLatestVersion())
                            lunaLuaVersionsList.Items.Add(new ListViewItem { Text = version.version + " (Latest)" });
                        else
                            lunaLuaVersionsList.Items.Add(new ListViewItem { Text = version.version });
                    }
                }
                //TODO: Check latest version
            }
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.SaveSettings();
            Environment.Exit(0);
        }
    }
}
