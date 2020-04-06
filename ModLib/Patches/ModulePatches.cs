using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.MountAndBlade;

namespace ModLib.Patches
{
    [HarmonyPatch(typeof(TaleWorlds.MountAndBlade.Module), "OnApplicationTick")]
    public class OnApplicationTickPatch
    {
        static void Finalizer(Exception __exception)
        {
            if (__exception != null)
            {
                ModDebug.ShowError($"Mount and Blade Bannerlord has encountered an error and needs to close. See the error information below.",
                      "Mount and Blade Bannerlord has crashed", __exception);
            }
        }

        static bool Prepare()
        {
            return Settings.Instance.DebugMode;
        }
    }
}
