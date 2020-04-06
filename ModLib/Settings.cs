using ModLib.Interfaces;
using System;
using System.Xml.Serialization;

namespace ModLib
{
    public class Settings : ILoadable
    {
        private const string instanceID = "ModLibSettings";
        private static Settings _instance = null;
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Loader.Get<ModLib.Settings>(instanceID);
                    if (_instance == null)
                        throw new Exception("Unable to find ModLib settings in Loader");
                }
                return _instance;
            }
        }

        [XmlElement]
        public string ID { get; set; } = instanceID;
        [XmlElement]
        public bool DebugMode { get; set; } = true;
    }
}
