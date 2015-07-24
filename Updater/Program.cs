// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;

namespace Updater
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            if (args.Length >= 1)
                if (args[0] != "-force")
                    Environment.Exit(-666);
            else
                Environment.Exit(-666);

            Application.Init();
            MainWindow win = new MainWindow();
            win.Show();

            Application.Run();
        }
    }
}
