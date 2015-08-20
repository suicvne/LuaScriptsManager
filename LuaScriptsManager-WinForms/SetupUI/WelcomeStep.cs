using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LuaScriptsManager_WinForms.SetupUI
{
    public partial class WelcomeStep : UserControl
    {
        public WelcomeStep()
        {
            InitializeComponent();
            label1.Font = new Font(label1.Font.FontFamily, 20.0f);
        }

        private void WelcomeStep_Load(object sender, EventArgs e)
        {
            
        }
    }
}
