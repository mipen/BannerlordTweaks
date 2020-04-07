using ModLib.Attributes;
using ModLib.Interfaces;
using System;
using System.Xml.Serialization;

namespace ModLib
{
    public class Settings : SettingsBase, ILoadable
    {
        public override string ModName => "ModLib";
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
        [SettingProperty("Debug Mode", "When enabled, shows a message box showing the cause of a crash.", "Debugging")]
        public bool DebugMode { get; set; } = true;

        [SettingProperty("test val 1 bool", "this is a bool", "test group 1")]
        public bool testval1 { get; set; } = false;
        [SettingProperty("test val 2 int", 1, 25, "this is an int", "test group 1")]
        public int testval2 { get; set; } = 12;
        [SettingProperty("test val 3 float", 0.5f, 150f, "this is a float", "test group 2")]
        public float testval3 { get; set; } = 101.25f;
    }
}
