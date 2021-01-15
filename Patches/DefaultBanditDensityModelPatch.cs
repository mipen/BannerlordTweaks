using HarmonyLib;
using System;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Map;

// Replaces DefaultTroopCoundLimitModelPatch as the method was removed in 1.4.3.
namespace BannerlordTweaks.Patches
{
    [HarmonyPatch(typeof(DefaultBanditDensityModel), "GetPlayerMaximumTroopCountForHideoutMission")]
    public class DefaultBanditDensityModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (BannerlordTweaksSettings.Instance is null) return true;
            __result = Math.Min(BannerlordTweaksSettings.Instance.HideoutBattleTroopLimit, 90);
            return false;
        }

        static bool Prepare() => BannerlordTweaksSettings.Instance is { } settings && settings.HideoutBattleTroopLimitTweakEnabled; 
    }
}
