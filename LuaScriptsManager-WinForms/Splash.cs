using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LuaScriptsManager_WinForms
{
    public partial class Splash : Form
    {
        static MainWindow main = new MainWindow();
        public Splash()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        public void MainWindowConstructor()
        {
            main.OnWindowLoad();
        }

        Thread t;
        private void Splash_Load(object sender, EventArgs e)
        {
            // t = new Thread(MainWindowConstructor);
            //t.Start();

            main.LoadingDone += (ssender) =>
            {
                this.Hide();
            };
            main.Invoke((MethodInvoker)delegate () 
            {
                main.OnWindowLoad();
                main.Show();
            });
        }

        private void ShowStuff()
        {
            //t.Abort();
            main.Show();
            this.Hide();
        }
    }
}
