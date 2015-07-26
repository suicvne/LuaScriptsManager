// /**
// * Author: Mike Santiago
// */
using System;
using System.IO;
using Gtk;

namespace Gtktester
{
    public class Internals
    {
        public static InternalOperatingSystem CurrentOS {get;set;}
    }

    public enum InternalOperatingSystem
    {
        Windows, MacOSX, Linux
    }

    public class Settings
    {
        public string ConfigDirectory { get; set; }
        public string LunaLuaDirectory {get;set;}
        public string DatabaseURL { get; set; }
        public bool StartMaximized { get; set; }

        public Settings()
        {
            OperatingSystem os = Environment.OSVersion;
            PlatformID pid = os.Platform;

            switch (pid)
            {
                case(PlatformID.MacOSX):
                    ConfigDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Library/Application Support/LuaModuleManager";
                    Internals.CurrentOS = InternalOperatingSystem.MacOSX;
                    break;
                case(PlatformID.Unix):
                    ConfigDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/.luamodulemanager";
                    Internals.CurrentOS = InternalOperatingSystem.Linux;
                    break;
                case(PlatformID.Win32NT):
                    ConfigDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\LuaModuleManager";
                    Internals.CurrentOS = InternalOperatingSystem.Windows;
                    break;
                default:
                    Console.WriteLine("Invalid OS: {0}", pid.ToString());
                    Environment.Exit(-1);
                    break;
            }

            CreateConfigDirectory();

            //Create defaults
            DatabaseURL = "http://mrmiketheripper.x10.mx/luamodulemanager/test.json";
            LunaLuaDirectory = null;
            StartMaximized = false;
        }

        private void CreateConfigDirectory()
        {
            if (!Directory.Exists(ConfigDirectory))
            {
                try
                {
                    Directory.CreateDirectory(ConfigDirectory);
                }
                catch(Exception ex)
                {
                    MessageDialog md = new MessageDialog(null, 
                        DialogFlags.Modal, 
                        MessageType.Error, 
                        ButtonsType.Ok, 
                        "Could not create configuration directory at '{0}'!\nPlease contact miketheripper1@gmail.com with this information!", 
                        ConfigDirectory);
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.WindowPosition = WindowPosition.Center;
                    md.Run();
                    md.Destroy();
                }
            }
        }

    }
}

