using BannerlordTweaks.Lib;
using HarmonyLib;
using System;
using System.Windows.Forms;

namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(TaleWorlds.MountAndBlade.Module), "OnApplicationTick")]
    public class ModulePatch
    {
        static void Finalizer(Exception __exception)
        {
            if (__exception != null)
                MessageBox.Show($"Better crash report provided by Bannerlord Tweaks \n\n{__exception.ToStringFull()}", "Crash Report");
        }

        static bool Prepare()
        {
            return Settings.Instance.DebugMode;
        }
    }
}
