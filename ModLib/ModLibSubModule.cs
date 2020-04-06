using HarmonyLib;
using System;
using System.Windows.Forms;
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
                //Loader.Initialise(ModuleName);

                var harmony = new Harmony("mod.modlib.mipen");
                harmony.PatchAll();
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
