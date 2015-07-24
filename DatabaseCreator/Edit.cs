// /**
// * Author: Mike Santiago
// */
using System;
using thing2;

namespace DatabaseCreator
{
    
    [System.ComponentModel.ToolboxItem(true)]
    public partial class Edit : Gtk.Bin
    {
        private LuaModule _module;
        public int Index { get; set;}

        public Edit()
        {
            this.Build();
        }

        public void LoadModuleInfo(LuaModule module, int index)
        {
            Index = index;
            _module = module;

            scriptNameEntry.Text = _module.ScriptName;
            scriptURL.Text = _module.LuaURL;
            resourceURL.Text = _module.ResURL;
            screenshotURL.Text = _module.ScreenshotURL;
            usageExample.Buffer.Text = _module.UsageExample;
        }

        public LuaModule GetCurrentModule()
        {
            _module.ScriptName = scriptNameEntry.Text;
            _module.LuaURL = scriptURL.Text;
            _module.ResURL = resourceURL.Text;
            _module.ScreenshotURL = screenshotURL.Text;
            _module.UsageExample = usageExample.Buffer.Text;

            return _module;
        }
    }
}

