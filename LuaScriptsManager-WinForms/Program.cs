using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            if (File.Exists(Program.ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
            {
                LoadSettings();
                Application.Run(new MainWindow());
            }
            else
            {
                Application.Run(new SetupWindow());
            }
        }

        static bool DisplayWindowsOkCancelMessage(string message, string caption)
        {
            var name = typeof(int).Assembly.FullName.Replace("mscorlib", "System.Windows.Forms");
            var asm = Assembly.Load(name);
            var md = asm.GetType("System.Windows.Forms.MessageBox");
            var mbb = asm.GetType("System.Windows.Forms.MessageBoxButtons");
            var okCancel = Enum.ToObject(mbb, 1);
            var dr = asm.GetType("System.Windows.Forms.DialogResult");
            var ok = Enum.ToObject(dr, 1);

            const BindingFlags flags = BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static;
            return md.InvokeMember("Show", flags, null, null, new object[] { message, caption, okCancel }).Equals(ok);
        }

        public static void LoadSettings()
        {
            if(File.Exists(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
            {
                JsonSerializer js = new JsonSerializer();
                js.Formatting = Formatting.Indented;
                using (StreamReader sr = new StreamReader(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
                {
                    using (JsonReader jsr = new JsonTextReader(sr))
                    {
                        ProgramSettings = js.Deserialize<Settings>(jsr);
                    }
                }
            }
        }

        public static void SaveSettings()
        {
                JsonSerializer js = new JsonSerializer();
                js.Formatting = Formatting.Indented;
                using (StreamWriter sr = new StreamWriter(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
                {
                    using (JsonWriter jsr = new JsonTextWriter(sr))
                    {
                    js.Serialize(jsr, ProgramSettings);
                    }
                }
        }
    }
}
