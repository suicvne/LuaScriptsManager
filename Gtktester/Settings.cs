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
        public string WohlstandJSON { get; set;}
        public bool StartMaximized { get; set; }
        public bool EnableSilentBugReporting { get; set; }
        public string OptionalUsername {get;set;}

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
            WohlstandJSON = "http://engine.wohlnet.ru/LunaLua/get.php?showversions";
            LunaLuaDirectory = null;
            StartMaximized = false;
            EnableSilentBugReporting = true;
            OptionalUsername = "";
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
                        "Could not create configuration directory at '{0}'!", 
                        ConfigDirectory);
                    md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
                    md.WindowPosition = WindowPosition.Center;
                    md.Run();
                    md.Destroy();

                    if (Program.ProgramSettings.EnableSilentBugReporting)
                    {
                        BugReporter br = new BugReporter();
                        br.SubmitSilentBugReport(String.Format("An error ocurred while creating configuration directory at: {0}\nUsername: {3}\nMessage: {1}\n\nStack Trace: {2}", ConfigDirectory, ex.Message, ex.StackTrace, Program.ProgramSettings.OptionalUsername));
                        br.Destroy();
                    }
                }
            }
        }

    }
}

