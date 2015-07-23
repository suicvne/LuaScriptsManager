// /**
// * Author: Mike Santiago
// */
using System;

namespace Gtktester
{
    public class LuaScriptMetadata
    {
        public string Title {get;set;}
        public string Author {get;set;}
        public Version ScriptVersion {get;set;}
        public string Description {get;set;}
        public string URL { get; set;}

        /*private string __title = "Blank";
        private string __author = "Nobody";
        private Version __version = new Version("0.0.0.0");
        private string __description = "No description provided!";
        private string __url = "http://www.google.com/";*/

        public LuaScriptMetadata()
        {
            Title = "";
            Author = "";
            Description = "";
            URL = "";
            ScriptVersion = new Version("0.0.0.0");
        }
    }
}

