using System;
using Gtk;
using System.IO;
using Newtonsoft.Json;

namespace Gtktester
{
    public class Program
	{
        public static Settings ProgramSettings = new Settings();

		public static void Main (string[] args)
		{
            Application.Init();

            if (File.Exists(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
            {
                MainWindow mw = new MainWindow();
                mw.Show();
            }
            else
            {
                FirstRunWindow win = new FirstRunWindow();
                win.Show ();
            }
			Application.Run ();
		}


		public static void LoadSettings()
		{
            if (File.Exists(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
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

            using(StreamWriter sw = new StreamWriter(ProgramSettings.ConfigDirectory + Path.DirectorySeparatorChar + ".settings.json"))
            {
                using(JsonWriter jsw = new JsonTextWriter(sw))
                {
                    js.Serialize(jsw, ProgramSettings);
                }
            }
		}

	}
}
