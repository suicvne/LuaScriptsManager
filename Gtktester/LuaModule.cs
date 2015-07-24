using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuaModuleManager
{
    public class LuaModule
    {
        /// <summary>
        /// The name of the script.
        /// </summary>
        public string ScriptName { get; set; }
        /// <summary>
        /// The direct URL to the Lua script.
        /// </summary>
        public string LuaURL { get; set; }
        /// <summary>
        /// The direct URL to the external Lua resources, or NULL if there isn't any.
        /// </summary>
        public string ResURL { get; set; }
        /// <summary>
        /// The direct URL to a screenshot, or NULL if there isn't one.
        /// </summary>
        public string ScreenshotURL { get; set; }
        /// <summary>
        /// A small usage example.
        /// </summary>
        public string UsageExample { get; set; }

        public LuaModule()
        {
            this.LuaURL = "NULL";
            this.ResURL = "NULL";
            this.ScreenshotURL = "NULL";
            this.UsageExample = "--None!";
        }
    }
}
