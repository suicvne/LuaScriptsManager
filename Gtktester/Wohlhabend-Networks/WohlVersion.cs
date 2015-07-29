// /**
// * Author: Mike Santiago
// */
using System;

namespace Gtktester
{
    public class WohlVersion
    {
        public int id { get; set;}
        public string version {get;set;}

        public WohlVersion()
        {
            id = 0;
            version = "LunaLua 0.7.0.4";
        }
    }

    public class WohlJsonObj
    {
        public string latest { get; set;}

        public WohlVersion[] versions { get; set;}

        public WohlJsonObj(){}
    }
}

