using System;
using Gtk;
using System.IO;
using Newtonsoft.Json;

namespace Gtktester
{
	class MainClass
	{
        //public static SettingsObject SettingsInstance;

		public static void Main (string[] args)
		{
            /*Lua l = new Lua();
            StreamReader sr = new StreamReader(@"C:\Users\Mike\Desktop\SMBX\LuaScriptsLib\deathcounter.lua");

            string metadata = "";
            for (int i = 0; i < 4; i++)
            {
                metadata += sr.ReadLine();
            }
            l.DoString(metadata);
            var author = l.Context.Get("__author").AsString();


            Console.WriteLine(author.ToString());*/
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}

        /*
		public static void LoadSettings()
		{
            if (File.Exists(InternalValues.SettingsPath))
            {
                JsonSerializer js = new JsonSerializer();
                js.Formatting = Formatting.Indented;
                using (StreamReader sr = new StreamReader(InternalValues.SettingsPath))
                {
                    using (JsonReader jsr = new JsonReader(sr))
                    {
                        SettingsInstance = js.Deserialize<SettingsObject>(jsr);
                    }
                }
            }
            else
            {
                SettingsInstance = SettingsObject();
                SettingsInstance.XmlDatabaseUrl = "";
            }
		}

		public static void SaveSettings()
		{
            JsonSerializer js = new JsonSerializer();
            js.Formatting = Formatting.Indented;

            using(StreamWriter sw = new StreamWriter(InternalValues.SettingsPath))
            {
                using(JsonWriter jsw = new JsonTextWriter(sw))
                {
                    js.Serialize(jsw, SettingsInstance);
                }
            }
		}
*/

	}
}
