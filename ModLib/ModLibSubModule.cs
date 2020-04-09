using HarmonyLib;
using ModLib.Debugging;
using ModLib.GUI.GauntletUI;
using System;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace ModLib
{
    public class ModLibSubModule : MBSubModuleBase
    {
        public static string ModuleFolderName { get; } = "zzBannerlordTweaks";

        protected override void OnSubModuleLoad()
        {
            try
            {
                //Loader.Initialise(ModuleFolderName);
                SettingsDatabase.RegisterSettings(Settings.Instance, Settings.Instance.ID);

                var harmony = new Harmony("mod.modlib.mipen");
                harmony.PatchAll();

                Module.CurrentModule.AddInitialStateOption(new InitialStateOption("ModOptionsMenu", new TextObject("Mod Options"), 9990, () =>
                {
                    ScreenManager.PushScreen(new ModOptionsGauntletScreen());
                }, false));
            }
            catch (Exception ex)
            {
                ModDebug.ShowError($"An error occurred whilst initialising ModLib", "Error during initialisation", ex);
            }
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            SettingsDatabase.BuildModSettingsVMs();
        }
    }
}
