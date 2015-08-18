using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LuaScriptsManager_WinForms
{
    static class Program
    {
        public static Settings ProgramSettings = new Settings();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
