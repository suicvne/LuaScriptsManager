using LuaScriptsManager_WinForms.SetupUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LuaScriptsManager_WinForms
{
    public partial class SetupWindow : Form
    {
        private int step = 0;

        public SetupWindow()
        {
            InitializeComponent();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            //BLACKENED IS THE END
            //WINTER IT WILL SEND
            //THROWING ALL YOU SEE
            //INTO OBSCURITY
            if(step == 0)
            {
                step++;
                stepPanel.Controls[0].Hide();
                stepPanel.Controls.Add(new ChooseLunaLua { Tag = "choose", Dock = DockStyle.Fill });
            }
            else
            {
                ChooseLunaLua chooser = null;
                foreach (Control control in stepPanel.Controls)
                {
                    if (control.Tag.ToString() == "choose")
                        chooser = (ChooseLunaLua)control;
                }
                if (chooser != null)
                {
                    if (chooser.CanProceed())
                    {
                        Program.ProgramSettings.LunaLuaDirectory = chooser.GetDirectory();
                        Program.SaveSettings();
                        MainWindow mw = new MainWindow();
                        this.Hide();
                        mw.Show();
                    }
                    else
                        MessageBox.Show("Please select a valid directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
