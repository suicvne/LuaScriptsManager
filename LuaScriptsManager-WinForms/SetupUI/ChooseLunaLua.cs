using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LuaScriptsManager_WinForms.SetupUI
{
    public partial class ChooseLunaLua : UserControl
    {
        public ChooseLunaLua()
        {
            InitializeComponent();
        }

        public bool CanProceed()
        {
            if (File.Exists(lunaluaDirTextbox.Text + Path.DirectorySeparatorChar + "smbx.exe") 
                && Directory.Exists(lunaluaDirTextbox.Text + Path.DirectorySeparatorChar + "LuaScriptsLib"))
                return true;
            else
                return false;
        }

        public string GetDirectory() { return lunaluaDirTextbox.Text; }

        private void lunaluaDirTextbox_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(lunaluaDirTextbox.Text + Path.DirectorySeparatorChar + "smbx.exe") && Directory.Exists(lunaluaDirTextbox.Text + Path.DirectorySeparatorChar + "LuaScriptsLib"))
            {
                lunaluaDirTextbox.ForeColor = Color.Green;
                toolTip1.SetToolTip(lunaluaDirTextbox, "LuaScriptsLib and smbx.exe exist!");
            }
            else
            {
                lunaluaDirTextbox.ForeColor = Color.Red;
                toolTip1.SetToolTip(lunaluaDirTextbox, "LuaScriptsLib or smbx.exe don't exist!");
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select the LunaLua directory";
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                lunaluaDirTextbox.Text = fbd.SelectedPath;
            }
        }
    }
}
