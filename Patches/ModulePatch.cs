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
                MessageBox.Show($"Better crash report provided by Bannerlord Tweaks.\nNB: This does not mean this crash was necessarily caused by Bannerlord Tweaks, it is just allowing you to see what the error was. \n\n{__exception.ToStringFull()}", "Mount and Blade Bannerlord has crashed");
        }

        static bool Prepare()
        {
            return Settings.Instance.DebugMode;
        }
    }
}
