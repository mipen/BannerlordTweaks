using ModLib.Debug;
using ModLib.GUI.ViewModels;
using ModLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModLib
{
    public static class SettingsDatabase
    {
        private static List<ModSettingsVM> _modSettingsVMs = null;
        private static Dictionary<string, SettingsBase> AllSettingsDict { get; } = new Dictionary<string, SettingsBase>();

        public static List<SettingsBase> AllSettings => AllSettingsDict.Values.ToList();
        public static int SettingsCount => AllSettingsDict.Values.Count;
        public static List<ModSettingsVM> ModSettingsVMs
        {
            get
            {
                if (_modSettingsVMs == null)
                {
                    BuildModSettingsVMs();
                }
                return _modSettingsVMs;
            }
        }

        public static bool RegisterSettings(SettingsBase settingsClass, string uniqueID)
        {
            if (!AllSettingsDict.ContainsKey(uniqueID))
            {
                AllSettingsDict.Add(uniqueID, settingsClass);
                _modSettingsVMs = null;
                return true;
            }
            else
            {
                //TODO:: When debugging log is finished, show a message saying that the key already exists
                return false;
            }
        }

        public static ISerialisableFile GetSettings(string uniqueID)
        {
            if (AllSettingsDict.ContainsKey(uniqueID))
            {
                return AllSettingsDict[uniqueID];
            }
            else
                return null;
        }

        public static void BuildModSettingsVMs()
        {
            try
            {
                _modSettingsVMs = new List<ModSettingsVM>();
                foreach (var settings in AllSettings)
                {
                    ModSettingsVM msvm = new ModSettingsVM(settings);
                    _modSettingsVMs.Add(msvm);
                }
                _modSettingsVMs.Sort((x, y) => y.ModName.CompareTo(x.ModName));
            }
            catch (Exception ex)
            {
                ModDebug.ShowError("An error occurred while creating the ViewModels for all mod settings", "Error Occurred", ex);
            }
        }
    }
}
