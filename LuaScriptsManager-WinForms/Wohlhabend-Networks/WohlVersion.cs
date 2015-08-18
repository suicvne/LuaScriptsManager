// /**
// * Author: Mike Santiago
// */
using System;

namespace WohlhabendNetworks
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

        public Version ReturnLatestVersion()
        {
            if (latest != null)
            {
                string[] split = latest.Split(new char[]{ ' ' });
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i].Contains("."))
                        return new Version(split[i].Trim());
                }
            }

            return new Version("0.0.0.0");
        }
    }
}

