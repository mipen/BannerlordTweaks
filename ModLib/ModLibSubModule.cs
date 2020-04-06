using HarmonyLib;
using ModLib.Debug;
using ModLib.GauntletUI;
using System;
using System.Windows.Forms;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace ModLib
{
    public class ModLibSubModule : MBSubModuleBase
    {
        public static string ModuleName { get; } = "zzBannerlordTweaks";

        protected override void OnSubModuleLoad()
        {
            try
            {
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

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("event");
        }
    }
}
