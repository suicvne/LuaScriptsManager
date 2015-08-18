using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuaScriptsManager_WinForms
{
    class LuaModules
    {
        public List<LuaModule> ModuleList = new List<LuaModule>();
        public LuaModules()
        {
            /*ModuleList.Add(new LuaModule()
            {
                ScriptName = "Level Timer",
                LuaURL = "http://mrmiketheripper.x10.mx/luamodulemanager/leveltimer.lua",
                ScreenshotURL = "NULL",
                ResURL = "http://mrmiketheripper.x10.mx/luamodulemanager/leveltimer.zip",
                UsageExample = "levelTimer = loadAPI(\"leveltimer\")\n\nfunction onLoad()\n\tlevelTimer.setSecondsLeft(300);\n\tlevelTimer.setTimerState(true);"
            });
            ModuleList.Add(new LuaModule()
            {
                ScriptName = "Death Counter",
                LuaURL = "http://mrmiketheripper.x10.mx/luamodulemanager/deathcounter.lua",
                ScreenshotURL = "NULL",
                ResURL = "NULL",
                UsageExample = "_deathCounter = loadAPI(\"deathcounter\");"
            });*/
        }
    }
}
